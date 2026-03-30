using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

namespace Apeiron.Platform.OsmLibrary;

/// <summary>
/// Представляет класс для работы с пространственным типом данных.
/// </summary>
public static class GeometryExtention
{

    /// <summary>
    /// Преобразует координаты из epsg4326 в epsg3857 систему координат.
    /// Для преобразования координат используется библиотека ProjNet.
    /// </summary>
    /// <param name="lon">Долгота.</param>
    /// <param name="lat">Широта.</param>
    /// <exception cref="ArgumentOutOfRangeException">Значение долготы/широты выходит за границы допустимых значений.</exception>
    /// <returns>Координаты в формате epsg3857.</returns>
    public static (double x, double y) CoordinateEpsg4326toEpsg3857Converter(double lon, double lat)
    {
        if (lon < double.MinValue || lon > double.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(lon), "Значение долготы выходит за границы допустимых значений.");

        if (lat < double.MinValue || lat > double.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(lat), "Значение широты выходит за границы допустимых значений.");

        // Создание фабрики для преобразования координат из 4326 в 3857.
        ProjectedCoordinateSystem epsg3857 = ProjectedCoordinateSystem.WebMercator;
        GeographicCoordinateSystem epsg4326 = GeographicCoordinateSystem.WGS84;

        CoordinateTransformationFactory coordinateTransformationFactory = new();
        ICoordinateTransformation coordinateTransformation = coordinateTransformationFactory.CreateFromCoordinateSystems(epsg4326, epsg3857);

        return coordinateTransformation.MathTransform.Transform(lon, lat);
    }

}