﻿INSERT INTO [dbo].[AspNetUsers]
           ([Id]
           ,[UserName]
           ,[IsAdmin]
           ,[NormalizedUserName]
           ,[Email]
           ,[NormalizedEmail]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[ConcurrencyStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEnd]
           ,[LockoutEnabled]
           ,[AccessFailedCount]
           ,[IsDeactivated]
           ,[FirstName]
           ,[LastName])
     VALUES
           ('10000000-0000-0000-0000-000000000001'
           ,'Admin@Admin.com'
           ,1
           ,'ADMIN@ADMIN.COM'
           ,'Admin@Admin.com'
           ,'ADMIN@ADMIN.COM'
           ,0
           ,'AQAAAAEAACcQAAAAEByxNsZp4nkSopJ2p9s+JbAMppwCsENt48rBw7qjgnZls6hLF5hgyvA/jeTysDgN2Q=='
           ,NULL
           ,NULL
           ,NULL
           ,0
           ,0
           ,NULL
           ,0
           ,0
           ,0
           ,'Admin'
           ,'Admin')
GO