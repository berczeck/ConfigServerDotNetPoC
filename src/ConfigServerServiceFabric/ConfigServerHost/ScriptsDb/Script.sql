DROP TABLE [System];
DROP TABLE [Application];
DROP TABLE ConfigurationFile;

CREATE TABLE [System]
(
Identifier INT IDENTITY(1,1) PRIMARY KEY,
[Name] VARCHAR(100) NOT NULL,
[Enabled] bit not null
)
GO

CREATE TABLE [Application]
(
Identifier INT IDENTITY(1,1) PRIMARY KEY,
SystemIdentifier INT NOT NULL,
[Name] VARCHAR(100) NOT NULL,
[Enabled] bit not null
)
GO

CREATE TABLE ConfigurationFile
(
Identifier INT IDENTITY(1,1) PRIMARY KEY,
SystemIdentifier INT NULL,
ApplicationIdentifier INT NULL,
ConfigurationFileParentIdentifier INT NULL,
[Name] VARCHAR(100) NOT NULL,
[Version] VARCHAR(50) NOT NULL,
Content VARCHAR(MAX) NOT NULL,
Environment VARCHAR(50) NOT NULL,
Extension VARCHAR(5) NOT NULL,
[Enabled] bit not null
)
GO

INSERT INTO [System](Name, Enabled)
VALUES('DOTTIE',1);

INSERT INTO [Application](Name,SystemIdentifier, Enabled)
VALUES('API_DEMO',1,1);

INSERT INTO ConfigurationFile(Name,SystemIdentifier,Version,Content,Environment,Extension,Enabled)
VALUES('dottiesettings.dev.json',1,'1',
'{
  "application": {
    "dataBaseConnection": {
      "pricing": "Connecion de Pricing v1 dev",
      "invoicing": "Connecion de Invoicing v1 dev"
    }
  }
}','DEV','json',1)

INSERT INTO ConfigurationFile(Name,ApplicationIdentifier,Version,Content,Environment,Extension,Enabled)
VALUES('appsettings.dev.json',1,'1',
'{
  "application": {
    "ftp": {
      "server": "sftp://localhost",
      "path": "/new/files"
    }
  }
}','DEV','json',1)

INSERT INTO ConfigurationFile(Name,ApplicationIdentifier,ConfigurationFileParentIdentifier,Version,Content,Environment,Extension,Enabled)
VALUES('',1,1,'','','','',1)


SELECT * FROM [System]
SELECT * FROM [Application]
SELECT * FROM ConfigurationFile

UPDATE ConfigurationFile SET Content=
'{
  "application": {
    "dataBaseConnection": {
      "pricing": "Connecion de Pricing v2 dev",
      "invoicing": "Connecion de Invoicig v2 dev"
    }
  }
}' WHERE Identifier = 1


UPDATE ConfigurationFile SET Content=
'{
  "application": {
    "ftp": {
      "server": "sftp://localhost2",
      "path": "/new/files2"
    }
  }
}' WHERE Identifier = 2

ALTER PROCEDURE USPS_GETConfigurationFileByApplicationEnvironment
@ApplicationName VARCHAR(100),
@Environment VARCHAR(50)
AS
SELECT CF.* FROM ConfigurationFile  AS CF
INNER JOIN [Application] AS A ON CF.ApplicationIdentifier = A.Identifier
WHERE A.Name = @ApplicationName AND Environment = @Environment
UNION ALL
SELECT CFP.* FROM ConfigurationFile  AS CF
INNER JOIN [Application] AS A ON CF.ApplicationIdentifier = A.Identifier
INNER JOIN ConfigurationFile AS CFP ON CF.ConfigurationFileParentIdentifier = CFP.Identifier
WHERE A.Name = @ApplicationName AND CFP.Environment = @Environment
GO

EXEC USPS_GETConfigurationFileByApplicationEnvironment 'API_DEMO','DEV'