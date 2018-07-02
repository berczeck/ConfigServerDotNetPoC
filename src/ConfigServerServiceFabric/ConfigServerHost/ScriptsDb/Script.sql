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
ApplicationIdentifier INT NOT NULL,
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

INSERT INTO ConfigurationFile(Name,ApplicationIdentifier,Version,Content,Environment,Extension,Enabled)
VALUES('appsettings.dev.json',1,'1',
'{
  "application": {
    "dataBaseConnection": {
      "pricing": "Connecion de Pricing v1 dev",
      "invoicing": "Connecion de Invoicing v1 dev"
    }
  }
}','DEV','json',1)

SELECT * FROM [System]
SELECT * FROM [Application]
SELECT * FROM ConfigurationFile

UPDATE ConfigurationFile SET Content='{
  "application": {
    "dataBaseConnection": {
      "pricing": "Connecion de Pricing v1 dev",
      "invoicing": "Connecion de Invoicing v1 dev"
    }
  }
}' WHERE Identifier = 1

ALTER PROCEDURE USPS_GETConfigurationFileByApplicationEnvironment
@ApplicationName VARCHAR(100),
@Environment VARCHAR(50)
AS
SELECT CF.* FROM ConfigurationFile  AS CF
INNER JOIN [Application] AS A ON CF.ApplicationIdentifier = A.Identifier
WHERE A.Name = @ApplicationName AND Environment = @Environment
GO

EXEC USPS_GETConfigurationFileByApplicationEnvironment 'API_DEMO','DEV'