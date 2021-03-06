USE [bookstore]
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([Id], [BookName], [Author], [Publisher], [PublishedDate], [Quantity], [UnitPrice], [IsDeleted], [UpdatedAt], [CreatedAt]) VALUES (1, N'Book1', N'Author1', N'Pub1', CAST(N'2020-09-01T22:24:17.000' AS DateTime), 98, CAST(29 AS Decimal(18, 0)), 0, CAST(N'2020-09-04T08:55:28.733' AS DateTime), CAST(N'2020-09-03T22:24:47.000' AS DateTime))
INSERT [dbo].[Books] ([Id], [BookName], [Author], [Publisher], [PublishedDate], [Quantity], [UnitPrice], [IsDeleted], [UpdatedAt], [CreatedAt]) VALUES (3, N'Book2', N'Author2', N'Pub2', CAST(N'2020-09-01T22:24:17.000' AS DateTime), 96, CAST(19 AS Decimal(18, 0)), 0, CAST(N'2020-09-04T08:55:28.767' AS DateTime), CAST(N'2020-09-03T22:24:47.000' AS DateTime))
INSERT [dbo].[Books] ([Id], [BookName], [Author], [Publisher], [PublishedDate], [Quantity], [UnitPrice], [IsDeleted], [UpdatedAt], [CreatedAt]) VALUES (4, N'testCreate', N'testAuthor', N'testPublisher', CAST(N'2019-01-01T00:00:00.000' AS DateTime), 150, CAST(35 AS Decimal(18, 0)), 0, CAST(N'2020-09-03T16:47:23.820' AS DateTime), CAST(N'2020-09-03T16:47:23.813' AS DateTime))
INSERT [dbo].[Books] ([Id], [BookName], [Author], [Publisher], [PublishedDate], [Quantity], [UnitPrice], [IsDeleted], [UpdatedAt], [CreatedAt]) VALUES (5, N'testCreate', N'testAuthor', N'testPublisher', CAST(N'2019-01-01T00:00:00.000' AS DateTime), 150, CAST(35 AS Decimal(18, 0)), 0, CAST(N'2020-09-03T17:44:45.937' AS DateTime), CAST(N'2020-09-03T16:47:43.027' AS DateTime))
INSERT [dbo].[Books] ([Id], [BookName], [Author], [Publisher], [PublishedDate], [Quantity], [UnitPrice], [IsDeleted], [UpdatedAt], [CreatedAt]) VALUES (6, N'testUpdate', N'updateAuthor', N'updatePublisher', CAST(N'2019-01-01T00:00:00.000' AS DateTime), 200, CAST(15 AS Decimal(18, 0)), 0, CAST(N'2020-09-03T18:18:59.997' AS DateTime), CAST(N'2020-09-03T17:15:16.047' AS DateTime))
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [Email], [HashPassword], [Role], [Name], [IsDeleted], [UpdatedAt], [CreatedAt]) VALUES (2, N'admin', N'haunguyen@mailsac.com', N'$2a$11$WxepArZaiGdjQkjUCuXsz.V7ZT/p5BvqFX5GDBSRS5NWlTUvmvVDy', N'admin', N'admin', 0, CAST(N'2020-08-30T22:14:51.000' AS DateTime), CAST(N'2020-08-30T22:14:54.000' AS DateTime))
INSERT [dbo].[Users] ([Id], [Username], [Email], [HashPassword], [Role], [Name], [IsDeleted], [UpdatedAt], [CreatedAt]) VALUES (3, N'test', N'test@example.com', N'$2a$11$KsJ.NdB9lLYeNh4ljq8cAu/6Xk8W0Fxuw/9ZTzXPJPme6JYjl.38K', N'user', N'test', 0, CAST(N'2020-09-02T11:54:43.573' AS DateTime), CAST(N'2020-09-02T11:54:43.323' AS DateTime))
INSERT [dbo].[Users] ([Id], [Username], [Email], [HashPassword], [Role], [Name], [IsDeleted], [UpdatedAt], [CreatedAt]) VALUES (4, N'test2', N'test2@example.com', N'$2a$11$DwT.MmQteQEo/Ups2KtYdOVhgbE0Lhm5guslpF1V/30uExDZcRnp2', N'user', N'test', 0, CAST(N'2020-09-02T15:11:34.407' AS DateTime), CAST(N'2020-09-02T15:11:34.403' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [Quantity], [UnitPrice], [UpdatedAt], [CreatedAt]) VALUES (N'fd1e85b6-d2a9-478d-971b-09c60ada127b', 2, 1, 1, CAST(29 AS Decimal(18, 0)), CAST(N'2020-09-04T08:55:28.747' AS DateTime), CAST(N'2020-09-04T08:55:28.503' AS DateTime))
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [Quantity], [UnitPrice], [UpdatedAt], [CreatedAt]) VALUES (N'fd1e85b6-d2a9-478d-971b-09c60ada127b', 2, 3, 2, CAST(19 AS Decimal(18, 0)), CAST(N'2020-09-04T08:55:28.767' AS DateTime), CAST(N'2020-09-04T08:55:28.503' AS DateTime))
GO
