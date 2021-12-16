CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


CREATE TABLE stations(  
    id int NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'Primary Key',
    create_time DATETIME COMMENT 'Create Time',
    update_time DATETIME COMMENT 'Update Time',
    name TEXT NOT NULL COMMENT 'The Name Of the Station',
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) DEFAULT CHARSET UTF8 COMMENT '';

CREATE TABLE accountstations(  
    id int NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'Primary Key',
    accountId VARCHAR(255) NOT NULL,
    stationId INT NOT NULL,

    FOREIGN KEY (accountId)
        REFERENCES accounts(id)
        ON DELETE CASCADE,

    FOREIGN KEY (stationId)
        REFERENCES stations(id)
        ON DELETE CASCADE

) DEFAULT CHARSET UTF8 COMMENT '';



INSERT INTO accountstations
(accountId, stationId)
VALUES
("61bb7496776066b1d032f988", 10);

SELECT * FROM accountstations;

/* Get all elves at a station */
/* From on a many to many FROM will always be the many to many table */
/* WHERE is the many to many table and which side of the relationship data you have */
/* JOIN starts with the data you actually want */
SELECT
  a.*,
  acctStations.id AS accountStationId
FROM accountstations acctStations
JOIN accounts a ON acctStations.accountId = a.id
WHERE acctStations.stationId = 10;




/* get all stations by account id */
SELECT
  s.*,
  acctStations.id AS accountStationId
FROM accountstations acctStations
JOIN stations s ON s.id = acctStations.stationId
WHERE acctStations.accountId = "61bb7496776066b1d032f988"

/* future populating data retrieved from the many-to-many */
SELECT
  s.*,
  acctStations.id AS accountStationId,
  a.*
FROM accountstations acctStations
JOIN stations s ON s.id = acctStations.stationId
JOIN account a ON a.id = s.creatorId
WHERE acctStations.accountId = "61bb7496776066b1d032f988"

/* 
  query
  _db.Query<AccountStationViewModel, Account, AccountStationViewModel>(sql, (vm, acct)=>{
    vm.creator = acct;
    return vm;
  }, new {accountId}, splitOn: 'id')
 */