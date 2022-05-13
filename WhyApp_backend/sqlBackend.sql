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