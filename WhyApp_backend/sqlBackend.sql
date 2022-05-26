
create table ranks(rank_id int, rank_Name varchar(30), rank_color varchar(30), post_count int, primary key(rank_id));
create table chatuser (user_id int, username varchar(30), password varchar(30), joindate date, foreign key(rank_id) references(ranks),primary key(user_id), unique(username));
create table rooms (room_id int, room_name varchar(20), room_desc varchar(100), primary key(room_id));
create table posts (post_id int, user_id int, room_id int, content varchar(256), createdate timestamp, primary key(post_id), foreign key(user_id) references chatuser, foreign key(room_id) references rooms);
create table attachments(attachment_id int, post_id int, filepath varchar(255), primary key(attachment_id), foreign key(post_id) references posts);
create table bans(user_id int, room_id int, startdate timestamp, enddate timestamp, ban_reason varchar(100), primary key(user_id, room_id), foreign key(user_id) references chatuser, foreign key (room_id) references rooms);

insert into ranks values(0, "Novice", "white", 0)

create sequence userIDGenerator INCREMENT BY 1 START WITH 1;

create or replace trigger populateUserID
before insert on chatUser
for each row
begin
select userIDGenerator.nextVal into :new.user_id from dual;
select sysdate into :new.joindate from dual;
select 2 into :new.rank_id from dual;
end;

create sequence postIDGenerator INCREMENT BY 1 START WITH 1;

create or replace trigger populatePost
before insert on posts
for each row
begin
select postIDGenerator.nextVal into :new.post_id from dual;
select current_timestamp into :new.createdate from dual;
end;


create sequence attachmentIDGenerator INCREMENT BY 1 START WITH 1;

create or replace trigger populateAttachment
before insert on attachments
for each row
begin
select attachmentIDGenerator.nextVal into :new.attachment_id from dual;
end;

create or replace procedure updateBan
as
begin
    delete from bans where current_timestamp > enddate;
end;