create database counter_app;
create role counter login password 'counter123';
grant all privileges on database counter_app to counter;