CREATE TABLE Artists (
    Id int NOT NULL,
    Name varchar(255) NOT NULL UNIQUE,
	PRIMARY KEY (Id)
);

CREATE TABLE Songs(
   Id        int  NOT NULL PRIMARY KEY 
  ,Name      VARCHAR(255) NOT NULL
  ,Year      int  NOT NULL
  ,Artist    VARCHAR(255) NOT NULL
  ,Shortname VARCHAR(255) NOT NULL
  ,Bpm       Int  NOT NULL
  ,Duration  Int  NOT NULL
  ,Genre     VARCHAR(255) NOT NULL
  ,SpotifyId VARCHAR(255) NOT NULL
  ,Album     VARCHAR(255) NOT NULL
);