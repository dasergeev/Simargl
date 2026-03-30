using Apeiron.Platform.Databases.Osm2RussiaDatabase;
using Apeiron.Platform.OsmLibrary.Enums;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Npgsql;

namespace Apeiron.Platform.OsmLibrary;

/// <summary>
/// Представляет класс с методами работы с БД OSM.
/// </summary>
public static class OsmOperator
{
    /// <summary>
    /// Возвращает станцию на минимальном удалении от заданной точки.
    /// </summary>
    /// <param name="lon">Долгота.</param>
    /// <param name="lat">Широта.</param>
    /// <exception cref="ArgumentOutOfRangeException">Значение долготы/широты выходит за границы допустимых значений.</exception>
    /// <returns>Возвращает сущность станции/точки в виде списка.</returns>
    public static PlanetOsmPointRailway? GetStationWithMinimumDistance(double lat, double lon)
    {
        // Создание точки с координатами.
        GeolocationPoint geolocationPoint = GeolocationPoint.CreatePoint(lat, lon);

        // Создание контекста работы с БД.
        using Osm2RussiaContext database = new();

        // Создание геометрической точки.
        GeometryFactory geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        Point locationPoint = geometryFactory.CreatePoint(new Coordinate(geolocationPoint.Longitude, geolocationPoint.Latitude));
        
        // Создание параметра запроса.
        NpgsqlParameter param = new("@point", locationPoint);

        //EF.Functions.DistanceKnn(geom1, geom2) возможно переделать в будующем на вызов встроенной функции.

        PlanetOsmPointRailway? queryResult = database.PlanetOsmPointRailways.FromSqlRaw("SELECT * FROM planet_osm_point_railway " +
            "WHERE railway IN('station', 'halt', 'stop', 'stop_area', 'stop_position', 'platform') " +
            "ORDER BY ROUND(ST_DistanceSphere(ST_Transform(way, 4326), ST_GeomFromWKB(@point))) LIMIT 1", param)
            .AsNoTracking().FirstOrDefault();

        return queryResult;
    }

    /// <summary>
    /// Возвращает станцию на минимальном удалении от заданной точки.
    /// Ассинхронная функция.
    /// </summary>
    /// <param name="lon">Долгота.</param>
    /// <param name="lat">Широта.</param>
    /// <exception cref="ArgumentOutOfRangeException">Значение долготы/широты выходит за границы допустимых значений.</exception>
    /// <returns>Возвращает сущность станции/точки в виде списка.</returns>
    public static async Task<PlanetOsmPointRailway?> GetStationWithMinimumDistanceAsync(double lat, double lon)
    {
        // Создание точки с координатами.
        GeolocationPoint geolocationPoint = GeolocationPoint.CreatePoint(lat, lon);

        // Создание контекста работы с БД.
        using Osm2RussiaContext database = new();

        // Создание точки.
        GeometryFactory geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        Point locationPoint = geometryFactory.CreatePoint(new Coordinate(geolocationPoint.Longitude, geolocationPoint.Latitude));

        // Создание параметра запроса.
        NpgsqlParameter param = new("@point", locationPoint);

        //EF.Functions.DistanceKnn(geom1, geom2) возможно переделать в будующем на вызов встроенной функции.

        PlanetOsmPointRailway? queryResult = await database.PlanetOsmPointRailways.FromSqlRaw("SELECT * FROM planet_osm_point_railway " +
            "WHERE railway IN('station', 'halt', 'stop', 'stop_area', 'stop_position', 'platform') " +
            "ORDER BY ROUND(ST_DistanceSphere(ST_Transform(way, 4326), ST_GeomFromWKB(@point))) LIMIT 1", param)
            .AsNoTracking().FirstOrDefaultAsync();

        return queryResult;
    }


