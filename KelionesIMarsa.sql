USE [KelionesIMarsa]
GO
/****** Object:  Table [dbo].[Patiekalas]    Script Date: 4/11/2021 4:03:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patiekalas](
	[Pavadinimas] [varchar](50) NOT NULL,
	[Kalorijos] [int] NOT NULL,
	[Tipas] [varchar](50) NOT NULL,
	[id_Patiekalas] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Patiekalas] PRIMARY KEY CLUSTERED 
(
	[id_Patiekalas] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Veikla]    Script Date: 4/11/2021 4:03:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Veikla](
	[Tipas] [varchar](50) NOT NULL,
	[Trukme] [decimal](4, 2) NOT NULL,
	[Pavadinimas] [varchar](50) NOT NULL,
	[id_Veikla] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Veikla] PRIMARY KEY CLUSTERED 
(
	[id_Veikla] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
