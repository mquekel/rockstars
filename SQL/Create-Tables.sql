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
  ,Shortname VARCHAR(255)
  ,Bpm       Int
  ,Duration  Int
  ,Genre     VARCHAR(255)
  ,SpotifyId VARCHAR(255)
  ,Album     VARCHAR(255)
);