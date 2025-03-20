CREATE USER MY_APPLICATION_USER WITH PASSWORD = '(a safe password here)'

EXEC sp_addrolemember 'db_datawriter', 'MY_APPLICATION_USER';
EXEC sp_addrolemember 'db_datareader', 'MY_APPLICATION_USER';
-- Uncomment this line if you would like the user to have full database administrator rights
-- EXEC sp_addrolemember 'db_owner', 'MY_APPLICATION_USER' 

