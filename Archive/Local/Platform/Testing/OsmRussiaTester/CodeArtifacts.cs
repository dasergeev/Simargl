namespace OsmRussiaTester;

internal class CodeArtifacts
{
    //  Создание контекста сеанса работы с базой данных.
    //using OsmRussiaDatabaseContext database = new();

    //var queryResult = database.Ways.Where(x => x.Tags.ContainsKey("railway")).ToList();

    //var queryResult = database.Ways.Select(x => x.Tags.Keys.Where(key => key.Contains("railway")))
    //    .Take(10).ToList();

    //var queryResult = database.Ways.Select(x => x.Tags.Keys["railway"])
    //    .Take(10).ToList();

    //var queryResult = (from x in database.Ways
    //                  from t in x.Tags
    //                  where t.Key == "railway"
    //                  select t).Take(10).ToList();

    //var queryResult = database.Ways.Select(x => x.Tags.Cast<List<Dictionary<string, string>>>()).ToList();

    //var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

    //var location = geometryFactory.CreatePoint(new Coordinate(35.84625, 56.85858));

    //var result = database.Ways.Where(x => x.Linestring.Contains(location)).ToList();

    //var result = database.Nodes.FirstOrDefault(x => x.Geom.Distance(location) <= 10);

    //var result = database.Nodes .Where(x => x.Id == 54);

    //var a = result;

    //var result = database.Ways.Where(r => DbFunctionsExtensions.HasKeyValue(r.Tags, "railway", "=", "monorail")).ToList();

    //var result = database.Ways.Where(r => DbFunctionsExtensions.HasKey(r.Tags, "railway")).ToList();

    //var a = result;

    //var b = result[0].Tags.ToList();

    //var queryResult1 = database.Ways.Select(x => x.Tags.Cast<List<Dictionary<string, string>>>()).First();
    //	//.SelectMany(z => z.First().Where(w => w.ContainsKey("railway"))).ToList();

    //var b = queryResult1;

    //var a = queryResult.Count;
}