    /// <summary>
    /// Возвращает коллекцию точек в формате Point(lon, lat) из которых состоит отрезок железнодорожного пути ближайшего к точке заданной координатами.
    /// </summary>
    /// <param name="lon">Долгота.</param>
    /// <param name="lat">Широта.</param>
    /// <exception cref="ArgumentOutOfRangeException">Значение долготы/широты выходит за границы допустимых значений.</exception>
    /// <exception cref="FormatException">Ошибка формата данных.</exception>
    /// <returns>Коллекция точек из которых состоит линия пути.</returns>
    public static IEnumerable<Point> GetAllPointFromNearestRailway(double lat, double lon)
    {
        // Создание точки с координатами.
        GeolocationPoint geolocationPoint = GeolocationPoint.CreatePoint(lat, lon);

        // Создание точки.
        GeometryFactory geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        Point locationPoint = geometryFactory.CreatePoint(new Coordinate(geolocationPoint.Longitude, geolocationPoint.Latitude));

        List<Point> queryResult = new();

        // Создание контекста работы с БД для получения строки подлючения.
        using Osm2RussiaContext database = new();
        // Создание соединения.
        using NpgsqlConnection connection = new(database.Database.GetConnectionString());
        {
            // Открытие соединения.
            connection.Open();

            // Формирование команды.
            using NpgsqlCommand cmd = new("SELECT ST_Transform((dp).geom, 4326) " +
                "FROM(SELECT ST_DumpPoints" +
                "((SELECT way FROM public.planet_osm_roads WHERE railway IN('rail') " +
                "ORDER BY way<-> ST_Transform(ST_GeomFromWKB(@point), 3857) " +
                "LIMIT 1)) AS dp) " +
                "as foo", connection)
            {
                Parameters =
                {
                    new("point", locationPoint)
                }
            };

            // Выполнение команды.
            NpgsqlDataReader databasrReader = cmd.ExecuteReader();

            //Чтение данных.
            while (databasrReader.Read())
            {
                try
                {
                    queryResult.Add((Point)databasrReader[0]);
                }
                catch (InvalidCastException ex)
                {
                    throw new FormatException("Ошибка формата данных полученных из БД.", ex);
                }
            }

            connection.Close();
        }

        return queryResult;
    }

    /// <summary>
    /// Возвращает коллекцию точек в формате Point(lon, lat) из которых состоит отрезок железнодорожного пути ближайшего к точке заданной координатами.
    /// Ассинхронная функция.
    /// </summary>
    /// <param name="lon">Долгота.</param>
    /// <param name="lat">Широта.</param>
    /// <exception cref="ArgumentOutOfRangeException">Значение долготы/широты выходит за границы допустимых значений.</exception>
    /// <exception cref="FormatException">Ошибка формата данных.</exception>
    /// <returns>Коллекция точек из которых состоит линия пути.</returns>
    public static async Task<IEnumerable<Point>> GetAllPointFromNearestRailwayAsync(double lat, double lon)
    {
        // Создание точки с координатами.
        GeolocationPoint geolocationPoint = GeolocationPoint.CreatePoint(lat, lon);

        // Создание точки.
        GeometryFactory geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        Point locationPoint = geometryFactory.CreatePoint(new Coordinate(geolocationPoint.Longitude, geolocationPoint.Latitude));

        List<Point> queryResult = new();

        // Создание контекста работы с БД для получения строки подлючения.
        using Osm2RussiaContext database = new();
        // Создание соединения.
        using NpgsqlConnection connection = new(database.Database.GetConnectionString());
        {
            // Открытие соединения.
            await connection.OpenAsync();

            // Формирование команды.
            using NpgsqlCommand cmd = new("SELECT ST_Transform((dp).geom, 4326) " +
                "FROM(SELECT ST_DumpPoints" +
                "((SELECT way FROM public.planet_osm_roads WHERE railway IN('rail') " +
                "ORDER BY way<-> ST_Transform(ST_GeomFromWKB(@point), 3857) " +
                "LIMIT 1)) AS dp) " +
                "as foo", connection)
            {
                Parameters =
                {
                    new("point", locationPoint)
                }
            };

            // Выполнение команды.
            NpgsqlDataReader databasrReader = await cmd.ExecuteReaderAsync();

            //Чтение данных.
            while (await databasrReader.ReadAsync())
            {
                try
                {
                    queryResult.Add((Point)databasrReader[0]);
                }
                catch (InvalidCastException ex)
                {
                    throw new FormatException("Ошибка формата данных полученных из БД.", ex);
                }
            }

            await connection.CloseAsync();
        }

        return queryResult;
    }


