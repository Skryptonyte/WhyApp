from flask import Flask, jsonify, request, stream_with_context, Response
from flask_socketio import SocketIO, emit, join_room
import json
import cx_Oracle
import sys
import filetype


import uuid
conn = cx_Oracle.connect(user=sys.argv[1],password=sys.argv[2],events=True)
app = Flask(__name__)
socketio = SocketIO(app, cors_allowed_origins='*')


@app.route('/api/login',methods=['GET'])
def validateLogin():
    username=request.args.get("username")
    password=request.args.get("password")
    print(type(username), password)
    cursor = conn.cursor()
    cursor.execute("select user_id from chatUser where username = :1 and password=ora_hash( :2 )",(username, password))
    
    result = cursor.fetchone()
    if (cursor.rowcount > 0):
        return str(result[0])
    else:
        return "-1"
@app.route('/api/rooms',methods=['GET'])
def getRooms():
    cursor = conn.cursor()

    roomRows = []
    for row in cursor.execute("select room_id, room_name from rooms"):
        roomRows.append({'room_id':row[0], 'room_name': row[1]})

    return jsonify(roomRows)

@app.route('/api/posts/<room_id>',methods=['GET'])
def getPosts(room_id):
    cursor = conn.cursor()

    postRows = []

    query = f"""
    with p as (select user_id, post_id, createDate, content from posts where room_id={room_id} order by createdate),
    cu as (select * from chatUser natural join ranks)
    select p.post_id, cu.username, p.content, p.createDate, a.attachment_id, cu.rank_name, cu.rank_color from (p natural join cu ) left join attachments a on p.post_id = a.post_id
    """
    
    for row in cursor.execute(query):
        atid = -1
        if (row[4] is not None):
            atid = row[4]
            print("Kek: ",atid)
        postRows.append({'post_id':row[0], 'username': row[1], 'content': row[2], 'createDate': row[3], 'attach_id': atid, 'rank_name':row[5], 'rank_color': row[6]})

    return jsonify(postRows)

@app.route("/api/register", methods=['POST'])
def registerUser():
    cursor = conn.cursor()
    request_json = request.get_json()
    username = request_json.get('username')
    password = request_json.get('password')


    try:
        cursor.execute("insert into chatUser values(NULL, :username, ora_hash(:password), NULL)",(username, password))
        conn.commit()
        print(username,"is created")
    except Exception as e:
        print(e)
        print("Failed to create user.")
        return "0"
    
    return "1"

@app.route("/api/upload", methods=['POST'])
def uploadAttachment():
    filename = str(uuid.uuid4())
    f = open("uploads/"+filename,"wb+")
    firstChunk = 1
    while True:
        chunk = request.stream.read(1024)
        if len(chunk) == 0:
            break
        f.write(chunk)

        if (firstChunk):
            f.flush()
            magicobj=filetype.guess("uploads/"+filename)
            if (magicobj is None or not (magicobj.mime.startswith("image"))):
                print("Magic object: ",magicobj)
                try:
                    print("MIME type: ",magicobj.mimetype)
                except Exception as e:
                    print("no object :(")
                print("Attachment is not image. Rejecting!")
                return "-1"
            firstChunk = 0

    cursor = conn.cursor()
    try:
        attach_id = cursor.var(int, arraysize=1)
        cursor.execute(f"insert into attachments values(NULL, NULL,:filePath) returning attachment_id into :attachID",("uploads/"+filename,attach_id))
        conn.commit()

        insertedAttachID = attach_id.values[0][0]


        print("Attach new file")
        return str(insertedAttachID)
    except Exception as e:
        print(e)
        print("Failed to create attachment")
        return "-1"

@app.route("/api/download/<attach_id>", methods=['GET'])
def downloadAttachment(attach_id):
    try:
        cursor = conn.cursor()
        cursor.execute(f"select filepath from attachments where attachment_id=:attach_id",(int(attach_id),))

        row = cursor.fetchone()
        filepath = row[0]

        def generate():
            f = open(filepath,"rb+")
            content = f.read(1024)
            while (content):
                yield content
                content = f.read(1024)

        filetypeobj = filetype.guess(filepath)
        if (filetypeobj is not None):
            return Response(stream_with_context(generate()), mimetype=filetypeobj.mime)
        else:
            return Response(stream_with_context(generate()))
    except Exception as e:
        print(e)
        print("Download failed")

        return "Stream failed"

