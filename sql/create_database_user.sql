CREATE USER MY_APPLICATION_USER WITH PASSWORD = '(a safe password here)'

EXEC sp_addrolemember 'db_datawriter', 'MY_APPLICATION_USER';
EXEC sp_addrolemember 'db_datareader', 'MY_APPLICATION_USER';


-- After this the Azure Connection String will look something like this
-- Server=tcp:avansict(studentnummer).database.windows.net,1433;Initial Catalog=db(studentnummer);Persist Security Info=False;User ID=MY_APPLICATION_USER;Password=(a safe password here);MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;