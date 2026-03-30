---- Создаём таблицу на основе данных полной таблицы point.
--CREATE TABLE planet_osm_point_railway AS (SELECT * FROM planet_osm_point WHERE railway <> '');
---- Создаём индекс.
--CREATE INDEX planet_osm_point_railway_way_idx ON planet_osm_point_railway USING gist(way) WITH (fillfactor = 100);


---- Создаём таблицу на основе данных полной таблицы line.
--CREATE TABLE planet_osm_line_railway AS (SELECT * FROM planet_osm_line WHERE railway <> '');
---- Создаём индекс.
--CREATE INDEX planet_osm_line_railway_way_idx ON planet_osm_line_railway USING gist(way) WITH (fillfactor = 100);


---- Создаём таблицу на основе данных полной таблицы polygon.
--CREATE TABLE planet_osm_polygon_railway AS (SELECT * FROM planet_osm_polygon WHERE railway <> '');
---- Создаём индекс.
--CREATE INDEX planet_osm_polygone_railway_way_idx ON planet_osm_polygon_railway USING gist(way) WITH (fillfactor = 100);


---- Создаём таблицу на основе данных полной таблицы roads.
--CREATE TABLE planet_osm_roads_railway AS (SELECT * FROM planet_osm_roads WHERE railway <> '');
---- Создаём индекс.
--CREATE INDEX planet_osm_roads_railway_way_idx ON planet_osm_roads_railway USING gist(way) WITH (fillfactor = 100);