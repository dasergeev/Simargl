--SELECT name, 
--railway, 
--ST_AsText(ST_Transform(way, 4326)) AS StationCoordinates, 
--ROUND(ST_DistanceSphere(
--    ST_Transform(way, 4326),
--    ST_GeomFromText('POINT(37.19427 55.83952)', 4326))) AS Distance_m
--FROM planet_osm_point_railway
--WHERE railway IN ('station', 'halt', 'stop', 'stop_area', 'stop_position','platform')
--ORDER BY Distance_m
--LIMIT 1;


--SELECT *
--FROM planet_osm_point_railway
--WHERE railway IN ('station', 'halt', 'stop', 'stop_area', 'stop_position','platform')
--ORDER BY ROUND(ST_DistanceSphere(
--    ST_Transform(way, 4326),
--    ST_GeomFromText('POINT(37.19427 55.83952)', 4326)))
--LIMIT 1;