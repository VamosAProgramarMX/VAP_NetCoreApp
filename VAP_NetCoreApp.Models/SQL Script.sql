USE [VAP_NetCoreApp]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 05/10/2021 06:12:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
[ID] [int] IDENTITY(1,1) NOT NULL,
[Name] [varchar](500) NOT NULL,
CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED
(
[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [UQ_Role_Name] UNIQUE NONCLUSTERED
(
[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 05/10/2021 06:12:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
[ID] [int] IDENTITY(1,1) NOT NULL,
[RoleID] [int] NOT NULL,
[Email] [varchar](500) NOT NULL,
[Age] [int] NULL,
[IsActive] [bit] NOT NULL,
[RecordDate] [datetimeoffset](7) NOT NULL,
[Balance] [money] NOT NULL,
CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED
(
[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [UQ_User_Email] UNIQUE NONCLUSTERED
(
[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[UserVM]    Script Date: 05/10/2021 06:12:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[UserVM] AS
SELECT U.ID, U.RoleID, R.Name AS Role, U.Email,
U.Age, U.IsActive, U.RecordDate, U.Balance
FROM [USER] AS U
INNER JOIN [Role] AS R
ON U.RoleID = R.ID
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsActive]  DEFAULT ('True') FOR [IsActive]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_RecordDate]  DEFAULT (sysdatetimeoffset()) FOR [RecordDate]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Balance]  DEFAULT ((0)) FOR [Balance]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [CH_User_Age] CHECK  (((0)<=[Age] AND [Age]<=(100)))
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [CH_User_Age]
GO
/****** Object:  StoredProcedure [dbo].[User_AddUser]    Script Date: 05/10/2021 06:12:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_AddUser]
@Email VARCHAR(250),
@Balance MONEY,
@Age INT,
@RoleID INT
AS
BEGIN

DECLARE @Success BIT = 0
DECLARE @Message VARCHAR(500)

IF EXISTS (SELECT * FROM [User] WHERE Email = @Email) BEGIN
SET @Message = 'El email esta siendo usado por otro usuario'
END
ELSE  BEGIN

INSERT INTO [User] (Email, Balance, RoleID, Age)
VALUES (@Email, @Balance, @RoleID, @Age)

SET @Success = 1
END

SELECT @Success AS Success, @Message AS Message

END
GO
