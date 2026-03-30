/* Поиск */

SELECT * FROM [OrioleKmt].[dbo].[RawFrames]
WHERE IsAnalyzed = 'false' AND FilePath LIKE '%restoring%'

/* ===================================================== */ 

/* Удаление */

DELETE FROM [OrioleKmt].[dbo].[RawFrames]
WHERE Id = 27272

/* ===================================================== */

DELETE FROM [OrioleKmt].[dbo].[RawFrames]
WHERE IsAnalyzed = 'false' AND FilePath LIKE '%restoring%'

/* Очистка таблицы */
TRUNCATE TABLE [OrioleKmt].[dbo].[AnalyzedFrames];
/* ===================================================== */

/* Выборка */

SELECT COUNT(*) FROM [OrioleKmt].[dbo].[RawFrames]
WHERE IsAnalyzed = 'false'

/* ===================================================== */
/* Сброс значения */
 UPDATE [OrioleKmt].[dbo].[RawFrames] SET [IsAnalyzed] = 0


 /****** Скрипт для команды SelectTopNRows из среды SSMS  ******/
SELECT [Id]
      ,[FilePath]
      ,[IsPocessed]
      ,[Latitude]
      ,[Longitude]
      ,[CurvatureMin]
      ,[CurvatureMax]
      ,[CurvatureAverage]
      ,[DurationParking]
      ,[DurationTraction]
      ,[DurationBraking]
      ,[DurationRunout]
      ,[TractionEffortMin]
      ,[TractionEffortMax]
      ,[TractionEffortSum]
      ,[TractionEffortSquaredSum]
      ,[TractionEffortCount]
  FROM [OrioleKmt].[dbo].[AnalyzedFrames]
  WHERE IsPocessed = 1