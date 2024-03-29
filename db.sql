USE [task]
GO
/****** Object:  Table [dbo].[divizia]    Script Date: 1. 6. 2021 16:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[divizia](
	[id] [bigint] NOT NULL,
	[nazov] [varchar](50) NOT NULL,
	[kod] [varchar](50) NOT NULL,
	[veduciID] [bigint] NOT NULL,
	[firmaID] [bigint] NOT NULL,
 CONSTRAINT [PK_divizia] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[firma]    Script Date: 1. 6. 2021 16:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[firma](
	[id] [bigint] NOT NULL,
	[nazov] [varchar](50) NOT NULL,
	[kod] [varchar](50) NOT NULL,
	[veduciID] [bigint] NOT NULL,
 CONSTRAINT [PK_firma] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[oddelenie]    Script Date: 1. 6. 2021 16:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[oddelenie](
	[id] [bigint] NOT NULL,
	[nazov] [varchar](50) NOT NULL,
	[kod] [varchar](50) NOT NULL,
	[veduciID] [bigint] NOT NULL,
	[projektID] [bigint] NOT NULL,
 CONSTRAINT [PK_oddelenie] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[projekt]    Script Date: 1. 6. 2021 16:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[projekt](
	[id] [bigint] NOT NULL,
	[nazov] [varchar](50) NOT NULL,
	[kod] [varchar](50) NOT NULL,
	[veduciID] [bigint] NOT NULL,
	[diviziaID] [bigint] NOT NULL,
 CONSTRAINT [PK_projekt] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[zamestnanec]    Script Date: 1. 6. 2021 16:43:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zamestnanec](
	[id] [bigint] NOT NULL,
	[titul] [varchar](15) NULL,
	[meno] [varchar](50) NOT NULL,
	[priezvisko] [varchar](50) NOT NULL,
	[telefon] [nvarchar](15) NOT NULL,
	[email] [varchar](50) NOT NULL,
 CONSTRAINT [PK_zamestnanec] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[divizia]  WITH CHECK ADD  CONSTRAINT [veduci_divizie] FOREIGN KEY([veduciID])
REFERENCES [dbo].[zamestnanec] ([id])
GO
ALTER TABLE [dbo].[divizia] CHECK CONSTRAINT [veduci_divizie]
GO
ALTER TABLE [dbo].[divizia]  WITH CHECK ADD  CONSTRAINT [vo_firme] FOREIGN KEY([firmaID])
REFERENCES [dbo].[firma] ([id])
GO
ALTER TABLE [dbo].[divizia] CHECK CONSTRAINT [vo_firme]
GO
ALTER TABLE [dbo].[firma]  WITH CHECK ADD  CONSTRAINT [veduci_firmy] FOREIGN KEY([veduciID])
REFERENCES [dbo].[zamestnanec] ([id])
GO
ALTER TABLE [dbo].[firma] CHECK CONSTRAINT [veduci_firmy]
GO
ALTER TABLE [dbo].[oddelenie]  WITH CHECK ADD  CONSTRAINT [projekty] FOREIGN KEY([projektID])
REFERENCES [dbo].[projekt] ([id])
GO
ALTER TABLE [dbo].[oddelenie] CHECK CONSTRAINT [projekty]
GO
ALTER TABLE [dbo].[oddelenie]  WITH CHECK ADD  CONSTRAINT [veduci_oddelenia] FOREIGN KEY([veduciID])
REFERENCES [dbo].[zamestnanec] ([id])
GO
ALTER TABLE [dbo].[oddelenie] CHECK CONSTRAINT [veduci_oddelenia]
GO
ALTER TABLE [dbo].[projekt]  WITH CHECK ADD  CONSTRAINT [v_divizii] FOREIGN KEY([diviziaID])
REFERENCES [dbo].[divizia] ([id])
GO
ALTER TABLE [dbo].[projekt] CHECK CONSTRAINT [v_divizii]
GO
ALTER TABLE [dbo].[projekt]  WITH CHECK ADD  CONSTRAINT [veduci_projektu] FOREIGN KEY([veduciID])
REFERENCES [dbo].[zamestnanec] ([id])
GO
ALTER TABLE [dbo].[projekt] CHECK CONSTRAINT [veduci_projektu]
GO
