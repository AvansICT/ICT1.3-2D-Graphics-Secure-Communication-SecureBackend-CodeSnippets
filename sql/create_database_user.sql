CREATE USER MY_APPLICATION_USER WITH PASSWORD = '(a safe password here)'

EXEC sp_addrolemember 'db_datawriter', 'MY_APPLICATION_USER';
EXEC sp_addrolemember 'db_datareader', 'MY_APPLICATION_USER';
