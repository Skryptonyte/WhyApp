
create table chatuser (user_id int, username varchar(30), password varchar(30), joindate date, primary key(user_id));
create table rooms (room_id int, room_name varchar(20), room_desc varchar(100), primary key(room_id));
create table posts (post_id int, user_id int, room_id int, content varchar(256), createdate timestamp, primary key(post_id), foreign key(user_id) references chatuser, foreign key(room_id) references rooms)

create sequence userIDGenerator INCREMENT BY 1 START WITH 1;

create or replace trigger populateUserID
before insert on chatUser
for each row
begin
select userIDGenerator.nextVal into :new.user_id from dual;
select sysdate into :new.joindate from dual;
end;

create sequence postIDGenerator INCREMENT BY 1 START WITH 1;

create or replace trigger populatePost
before insert on posts
for each row
begin
select postIDGenerator.nextVal into :new.post_id from dual;
select current_timestamp into :new.createdate from dual;
end;