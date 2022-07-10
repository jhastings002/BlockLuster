CREATE TABLE [Movies] (
	[Id] varchar(36),
	[Title] varchar(max),
	[Description] varchar(max),
	[Rating] int,
	[DailyRate] decimal(7,2),
	[PictureLocation] varchar(max),
	[IsAvailable] bit,
	CONSTRAINT [PK_Movies] PRIMARY KEY ([Id])	
);
GO