uid_to_sid = {}
sid_to_uid = {}
@socketio.on("joinChat")
def joinRoom(json_str):
    jsonDict = json.loads(json_str)

    user_id = jsonDict["user_id"]
    room_id = jsonDict["room_id"]

    if (uid_to_sid.get(int(user_id),None)):
        emit("errorMessage","Already connected to a room")
        print(user_id," is already present")
        return

    try:
        
        cursor = conn.cursor()
        
        cursor.execute("delete from bans where user_id=:userid and current_timestamp > enddate",(user_id, ))
        conn.commit()
        cursor.execute("select user_id, room_id, ban_reason, startdate from bans where user_id=:userid and room_id=:roomid",(user_id,room_id))

        row = cursor.fetchone()
        if (row is not None):
            print(f"Banned user {user_id} attempting to join {room_id}")
            emit("errorMessage", f"You are banned on {row[3]} from this room for reason '{row[2]}'")
            return
    except Exception as e:
        print(e)
        return
    print("Mapping SID and UID for",user_id)
    uid_to_sid[int(user_id)] = request.sid
    sid_to_uid[request.sid] = int(user_id)

    join_room(room_id)
    print("UID",user_id, "has joined the room ID",room_id)

@socketio.on('disconnect')
def connect():
    sid = request.sid

    if (not sid_to_uid.get(sid,None)):
        print("Unidentified client disconnected")
        return
    uid = sid_to_uid[sid]
    print("UID "+str(uid)+" has disconnected")

    uid_to_sid.pop(uid)
    sid_to_uid.pop(sid)

@socketio.on("postChat")
def postChat(json_str):
    cursor = conn.cursor()
    
    print(json_str)

    jsonDict = json.loads(json_str)
    userID = jsonDict['user_id']
    roomID = jsonDict['room_id']
    content = jsonDict['content']
    attach_id = jsonDict['attach_id']
    try:
        post_ids = cursor.var(int, arraysize=1)
        cmd = cursor.execute("insert into posts values(NULL, :userID, :roomID, :content, NULL) returning post_id into :out",(userID, roomID, content, post_ids))
        print("Post inserted")

        insertedPostID = post_ids.values[0][0]
        print("Inserted POST ID:",insertedPostID)
        if (attach_id != -1):
            cursor.execute("update attachments set post_id=:postid where attachment_id=:attach_id",(insertedPostID,attach_id))
        cursor.execute(f"""
        with p as (select user_id, post_id, createDate, content from posts where post_id={insertedPostID}), 
        cu as (select user_id, username, rank_name, rank_color from chatUser natural join ranks)
        select p.post_id, cu.username, p.createDate, p.content,cu.rank_name, cu.rank_color from (p natural join cu)
        """)
        row = (cursor.fetchone())

        jsonBroadcast = {}
        jsonBroadcast['post_id'] = insertedPostID
        jsonBroadcast['username'] = row[1]
        jsonBroadcast['createDate'] = str(row[2])
        jsonBroadcast['content'] = row[3]
        jsonBroadcast['rank_name'] = row[4]
        jsonBroadcast['rank_color'] = row[5]
        """
        if (row[4] is None):
            jsonBroadcast['attach_id'] = -1
        else:
            jsonBroadcast['attach_id'] = attach_id
        """

        jsonBroadcast['attach_id'] = attach_id
        print("File Path: ",row[4])
        jsonBroadcastStr = (json.dumps(jsonBroadcast))
        jsonBroadcastStr = "[" + jsonBroadcastStr + "]"
        conn.commit()

        print(jsonBroadcastStr)
        print("Broadcasting to everyone in room ID",roomID)
        emit("recievePost",jsonBroadcastStr, room=roomID)

    except Exception as e:
        print(e)
        print("Failed to create post.")
        emit("failedPost")
    

if __name__ == '__main__':
    socketio.run(app, debug=True)
