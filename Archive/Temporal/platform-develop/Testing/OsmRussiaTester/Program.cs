using Apeiron.Platform.Databases.Osm2RussiaDatabase;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Apeiron.Platform.OsmLibrary;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки

Console.WriteLine("Работа с базой!\n");


using Osm2RussiaContext database = new();


// Преобразование координат.
//var epsg3857Coordinate = GeometryExtention.CoordinateEpsg4326toEpsg3857Converter(35.84608, 56.85884);
var (x, y) = GeometryExtention.CoordinateEpsg4326toEpsg3857Converter(37.19427, 55.83952);
// Создание точки.
var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 3857);
var location = geometryFactory.CreatePoint(new Coordinate(x, y));

//GeolocationPoint geolocationPoint1 = GeolocationPoint.CreatePoint(55.83952, 37.19427);
//GeolocationPoint geolocationPoint2 = GeolocationPoint.CreatePoint(55.83952, 37.19427);

//var a = geolocationPoint1.Equals(geolocationPoint2);

//var b = a;

Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("Выборка всех полигонов/областей в которые входит данная точка с координатами: 56.85884, 35.84608\n");
Console.ResetColor();

var queryPolygone = await database.PlanetOsmPolygons.Where(x => x.Way.Contains(location)).OrderBy(x => x.Way.Area).ToListAsync();

foreach (var polygone in queryPolygone)
{
    if (!string.IsNullOrEmpty(polygone.Name))
        Console.WriteLine($"{polygone.Name}\r");
}

//-----------------------------------------------------------------------------------------------------------------------

//var result = database.PlanetOsmRoads.Where(x => x.Railway != "").Count();
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("\n\nПолучение всех железнодорожных путей расстояние до которых менее 50 единиц от точки 56.85884, 35.84608\n");
Console.ResetColor();
var queryRailwayRoads = await database.PlanetOsmRoadsRailways.Where(x => x.Way.Distance(location) <= 50).ToListAsync();


foreach (var rail in queryRailwayRoads)
{
    if (!string.IsNullOrEmpty(rail.Name))
        Console.WriteLine($"Название участка пути: {rail.Name}, {rail.Way.Length}\r");
    else
        Console.WriteLine($"Длинна отрезка пути: {rail.Way.Length}");
}

//-----------------------------------------------------------------------------------------------------------------------

Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("\n\nБлижайшая железнодорожная станция к точке с координатами: 55.83952, 37.19427\n");
Console.ResetColor();

//Синхронный вариант
//var queryResultStation = OsmOperator.GetStationWithMinimumDistance(37.19427, 55.83952);
//Ассинхронный вариант
var queryResultStation = await OsmOperator.GetStationWithMinimumDistanceAsync(55.83952, 37.19427);

Console.WriteLine($"Название станции: {queryResultStation?.Name}\r");

//-----------------------------------------------------------------------------------------------------------------------

Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("\n\nПолучение всех точек отрезка пути ближайшего к точке с координатами: 55.83952, 37.19427\n");
Console.ResetColor();

//Синхронный вариант.
//var queryResultListOfPoints = OsmOperator.GetAllPointFromNearestRailway(37.19427, 55.83952);
//Ассинхронный вариант.
var queryResultListOfPoints = await OsmOperator.GetAllPointFromNearestRailwayAsync(55.83952, 37.19427);

int i = 0;
foreach (var point in queryResultListOfPoints)
{
    Console.WriteLine($"{++i,2} Точка: {point.Y}, {point.X}\r");
}

//-----------------------------------------------------------------------------------------------------------------------

Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("\n\nПолучение всех железнодорожных отрезков в прямоугольнике очерченым координатами: 55.92189, 37.02908, 55.76363, 37.25361\n");
Console.ResetColor();

//Синхронный вариант.
//var queryRails = OsmOperator.GetRailFromEnvelope(37.02908, 55.92189, 37.25361, 55.76363);
//Ассинхронный варинат.
var queryRails = await OsmOperator.GetRailFromEnvelopeAsync(55.92189, 37.02908, 55.76363, 37.25361);

int j = 0;

foreach (var rail in queryRails)
{
    Console.WriteLine($"{++j,2}) Название: {rail.Name, 22}\t Сервис:{rail.Service, 10}\t Длинна: {rail.Way.Length}\r");
}


//-----------------------------------------------------------------------------------------------------------------------

Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("\n\nПолучение всех железнодорожных отрезков рядом: 55.83952, 37.19427\n");
Console.ResetColor();

var queryRailsRoad = OsmOperator.GetListRailwayNearPoint(55.83952, 37.19427);

int g = 0;

foreach (var rail in queryRailsRoad)
{
    Console.WriteLine($"{++g,2}), Id:,{rail.OsmId,10}, Название: {rail.Name,24},\t Сервис:{rail.Service,12},\t Длинна: {rail.Way.Length},\r");
}
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
