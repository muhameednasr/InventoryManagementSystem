CREATE TABLE [dbo].[tbUser]
(
	[username] VARCHAR(50) NOT NULL , 
    [fullname] VARCHAR(50) NULL, 
    [password] VARCHAR(50) NULL, 
    [phone] VARCHAR(50) NOT NULL, 
    PRIMARY KEY ([phone])
)

