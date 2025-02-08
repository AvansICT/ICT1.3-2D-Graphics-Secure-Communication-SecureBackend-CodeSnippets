-- On the master database
CREATE LOGIN the_real_user WITH PASSWORD = '(your password)';
GO

-- On the db<studentnumber> database
CREATE USER the_real_user FOR LOGIN the_real_user;
GO

EXEC sp_addrolemember 'db_datawriter', 'the_real_user';

