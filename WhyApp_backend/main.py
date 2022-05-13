from flask import Flask, jsonify, request
from flask_socketio import SocketIO, emit, join_room
import json
import cx_Oracle
import sys
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
    for row in cursor.execute("select * from rooms"):
        roomRows.append({'room_id':row[0], 'room_name': row[1]})

    return jsonify(roomRows)

@app.route('/api/posts/<room_id>',methods=['GET'])
def getPosts(room_id):
    cursor = conn.cursor()

    postRows = []
    for row in cursor.execute(f"select post_id, username, content, createDate from posts natural join chatUser where room_id = {room_id} order by createdate"):
        postRows.append({'post_id':row[0], 'username': row[1], 'content': row[2], 'createDate': row[3]})

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

    try:
        post_ids = cursor.var(int, arraysize=1)
        cmd = cursor.execute("insert into posts values(NULL, :userID, :roomID, :content, NULL) returning post_id into :out",(userID, roomID, content, post_ids))
        print("Post inserted")

        insertedPostID = post_ids.values[0][0]
        print("Inserted POST ID:",insertedPostID)

        cursor.execute(f"select post_id, username, createDate, content from posts natural join chatUser where post_id={insertedPostID}")
        row = (cursor.fetchone())

        jsonBroadcast = {}
        jsonBroadcast['post_id'] = insertedPostID
        jsonBroadcast['username'] = row[1]
        jsonBroadcast['createDate'] = str(row[2])
        jsonBroadcast['content'] = row[3]

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
