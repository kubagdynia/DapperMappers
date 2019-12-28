IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Books' and xtype='U')
	CREATE TABLE [dbo].[Books](
		[InternalId] bigint IDENTITY(1,1) NOT NULL,
		[Id] char(36) NOT NULL,
		[Title] nvarchar(200) NOT NULL,
		[PageCount] int NULL,
		[Isbn] varchar(15) NULL,
		[DateOfPublication] datetime NOT NULL,
		[Authors] xml NULL,
		[TableOfContents] xml NULL,
		[ShortDescription] nvarchar(500) NULL,
		[Description] xml NULL,
		[Publisher] nvarchar(200) NULL,
		[Url] nvarchar(200) NULL
		CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED
		(
			[InternalId] ASC
		),
		CONSTRAINT [IX_Books_ObjectId] UNIQUE NONCLUSTERED
		(
			[Id] ASC
		)
	)
GO
	