    /// <summary>
    /// Находит и возвращает все отрезки участвов железных дорог попавших в очерченный прямоугольник, заданный координатами.
    /// </summary>
    /// <param name="lon1">Долгота первой точки.</param>
    /// <param name="lat1">Широта первой точки.</param>
    /// <param name="lon2">Долгота второй точки.</param>
    /// <param name="lat2">Широта второй точки.</param>
    /// <exception cref="ArgumentOutOfRangeException">Значение долготы/широты выходит за границы допустимых значений.</exception>
    ///<returns>Список отрезков участков железных дорог.</returns>
    public static List<PlanetOsmRoadsRailway>? GetRailFromEnvelope(double lat1, double lon1, double lat2, double lon2)
    {
        // Создание точки с координатами.
        GeolocationPoint geolocationPoint1 = GeolocationPoint.CreatePoint(lat1, lon1);
        GeolocationPoint geolocationPoint2 = GeolocationPoint.CreatePoint(lat2, lon2);

        // Создание контекста работы с БД.
        using Osm2RussiaContext database = new();

        // Создание точки.
        GeometryFactory geometryFactory1 = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        Point locationPoint1 = geometryFactory1.CreatePoint(new Coordinate(geolocationPoint1.Longitude, geolocationPoint1.Latitude));
        // Создание точки.
        GeometryFactory geometryFactory2 = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        Point locationPoint2 = geometryFactory2.CreatePoint(new Coordinate(geolocationPoint2.Longitude, geolocationPoint2.Latitude));

        // Создание параметра запроса.
        NpgsqlParameter param1 = new("@point1", locationPoint1);
        NpgsqlParameter param2 = new("@point2", locationPoint2);

        // Получение списка объектов в очерченом прямоугольнике из базы.
        List<PlanetOsmRoadsRailway>? planetOsmRoadsRailways = database.PlanetOsmRoadsRailways.FromSqlRaw("SELECT * FROM planet_osm_roads_railway " +
            "WHERE railway IN ('rail') AND planet_osm_roads_railway.way && " +
            "ST_Transform(ST_MakeEnvelope(" +
            "ST_X(ST_GeomFromWKB(@point1))," +
            "ST_Y(ST_GeomFromWKB(@point1))," +
            "ST_X(ST_GeomFromWKB(@point2))," +
            "ST_Y(ST_GeomFromWKB(@point2)), 4326), 3857);", param1, param2)
            .AsNoTracking().ToList();

        return planetOsmRoadsRailways;
    }

    /// <summary>
    /// Находит и возвращает все отрезки участвов железных дорог попавших в очерченный прямоугольник, заданный координатами.
    /// </summary>
    /// <param name="lon1">Долгота первой точки.</param>
    /// <param name="lat1">Широта первой точки.</param>
    /// <param name="lon2">Долгота второй точки.</param>
    /// <param name="lat2">Широта второй точки.</param>
    /// <exception cref="ArgumentOutOfRangeException">Значение долготы/широты выходит за границы допустимых значений.</exception>
    ///<returns>Список отрезков участков железных дорог.</returns>
    public static async Task<List<PlanetOsmRoadsRailway>?> GetRailFromEnvelopeAsync(double lat1, double lon1, double lat2, double lon2)
    {
        // Создание точки с координатами.
        GeolocationPoint geolocationPoint1 = GeolocationPoint.CreatePoint(lat1, lon1);
        GeolocationPoint geolocationPoint2 = GeolocationPoint.CreatePoint(lat2, lon2);

        // Создание контекста работы с БД.
        using Osm2RussiaContext database = new();

        // Создание точки.
        GeometryFactory geometryFactory1 = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        Point locationPoint1 = geometryFactory1.CreatePoint(new Coordinate(geolocationPoint1.Longitude, geolocationPoint1.Latitude));
        // Создание точки.
        GeometryFactory geometryFactory2 = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        Point locationPoint2 = geometryFactory2.CreatePoint(new Coordinate(geolocationPoint2.Longitude, geolocationPoint2.Latitude));

        // Создание параметра запроса.
        NpgsqlParameter param1 = new("@point1", locationPoint1);
        NpgsqlParameter param2 = new("@point2", locationPoint2);

        // Получение списка объектов в очерченом прямоугольнике из базы.
        List<PlanetOsmRoadsRailway>? planetOsmRoadsRailways = await database.PlanetOsmRoadsRailways.FromSqlRaw("SELECT * FROM planet_osm_roads_railway " +
            "WHERE railway IN ('rail') AND planet_osm_roads_railway.way && " +
            "ST_Transform(ST_MakeEnvelope(" +
            "ST_X(ST_GeomFromWKB(@point1))," +
            "ST_Y(ST_GeomFromWKB(@point1))," +
            "ST_X(ST_GeomFromWKB(@point2))," +
            "ST_Y(ST_GeomFromWKB(@point2)), 4326), 3857);", param1, param2)
            .AsNoTracking().ToListAsync();

        return planetOsmRoadsRailways;
    }


