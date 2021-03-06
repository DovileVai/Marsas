USE [KelionesIMarsa]
GO
/****** Object:  Table [dbo].[Naudotojas]    Script Date: 5/3/2021 1:44:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Naudotojas](
	[Vardas] [varchar](50) NOT NULL,
	[Pavarde] [varchar](50) NOT NULL,
	[ElPastas] [varchar](50) NOT NULL,
	[Slaptazodis] [varchar](50) NOT NULL,
	[id_Naudotojas] [int] IDENTITY(1,1) NOT NULL,
	[ArAdminas] [bit] NOT NULL,
 CONSTRAINT [PK_Naudotojas] PRIMARY KEY CLUSTERED 
(
	[id_Naudotojas] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patiekalas]    Script Date: 5/3/2021 1:44:31 PM ******/
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
/****** Object:  Table [dbo].[Veikla]    Script Date: 5/3/2021 1:44:31 PM ******/
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
