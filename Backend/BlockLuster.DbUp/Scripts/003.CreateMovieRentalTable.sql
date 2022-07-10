CREATE TABLE [UserRentals] (
	[UserId] nvarchar(450) NOT NULL,
	[MovieId] varchar(36) NOT NULL,
	[RentalDate] date NOT NULL,
	[TotalCost] decimal(7,2),
	[IsReturned] bit,
	CONSTRAINT [PK_UserRentals] PRIMARY KEY ([UserId], [MovieId], [RentalDate]),
	CONSTRAINT [FK_UserRentalsAspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id]),
	CONSTRAINT [FK_UserRentalsMovies] FOREIGN KEY ([MovieId]) REFERENCES [Movies]([Id])
);
GO