    /// <summary>
    /// Выполняет поиск всех железнодорожных отрезков продолжающих или предшедствующих отрезку на наименьшем расстоянии от которого находиться точка с координатами.
    /// </summary>
    /// <remark>
    /// Эксперементальныя фукнция, возможно требует дороботки.
    /// </remark>
    /// <param name="lon">Долгота.</param>
    /// <param name="lat">Широта.</param>
    /// <returns>Список отрезков железных дорог.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Значение долготы/широты выходит за границы допустимых значений.</exception>
    ///<returns>Список отрезков участков железных дорог.</returns>
    public static List<PlanetOsmRoadsRailway> GetListRailwayNearPoint(double lat, double lon)
    {
        // Создание точки с координатами.
        GeolocationPoint geolocationPoint = GeolocationPoint.CreatePoint(lat, lon);

        // Создание контекста работы с БД.
        using Osm2RussiaContext database = new();

        // Создание точки.
        GeometryFactory geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        Point locationPoint = geometryFactory.CreatePoint(new Coordinate(geolocationPoint.Longitude, geolocationPoint.Latitude));
        // Создание параметра запроса.
        NpgsqlParameter param = new("@point", locationPoint);

        // Нахождение ближайшего отрезка ж/д пути к заданной точке.
        var queryResult = database.PlanetOsmRoadsRailways.FromSqlRaw(" SELECT * " +
            "FROM planet_osm_roads " +
            "WHERE railway IN('rail') " +
            "ORDER BY way <-> ST_Transform(ST_GeomFromWKB(@point), 3857)LIMIT 1", param)
            .AsNoTracking().FirstOrDefault();

        // Список с результатами поиска.
        List<PlanetOsmRoadsRailway> queryResultList = new();

        // Нахождение ближайшего соседа к пути.
        if (queryResult is not null && queryResult.Way is not null)
        {
            queryResultList = FindPreviousNextRailway(database, queryResultList, queryResult, DirectionRailwayLine.Next);
            queryResultList = FindPreviousNextRailway(database, queryResultList, queryResult, DirectionRailwayLine.Previous);
        }


        // Рекурсивно находит отрезки ж/д путей.
        static List<PlanetOsmRoadsRailway> FindPreviousNextRailway(Osm2RussiaContext database, List<PlanetOsmRoadsRailway> planetOsmRoadsRailwaysList, PlanetOsmRoadsRailway planetOsmRoadsRailway, DirectionRailwayLine direction)
        {
            if (planetOsmRoadsRailway.Way is not null)
            {
                switch (direction)
                {
                    case DirectionRailwayLine.Next:
                        {
                            var nearestNeighborRailway = database.PlanetOsmRoadsRailways
                                .AsNoTracking()
                                .FirstOrDefault(x => x.Way != null && x.Service != "siding" && x.Way.StartPoint.EqualsTopologically(planetOsmRoadsRailway.Way.EndPoint));

                            if (nearestNeighborRailway is not null)
                            {
                                planetOsmRoadsRailwaysList.Add(nearestNeighborRailway);

                                // Рекурсивно ищем следующий отрезок.
                                FindPreviousNextRailway(database, planetOsmRoadsRailwaysList, nearestNeighborRailway, DirectionRailwayLine.Next);
                            }
                            else
                            {
                                return planetOsmRoadsRailwaysList;
                            }

                            break;
                        }
                    case DirectionRailwayLine.Previous:
                        {
                            var nearestNeighborRailway = database.PlanetOsmRoadsRailways
                                .AsNoTracking()
                                .FirstOrDefault(x => x.Way != null && x.Service != "siding" && x.Way.EndPoint.EqualsTopologically(planetOsmRoadsRailway.Way.StartPoint) && x.Service != "siding");

                            if (nearestNeighborRailway is not null)
                            {
                                planetOsmRoadsRailwaysList.Add(nearestNeighborRailway);

                                // Рекурсивно ищем следующий отрезок.
                                FindPreviousNextRailway(database, planetOsmRoadsRailwaysList, nearestNeighborRailway, DirectionRailwayLine.Previous);
                            }
                            else
                            {
                                return planetOsmRoadsRailwaysList;
                            }

                            break;
                        }
                    default:
                        break;
                }
            }

            return planetOsmRoadsRailwaysList;
        }


        return queryResultList;
    }
}