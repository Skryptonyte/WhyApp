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
    for row in cursor.execute(f"""
    with p as (select user_id, post_id, createDate, content from posts where room_id={room_id} order by createdate)
    select p.post_id, cu.username, p.content, p.createDate, a.attachment_id from (p natural join chatUser cu) left join attachments a on p.post_id = a.post_id 
    """):
        atid = -1
        if (row[4] is not None):
            atid = row[4]
            print("Kek: ",atid)
        postRows.append({'post_id':row[0], 'username': row[1], 'content': row[2], 'createDate': row[3], 'attach_id': atid})

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
    while True:
        chunk = request.stream.read(1024)
        if len(chunk) == 0:
            break

        f.write(chunk)

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

@socketio.on("joinChat")
def joinRoom(json_str):
    jsonDict = json.loads(json_str)

    user_id = jsonDict["user_id"]
    room_id = jsonDict["room_id"]

    join_room(room_id)
    print("UID",user_id, "has joined the room ID",room_id)

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
        with p as (select user_id, post_id, createDate, content from posts where post_id={insertedPostID})
        select p.post_id, cu.username, p.createDate, p.content from (p natural join chatUser cu)
        """)
        row = (cursor.fetchone())

        jsonBroadcast = {}
        jsonBroadcast['post_id'] = insertedPostID
        jsonBroadcast['username'] = row[1]
        jsonBroadcast['createDate'] = str(row[2])
        jsonBroadcast['content'] = row[3]
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
