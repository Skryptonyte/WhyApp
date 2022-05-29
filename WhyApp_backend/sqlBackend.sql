drop table mod_perms;
drop table moderators;
drop table bans;
drop table attachments;
drop table posts;
drop table rooms;
drop table chatuser;
drop table ranks;

create table ranks(rank_id int,    
            rank_Name varchar(30),  
            rank_color varchar(30),
            post_count int, 
            primary key(rank_id));

create table chatuser (
            user_id int, 
            username varchar(30), 
            password varchar(30), 
            joindate date, 
            rank_id int,
            primary key(user_id), 
            unique(username),
            foreign key(rank_id) references ranks on delete cascade);

create table rooms (
            room_id int, 
            room_name varchar(20), 
            room_desc varchar(100), 
            primary key(room_id));

create table posts (post_id int, 
            user_id int, 
            room_id int, 
            content varchar(256), 
            createdate timestamp, 
            primary key(post_id), 
            foreign key(user_id) references chatuser on delete cascade, 
            foreign key(room_id) references rooms on delete cascade);

create table attachments(attachment_id int, 
            post_id int, 
            filepath varchar(255), 
            primary key(attachment_id), 
            foreign key(post_id) references posts on delete cascade);

create table moderators(m_id int, 
            username varchar(30), 
            password varchar(30), 
            joindate date, 
            primary key(m_id), unique(username));


create table bans(user_id int, 
            room_id int, 
            startdate timestamp, 
            enddate timestamp, 
            ban_reason varchar(100), 
            primary key(user_id, room_id), 
            foreign key(user_id) references chatuser on delete cascade, 
            foreign key (room_id) references rooms on delete cascade);

create table mod_perms(
            m_id int,
            room_id int,
            ban_perm number(1),
            delete_perm number(1),
            mod_perm number(1),
            foreign key(m_id) references moderators on delete cascade,
            foreign key(room_id) references rooms on delete cascade,
            primary key(m_id, room_id));

insert into ranks values(0, 'Novice', 'white', 0);
insert into ranks values(1, 'Regular', 'blue', 5);
insert into ranks values(2, 'Patron', 'maroon', 10);
insert into ranks values(3, 'Adept', 'red', 50);
insert into ranks values(4, 'Expert', 'purple', 100);

drop sequence userIDGenerator;
drop sequence modIDGenerator;
drop sequence attachmentIDGenerator;
drop sequence postIDGenerator;
drop sequence roomIDGenerator;

create sequence userIDGenerator INCREMENT BY 1 START WITH 1;
create sequence modIDGenerator INCREMENT BY 1 START WITH 1;
create sequence postIDGenerator INCREMENT BY 1 START WITH 1;
create sequence attachmentIDGenerator INCREMENT BY 1 START WITH 1;
create sequence roomIDGenerator INCREMENT BY 1 START WITH 1;

create or replace trigger populateUserID
before insert on chatUser
for each row
declare
hashed_pass varchar(30);
begin
select userIDGenerator.nextVal into :new.user_id from dual;
select sysdate into :new.joindate from dual;
select 0 into :new.rank_id from dual;

if (length(:new.password) < 7) then
    raise_application_error(-20002,'Password constraints failed (Length at least 7)');
end if;

select ora_hash(:new.password) into hashed_pass from dual;
select hashed_pass into :new.password from dual;
end;
/


create or replace trigger populateModID
before insert on moderators
for each row
declare
hashed_pass varchar(30);
begin
select modIDGenerator.nextVal into :new.m_id from dual;
select sysdate into :new.joindate from dual;

if (length(:new.password) < 10) then
    raise_application_error(-20002,'Password constraints failed (Length at least 10)');
end if;
select ora_hash(:new.password) into hashed_pass from dual;
select hashed_pass into :new.password from dual;
end;
/


create or replace trigger populatePost
before insert on posts
for each row
declare
user_post_count int;
match_rank int;

current_rank int;
new_rank int;
begin
select postIDGenerator.nextVal into :new.post_id from dual;
select current_timestamp into :new.createdate from dual;

select count(*) into user_post_count from posts where user_id=:new.user_id;

select rank_id into new_rank from ranks where user_post_count >= post_count order by post_count desc fetch next 1 rows only;
select rank_id into current_rank from chatuser where user_id=:new.user_id;

if (current_rank != new_rank) then
    update chatuser set rank_id=new_rank;
end if;
end;
/


create or replace trigger populateAttachment
before insert on attachments
for each row
begin
select attachmentIDGenerator.nextVal into :new.attachment_id from dual;
end;
/



create or replace procedure verifyPerms (modid int, roomid int, ban int, del int, mod int)
as 
checkPerm int;
begin
    select count(*) into checkPerm from mod_perms where room_id = roomid and m_id = modid and ban <= ban_perm and mod <= mod_perm and del <= delete_perm;

    if checkPerm <= 0 then
        raise_application_error(-20001,'Insufficient privileges');
    end if;
end;
/

create or replace procedure deletePost (modid int, postid int)
as
post_room_id int;
begin

    select room_id into post_room_id from posts where post_id = postid;
    verifyPerms(modid, post_room_id, 0, 1, 0);
    delete from posts where post_id = postid;
end;
/

create or replace procedure modifyPost (modid int, postid int, new_content varchar)
as
post_room_id int;
begin

    select room_id into post_room_id from posts where post_id = postid;
    verifyPerms(modid, post_room_id, 0, 0, 1);
    update posts set content = new_content where post_id = postid;
end;
/