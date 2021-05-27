USE [ExamCorrection]
GO

/****** Object:  Table [dbo].[Courses]    Script Date: 27.05.2021 14:49:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Courses](
	[id] INTEGER IDENTITY(1, 1) PRIMARY KEY NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[description] [nvarchar](max) NULL,
	[ownerId] [nvarchar](max) NOT NULL)

GO



USE [ExamCorrection]
GO

/****** Object:  Table [dbo].[Exams]    Script Date: 27.05.2021 14:50:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Exams](
	[id] INTEGER IDENTITY(1, 1) PRIMARY KEY NOT NULL,
	[courseId] INTEGER NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[description] [nvarchar](max) NULL,
	[date] [datetime] NULL,
	[scoreThreshold] [decimal](18, 2) NOT NULL)
GO

ALTER TABLE [dbo].[Exams]  WITH CHECK ADD  CONSTRAINT [FK_Exams_Course] FOREIGN KEY([courseId])
REFERENCES [dbo].[Courses] ([id])
GO

ALTER TABLE [dbo].[Exams] CHECK CONSTRAINT [FK_Exams_Course]
GO



USE [ExamCorrection]
GO

/****** Object:  Table [dbo].[ExamTasks]    Script Date: 27.05.2021 14:50:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ExamTasks](
	[id] INTEGER IDENTITY(1, 1) PRIMARY KEY NOT NULL,
	[examId] INTEGER NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[solution] [nvarchar](max) NOT NULL)
GO

ALTER TABLE [dbo].[ExamTasks]  WITH CHECK ADD  CONSTRAINT [FK_ExamTasks_Exam] FOREIGN KEY([examId])
REFERENCES [dbo].[Exams] ([id])
GO

ALTER TABLE [dbo].[ExamTasks] CHECK CONSTRAINT [FK_ExamTasks_Exam]
GO


USE [ExamCorrection]
GO

/****** Object:  Table [dbo].[StudentSolutions]    Script Date: 27.05.2021 14:51:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StudentSolutions](
	[id] INTEGER IDENTITY(1, 1) PRIMARY KEY NOT NULL,
	[taskId] INTEGER NOT NULL,
	[studentId] [nvarchar](max) NOT NULL,
	[answer] [nvarchar](max) NOT NULL)
GO

ALTER TABLE [dbo].[StudentSolutions]  WITH CHECK ADD  CONSTRAINT [FK_StudentSolutions_ExamTask] FOREIGN KEY([taskId])
REFERENCES [dbo].[ExamTasks] ([id])
GO

ALTER TABLE [dbo].[StudentSolutions] CHECK CONSTRAINT [FK_StudentSolutions_ExamTask]
GO



