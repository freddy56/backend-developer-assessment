CREATE TABLE [dbo].[Artist] (
    [ArtistGuid] VARCHAR (50)   NOT NULL,
    [ArtistName] VARCHAR (50)   NOT NULL,
    [Aliases]    NVARCHAR (MAX) NULL,
    [Country]    CHAR (10)      NOT NULL,
    [ArtistId]   INT            IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_Artist] PRIMARY KEY CLUSTERED ([ArtistId] ASC)
);

