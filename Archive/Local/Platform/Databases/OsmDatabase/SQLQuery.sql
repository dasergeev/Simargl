/* Очистка таблицы и сброс значения первичного ключа */

USE [Osm]
GO

DELETE FROM [dbo].[OsmFiles]
GO

DBCC CHECKIDENT ('OsmFiles', RESEED, 0);  
GO

/* == Очистка таблицы более быстрая чем через Delete === */
GO  
SELECT COUNT(*)
FROM [Osm].[dbo].[OsmNodes]

GO  
TRUNCATE TABLE OsmNodes
GO
   
SELECT COUNT(*)
FROM [Osm].[dbo].[OsmNodes]
GO

/* ===================================================== */

/* ======= Получение количества записей в таблице ====== */
USE [Osm]
GO

SELECT COUNT(*)
  FROM [dbo].[OsmFiles]

GO
/* ===================================================== */

/* = Получение последних 1000 записей по полю FilePath = */
SELECT TOP (1000) [Id]
      ,[FilePath]
      ,[IsAnalyzed]
      ,[IsEmpty]
      ,[IsNodesLoad]
      ,[IsTagsLoad]
  FROM [Osm].[dbo].[OsmFiles]
  ORDER BY FilePath DESC
/* ===================================================== */

/* Сброс значения */
 UPDATE [Osm].[dbo].[OsmFiles] SET [IsNodesLoad] = 0

 /****** Скрипт для команды SelectTopNRows из среды SSMS  ******/
SELECT TOP (1000) [Id]
      ,[FilePath]
      ,[IsAnalyzed]
      ,[IsEmpty]
      ,[IsNodesLoad]
      ,[IsNodeTagsLoad]
      ,[IsWayTagsLoad]
      ,[IsWaysLoad]
  FROM [Osm].[dbo].[OsmFiles]
  WHERE IsNodesLoad = 'true'


  /****** Скрипт для команды SelectTopNRows из среды SSMS  ******/
SELECT TOP (1000) [Id]
      ,[Address]
      ,[GlobalIdentifierId]
      ,[PacketIdentifier]
      ,[Port]
      ,[Source]
      ,[Time]
      ,[Version]
      ,[ReceiptTime]
  FROM [GlobalIdentity].[dbo].[IdentityMessages]
  WHERE GlobalIdentifierId = '1' AND Time > '2022-04-05 14:00' AND Time < '2022-04-06 09:32'


  /****** Скрипт для команды SelectTopNRows из среды SSMS  ******/
SELECT TOP (1000) [Id]
      ,[Address]
      ,[GlobalIdentifierId]
      ,[PacketIdentifier]
      ,[Port]
      ,[Source]
      ,[Time]
      ,[Version]
      ,[ReceiptTime]
  FROM [GlobalIdentity].[dbo].[IdentityMessages]
  WHERE GlobalIdentifierId = '1' AND Time > '2022-02-01 00:00' AND Time < '2022-02-28 23:59'
  ORDER BY Time DESC


  /****** Скрипт для команды SelectTopNRows из среды SSMS  ******/
DELETE
  FROM [GlobalIdentity].[dbo].[IdentityMessages]
  WHERE GlobalIdentifierId = '1' AND Time > '2022-04-01 00:00' AND Time < '2022-04-30 23:59'
  /*ORDER BY Time DESC*/