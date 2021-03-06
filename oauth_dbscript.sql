USE [TestApiOAuthDb]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 05/17/2012 13:35:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](4000) NOT NULL,
	[Description] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Resource]    Script Date: 05/17/2012 13:35:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resource](
	[Id] [int] NOT NULL,
	[Uri] [nvarchar](4000) NOT NULL,
	[Description] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 05/17/2012 13:35:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] NOT NULL,
	[ClaimId] [nvarchar](4000) NOT NULL,
	[ClaimName] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 05/17/2012 13:35:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[Id] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleResourceAction]    Script Date: 05/17/2012 13:35:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleResourceAction](
	[Id] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[ResourceId] [int] NOT NULL,
	[AllowedMethods] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_RoleResourceAction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Application]    Script Date: 05/17/2012 13:35:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Application](
	[Id] [int] NOT NULL,
	[AppPublicId] [nvarchar](4000) NOT NULL,
	[AppSecret] [nvarchar](4000) NOT NULL,
	[Name] [nvarchar](4000) NOT NULL,
	[Created] [datetime] NOT NULL,
	[OwnerId] [int] NULL,
 CONSTRAINT [PK_Application] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppAuthorization]    Script Date: 05/17/2012 13:35:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppAuthorization](
	[Id] [int] NOT NULL,
	[AppId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Scope] [nvarchar](4000) NOT NULL,
	[Created] [datetime] NOT NULL,
	[AuthTokenExpiration] [datetime] NULL,
	[Token] [nvarchar](4000) NOT NULL,
	[RefreshToken] [nvarchar](4000) NULL,
	[RefreshTokenExpiration] [datetime] NULL,
 CONSTRAINT [PK_AppAuthorization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [Application_AppAuthorization]    Script Date: 05/17/2012 13:35:31 ******/
ALTER TABLE [dbo].[AppAuthorization]  WITH CHECK ADD  CONSTRAINT [Application_AppAuthorization] FOREIGN KEY([AppId])
REFERENCES [dbo].[Application] ([Id])
GO
ALTER TABLE [dbo].[AppAuthorization] CHECK CONSTRAINT [Application_AppAuthorization]
GO
/****** Object:  ForeignKey [User_AppAuthorization]    Script Date: 05/17/2012 13:35:31 ******/
ALTER TABLE [dbo].[AppAuthorization]  WITH CHECK ADD  CONSTRAINT [User_AppAuthorization] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[AppAuthorization] CHECK CONSTRAINT [User_AppAuthorization]
GO
/****** Object:  ForeignKey [User_Application]    Script Date: 05/17/2012 13:35:31 ******/
ALTER TABLE [dbo].[Application]  WITH CHECK ADD  CONSTRAINT [User_Application] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Application] CHECK CONSTRAINT [User_Application]
GO
/****** Object:  ForeignKey [Resource_RoleResourceAction]    Script Date: 05/17/2012 13:35:31 ******/
ALTER TABLE [dbo].[RoleResourceAction]  WITH CHECK ADD  CONSTRAINT [Resource_RoleResourceAction] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resource] ([Id])
GO
ALTER TABLE [dbo].[RoleResourceAction] CHECK CONSTRAINT [Resource_RoleResourceAction]
GO
/****** Object:  ForeignKey [Role_RoleResourceAction]    Script Date: 05/17/2012 13:35:31 ******/
ALTER TABLE [dbo].[RoleResourceAction]  WITH CHECK ADD  CONSTRAINT [Role_RoleResourceAction] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[RoleResourceAction] CHECK CONSTRAINT [Role_RoleResourceAction]
GO
/****** Object:  ForeignKey [Role_UserRole]    Script Date: 05/17/2012 13:35:31 ******/
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [Role_UserRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [Role_UserRole]
GO
/****** Object:  ForeignKey [User_UserRole]    Script Date: 05/17/2012 13:35:31 ******/
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [User_UserRole] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [User_UserRole]
GO
