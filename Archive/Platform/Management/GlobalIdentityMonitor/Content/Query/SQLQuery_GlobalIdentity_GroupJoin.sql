-- Region Parameters
DECLARE @p0 NVarChar(1000) = N'Неизвестный идентификатор'
-- EndRegion
SELECT [t2].[Name], [t2].[value] AS [ReceiptTime]
FROM (
    SELECT [t0].[Name], (
        SELECT MAX([t1].[ReceiptTime])
        FROM [IdentityMessages] AS [t1]
        WHERE [t0].[Id] = [t1].[GlobalIdentifierId]
        ) AS [value]
    FROM [GlobalIdentifiers] AS [t0]
    ) AS [t2]
WHERE [t2].[Name] <> @p0
ORDER BY [t2].[Name]
