using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Apeiron.Platform.Databases.Osm2RussiaDatabase;

public partial class Osm2RussiaContext : DbContext
{
    public Osm2RussiaContext()
    {
    }

    public Osm2RussiaContext(DbContextOptions<Osm2RussiaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PlanetOsmLine> PlanetOsmLines { get; set; } = null!;
    public virtual DbSet<PlanetOsmLineRailway> PlanetOsmLineRailways { get; set; } = null!;
    public virtual DbSet<PlanetOsmPoint> PlanetOsmPoints { get; set; } = null!;
    public virtual DbSet<PlanetOsmPointRailway> PlanetOsmPointRailways { get; set; } = null!;
    public virtual DbSet<PlanetOsmPolygon> PlanetOsmPolygons { get; set; } = null!;
    public virtual DbSet<PlanetOsmPolygonRailway> PlanetOsmPolygonRailways { get; set; } = null!;
    public virtual DbSet<PlanetOsmRoad> PlanetOsmRoads { get; set; } = null!;
    public virtual DbSet<PlanetOsmRoadsRailway> PlanetOsmRoadsRailways { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //  Проверка настроенных параметров.
        if (!optionsBuilder.IsConfigured)
        {
            NpgsqlConnectionStringBuilder npgsqlConnectionStringBuilder = new()
            {
                Host = "10.69.16.239",
                Port = 5432,
                Database = "OsmRussiaOthers",
                Username = "postgres",
                Password = "!TTCRTdbsa2",
                Pooling = true,
                ConnectionLifetime = 600,
            };

#if DEBUG
            // Вывод запроса в вывод консоли отладки.
            optionsBuilder.LogTo(message => Debug.WriteLine(message), new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                          .EnableSensitiveDataLogging()
                          .EnableDetailedErrors();
#endif

            //  Установка строки подключения к серверу баз данных.
            optionsBuilder.UseNpgsql(npgsqlConnectionStringBuilder.ConnectionString, x => x.UseNetTopologySuite());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("hstore")
            .HasPostgresExtension("postgis");

        modelBuilder.Entity<PlanetOsmLine>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("planet_osm_line");

            entity.HasIndex(e => e.Way, "planet_osm_line_way_idx")
                .HasMethod("gist");

            entity.Property(e => e.Access).HasColumnName("access");

            entity.Property(e => e.AddrHousename).HasColumnName("addr:housename");

            entity.Property(e => e.AddrHousenumber).HasColumnName("addr:housenumber");

            entity.Property(e => e.AddrInterpolation).HasColumnName("addr:interpolation");

            entity.Property(e => e.AdminLevel).HasColumnName("admin_level");

            entity.Property(e => e.Aerialway).HasColumnName("aerialway");

            entity.Property(e => e.Aeroway).HasColumnName("aeroway");

            entity.Property(e => e.Amenity).HasColumnName("amenity");

            entity.Property(e => e.Area).HasColumnName("area");

            entity.Property(e => e.Barrier).HasColumnName("barrier");

            entity.Property(e => e.Bicycle).HasColumnName("bicycle");

            entity.Property(e => e.Boundary).HasColumnName("boundary");

            entity.Property(e => e.Brand).HasColumnName("brand");

            entity.Property(e => e.Bridge).HasColumnName("bridge");

            entity.Property(e => e.Building).HasColumnName("building");

            entity.Property(e => e.Construction).HasColumnName("construction");

            entity.Property(e => e.Covered).HasColumnName("covered");

            entity.Property(e => e.Culvert).HasColumnName("culvert");

            entity.Property(e => e.Cutting).HasColumnName("cutting");

            entity.Property(e => e.Denomination).HasColumnName("denomination");

            entity.Property(e => e.Disused).HasColumnName("disused");

            entity.Property(e => e.Embankment).HasColumnName("embankment");

            entity.Property(e => e.Foot).HasColumnName("foot");

            entity.Property(e => e.GeneratorSource).HasColumnName("generator:source");

            entity.Property(e => e.Harbour).HasColumnName("harbour");

            entity.Property(e => e.Highway).HasColumnName("highway");

            entity.Property(e => e.Historic).HasColumnName("historic");

            entity.Property(e => e.Horse).HasColumnName("horse");

            entity.Property(e => e.Intermittent).HasColumnName("intermittent");

            entity.Property(e => e.Junction).HasColumnName("junction");

            entity.Property(e => e.Landuse).HasColumnName("landuse");

            entity.Property(e => e.Layer).HasColumnName("layer");

            entity.Property(e => e.Leisure).HasColumnName("leisure");

            entity.Property(e => e.Lock).HasColumnName("lock");

            entity.Property(e => e.ManMade).HasColumnName("man_made");

            entity.Property(e => e.Military).HasColumnName("military");

            entity.Property(e => e.Motorcar).HasColumnName("motorcar");

            entity.Property(e => e.Name).HasColumnName("name");

            entity.Property(e => e.Natural).HasColumnName("natural");

            entity.Property(e => e.Office).HasColumnName("office");

            entity.Property(e => e.Oneway).HasColumnName("oneway");

            entity.Property(e => e.Operator).HasColumnName("operator");

            entity.Property(e => e.OsmId).HasColumnName("osm_id");

            entity.Property(e => e.Place).HasColumnName("place");

            entity.Property(e => e.Population).HasColumnName("population");

            entity.Property(e => e.Power).HasColumnName("power");

            entity.Property(e => e.PowerSource).HasColumnName("power_source");

            entity.Property(e => e.PublicTransport).HasColumnName("public_transport");

            entity.Property(e => e.Railway).HasColumnName("railway");

            entity.Property(e => e.Ref).HasColumnName("ref");

            entity.Property(e => e.Religion).HasColumnName("religion");

            entity.Property(e => e.Route).HasColumnName("route");

            entity.Property(e => e.Service).HasColumnName("service");

            entity.Property(e => e.Shop).HasColumnName("shop");

            entity.Property(e => e.Sport).HasColumnName("sport");

            entity.Property(e => e.Surface).HasColumnName("surface");

            entity.Property(e => e.Toll).HasColumnName("toll");

            entity.Property(e => e.Tourism).HasColumnName("tourism");

            entity.Property(e => e.TowerType).HasColumnName("tower:type");

            entity.Property(e => e.Tracktype).HasColumnName("tracktype");

            entity.Property(e => e.Tunnel).HasColumnName("tunnel");

            entity.Property(e => e.Water).HasColumnName("water");

            entity.Property(e => e.Waterway).HasColumnName("waterway");

            entity.Property(e => e.Way)
                .HasColumnType("geometry(LineString,3857)")
                .HasColumnName("way");

            entity.Property(e => e.WayArea).HasColumnName("way_area");

            entity.Property(e => e.Wetland).HasColumnName("wetland");

            entity.Property(e => e.Width).HasColumnName("width");

            entity.Property(e => e.Wood).HasColumnName("wood");

            entity.Property(e => e.ZOrder).HasColumnName("z_order");
        });

        modelBuilder.Entity<PlanetOsmLineRailway>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("planet_osm_line_railway");

            entity.HasIndex(e => e.Way, "planet_osm_line_railway_way_idx")
                .HasMethod("gist");

            entity.Property(e => e.Access).HasColumnName("access");

            entity.Property(e => e.AddrHousename).HasColumnName("addr:housename");

            entity.Property(e => e.AddrHousenumber).HasColumnName("addr:housenumber");

            entity.Property(e => e.AddrInterpolation).HasColumnName("addr:interpolation");

            entity.Property(e => e.AdminLevel).HasColumnName("admin_level");

            entity.Property(e => e.Aerialway).HasColumnName("aerialway");

            entity.Property(e => e.Aeroway).HasColumnName("aeroway");

            entity.Property(e => e.Amenity).HasColumnName("amenity");

            entity.Property(e => e.Area).HasColumnName("area");

            entity.Property(e => e.Barrier).HasColumnName("barrier");

            entity.Property(e => e.Bicycle).HasColumnName("bicycle");

            entity.Property(e => e.Boundary).HasColumnName("boundary");

            entity.Property(e => e.Brand).HasColumnName("brand");

            entity.Property(e => e.Bridge).HasColumnName("bridge");

            entity.Property(e => e.Building).HasColumnName("building");

            entity.Property(e => e.Construction).HasColumnName("construction");

            entity.Property(e => e.Covered).HasColumnName("covered");

            entity.Property(e => e.Culvert).HasColumnName("culvert");

            entity.Property(e => e.Cutting).HasColumnName("cutting");

            entity.Property(e => e.Denomination).HasColumnName("denomination");

            entity.Property(e => e.Disused).HasColumnName("disused");

            entity.Property(e => e.Embankment).HasColumnName("embankment");

            entity.Property(e => e.Foot).HasColumnName("foot");

            entity.Property(e => e.GeneratorSource).HasColumnName("generator:source");

            entity.Property(e => e.Harbour).HasColumnName("harbour");

            entity.Property(e => e.Highway).HasColumnName("highway");

            entity.Property(e => e.Historic).HasColumnName("historic");

            entity.Property(e => e.Horse).HasColumnName("horse");

            entity.Property(e => e.Intermittent).HasColumnName("intermittent");

            entity.Property(e => e.Junction).HasColumnName("junction");

            entity.Property(e => e.Landuse).HasColumnName("landuse");

            entity.Property(e => e.Layer).HasColumnName("layer");

            entity.Property(e => e.Leisure).HasColumnName("leisure");

            entity.Property(e => e.Lock).HasColumnName("lock");

            entity.Property(e => e.ManMade).HasColumnName("man_made");

            entity.Property(e => e.Military).HasColumnName("military");

            entity.Property(e => e.Motorcar).HasColumnName("motorcar");

            entity.Property(e => e.Name).HasColumnName("name");

            entity.Property(e => e.Natural).HasColumnName("natural");

            entity.Property(e => e.Office).HasColumnName("office");

            entity.Property(e => e.Oneway).HasColumnName("oneway");

            entity.Property(e => e.Operator).HasColumnName("operator");

            entity.Property(e => e.OsmId).HasColumnName("osm_id");

            entity.Property(e => e.Place).HasColumnName("place");

            entity.Property(e => e.Population).HasColumnName("population");

            entity.Property(e => e.Power).HasColumnName("power");

            entity.Property(e => e.PowerSource).HasColumnName("power_source");

            entity.Property(e => e.PublicTransport).HasColumnName("public_transport");

            entity.Property(e => e.Railway).HasColumnName("railway");

            entity.Property(e => e.Ref).HasColumnName("ref");

            entity.Property(e => e.Religion).HasColumnName("religion");

            entity.Property(e => e.Route).HasColumnName("route");

            entity.Property(e => e.Service).HasColumnName("service");

            entity.Property(e => e.Shop).HasColumnName("shop");

            entity.Property(e => e.Sport).HasColumnName("sport");

            entity.Property(e => e.Surface).HasColumnName("surface");

            entity.Property(e => e.Toll).HasColumnName("toll");

            entity.Property(e => e.Tourism).HasColumnName("tourism");

            entity.Property(e => e.TowerType).HasColumnName("tower:type");

            entity.Property(e => e.Tracktype).HasColumnName("tracktype");

            entity.Property(e => e.Tunnel).HasColumnName("tunnel");

            entity.Property(e => e.Water).HasColumnName("water");

            entity.Property(e => e.Waterway).HasColumnName("waterway");

            entity.Property(e => e.Way)
                .HasColumnType("geometry(LineString,3857)")
                .HasColumnName("way");

            entity.Property(e => e.WayArea).HasColumnName("way_area");

            entity.Property(e => e.Wetland).HasColumnName("wetland");

            entity.Property(e => e.Width).HasColumnName("width");

            entity.Property(e => e.Wood).HasColumnName("wood");

            entity.Property(e => e.ZOrder).HasColumnName("z_order");
        });

        modelBuilder.Entity<PlanetOsmPoint>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("planet_osm_point");

            entity.HasIndex(e => e.Way, "planet_osm_point_way_idx")
                .HasMethod("gist");

            entity.Property(e => e.Access).HasColumnName("access");

            entity.Property(e => e.AddrHousename).HasColumnName("addr:housename");

            entity.Property(e => e.AddrHousenumber).HasColumnName("addr:housenumber");

            entity.Property(e => e.AddrInterpolation).HasColumnName("addr:interpolation");

            entity.Property(e => e.AdminLevel).HasColumnName("admin_level");

            entity.Property(e => e.Aerialway).HasColumnName("aerialway");

            entity.Property(e => e.Aeroway).HasColumnName("aeroway");

            entity.Property(e => e.Amenity).HasColumnName("amenity");

            entity.Property(e => e.Area).HasColumnName("area");

            entity.Property(e => e.Barrier).HasColumnName("barrier");

            entity.Property(e => e.Bicycle).HasColumnName("bicycle");

            entity.Property(e => e.Boundary).HasColumnName("boundary");

            entity.Property(e => e.Brand).HasColumnName("brand");

            entity.Property(e => e.Bridge).HasColumnName("bridge");

            entity.Property(e => e.Building).HasColumnName("building");

            entity.Property(e => e.Capital).HasColumnName("capital");

            entity.Property(e => e.Construction).HasColumnName("construction");

            entity.Property(e => e.Covered).HasColumnName("covered");

            entity.Property(e => e.Culvert).HasColumnName("culvert");

            entity.Property(e => e.Cutting).HasColumnName("cutting");

            entity.Property(e => e.Denomination).HasColumnName("denomination");

            entity.Property(e => e.Disused).HasColumnName("disused");

            entity.Property(e => e.Ele).HasColumnName("ele");

            entity.Property(e => e.Embankment).HasColumnName("embankment");

            entity.Property(e => e.Foot).HasColumnName("foot");

            entity.Property(e => e.GeneratorSource).HasColumnName("generator:source");

            entity.Property(e => e.Harbour).HasColumnName("harbour");

            entity.Property(e => e.Highway).HasColumnName("highway");

            entity.Property(e => e.Historic).HasColumnName("historic");

            entity.Property(e => e.Horse).HasColumnName("horse");

            entity.Property(e => e.Intermittent).HasColumnName("intermittent");

            entity.Property(e => e.Junction).HasColumnName("junction");

            entity.Property(e => e.Landuse).HasColumnName("landuse");

            entity.Property(e => e.Layer).HasColumnName("layer");

            entity.Property(e => e.Leisure).HasColumnName("leisure");

            entity.Property(e => e.Lock).HasColumnName("lock");

            entity.Property(e => e.ManMade).HasColumnName("man_made");

            entity.Property(e => e.Military).HasColumnName("military");

            entity.Property(e => e.Motorcar).HasColumnName("motorcar");

            entity.Property(e => e.Name).HasColumnName("name");

            entity.Property(e => e.Natural).HasColumnName("natural");

            entity.Property(e => e.Office).HasColumnName("office");

            entity.Property(e => e.Oneway).HasColumnName("oneway");

            entity.Property(e => e.Operator).HasColumnName("operator");

            entity.Property(e => e.OsmId).HasColumnName("osm_id");

            entity.Property(e => e.Place).HasColumnName("place");

            entity.Property(e => e.Population).HasColumnName("population");

            entity.Property(e => e.Power).HasColumnName("power");

            entity.Property(e => e.PowerSource).HasColumnName("power_source");

            entity.Property(e => e.PublicTransport).HasColumnName("public_transport");

            entity.Property(e => e.Railway).HasColumnName("railway");

            entity.Property(e => e.Ref).HasColumnName("ref");

            entity.Property(e => e.Religion).HasColumnName("religion");

            entity.Property(e => e.Route).HasColumnName("route");

            entity.Property(e => e.Service).HasColumnName("service");

            entity.Property(e => e.Shop).HasColumnName("shop");

            entity.Property(e => e.Sport).HasColumnName("sport");

            entity.Property(e => e.Surface).HasColumnName("surface");

            entity.Property(e => e.Toll).HasColumnName("toll");

            entity.Property(e => e.Tourism).HasColumnName("tourism");

            entity.Property(e => e.TowerType).HasColumnName("tower:type");

            entity.Property(e => e.Tunnel).HasColumnName("tunnel");

            entity.Property(e => e.Water).HasColumnName("water");

            entity.Property(e => e.Waterway).HasColumnName("waterway");

            entity.Property(e => e.Way)
                .HasColumnType("geometry(Point,3857)")
                .HasColumnName("way");

            entity.Property(e => e.Wetland).HasColumnName("wetland");

            entity.Property(e => e.Width).HasColumnName("width");

            entity.Property(e => e.Wood).HasColumnName("wood");

            entity.Property(e => e.ZOrder).HasColumnName("z_order");
        });

        modelBuilder.Entity<PlanetOsmPointRailway>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("planet_osm_point_railway");

            entity.HasIndex(e => e.Way, "planet_osm_point_railway_way_idx")
                .HasMethod("gist");

            entity.Property(e => e.Access).HasColumnName("access");

            entity.Property(e => e.AddrHousename).HasColumnName("addr:housename");

            entity.Property(e => e.AddrHousenumber).HasColumnName("addr:housenumber");

            entity.Property(e => e.AddrInterpolation).HasColumnName("addr:interpolation");

            entity.Property(e => e.AdminLevel).HasColumnName("admin_level");

            entity.Property(e => e.Aerialway).HasColumnName("aerialway");

            entity.Property(e => e.Aeroway).HasColumnName("aeroway");

            entity.Property(e => e.Amenity).HasColumnName("amenity");

            entity.Property(e => e.Area).HasColumnName("area");

            entity.Property(e => e.Barrier).HasColumnName("barrier");

            entity.Property(e => e.Bicycle).HasColumnName("bicycle");

            entity.Property(e => e.Boundary).HasColumnName("boundary");

            entity.Property(e => e.Brand).HasColumnName("brand");

            entity.Property(e => e.Bridge).HasColumnName("bridge");

            entity.Property(e => e.Building).HasColumnName("building");

            entity.Property(e => e.Capital).HasColumnName("capital");

            entity.Property(e => e.Construction).HasColumnName("construction");

            entity.Property(e => e.Covered).HasColumnName("covered");

            entity.Property(e => e.Culvert).HasColumnName("culvert");

            entity.Property(e => e.Cutting).HasColumnName("cutting");

            entity.Property(e => e.Denomination).HasColumnName("denomination");

            entity.Property(e => e.Disused).HasColumnName("disused");

            entity.Property(e => e.Ele).HasColumnName("ele");

            entity.Property(e => e.Embankment).HasColumnName("embankment");

            entity.Property(e => e.Foot).HasColumnName("foot");

            entity.Property(e => e.GeneratorSource).HasColumnName("generator:source");

            entity.Property(e => e.Harbour).HasColumnName("harbour");

            entity.Property(e => e.Highway).HasColumnName("highway");

            entity.Property(e => e.Historic).HasColumnName("historic");

            entity.Property(e => e.Horse).HasColumnName("horse");

            entity.Property(e => e.Intermittent).HasColumnName("intermittent");

            entity.Property(e => e.Junction).HasColumnName("junction");

            entity.Property(e => e.Landuse).HasColumnName("landuse");

            entity.Property(e => e.Layer).HasColumnName("layer");

            entity.Property(e => e.Leisure).HasColumnName("leisure");

            entity.Property(e => e.Lock).HasColumnName("lock");

            entity.Property(e => e.ManMade).HasColumnName("man_made");

            entity.Property(e => e.Military).HasColumnName("military");

            entity.Property(e => e.Motorcar).HasColumnName("motorcar");

            entity.Property(e => e.Name).HasColumnName("name");

            entity.Property(e => e.Natural).HasColumnName("natural");

            entity.Property(e => e.Office).HasColumnName("office");

            entity.Property(e => e.Oneway).HasColumnName("oneway");

            entity.Property(e => e.Operator).HasColumnName("operator");

            entity.Property(e => e.OsmId).HasColumnName("osm_id");

            entity.Property(e => e.Place).HasColumnName("place");

            entity.Property(e => e.Population).HasColumnName("population");

            entity.Property(e => e.Power).HasColumnName("power");

            entity.Property(e => e.PowerSource).HasColumnName("power_source");

            entity.Property(e => e.PublicTransport).HasColumnName("public_transport");

            entity.Property(e => e.Railway).HasColumnName("railway");

            entity.Property(e => e.Ref).HasColumnName("ref");

            entity.Property(e => e.Religion).HasColumnName("religion");

            entity.Property(e => e.Route).HasColumnName("route");

            entity.Property(e => e.Service).HasColumnName("service");

            entity.Property(e => e.Shop).HasColumnName("shop");

            entity.Property(e => e.Sport).HasColumnName("sport");

            entity.Property(e => e.Surface).HasColumnName("surface");

            entity.Property(e => e.Toll).HasColumnName("toll");

            entity.Property(e => e.Tourism).HasColumnName("tourism");

            entity.Property(e => e.TowerType).HasColumnName("tower:type");

            entity.Property(e => e.Tunnel).HasColumnName("tunnel");

            entity.Property(e => e.Water).HasColumnName("water");

            entity.Property(e => e.Waterway).HasColumnName("waterway");

            entity.Property(e => e.Way)
                .HasColumnType("geometry(Point,3857)")
                .HasColumnName("way");

            entity.Property(e => e.Wetland).HasColumnName("wetland");

            entity.Property(e => e.Width).HasColumnName("width");

            entity.Property(e => e.Wood).HasColumnName("wood");

            entity.Property(e => e.ZOrder).HasColumnName("z_order");
        });

        modelBuilder.Entity<PlanetOsmPolygon>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("planet_osm_polygon");

            entity.HasIndex(e => e.Way, "planet_osm_polygon_way_idx")
                .HasMethod("gist");

            entity.Property(e => e.Access).HasColumnName("access");

            entity.Property(e => e.AddrHousename).HasColumnName("addr:housename");

            entity.Property(e => e.AddrHousenumber).HasColumnName("addr:housenumber");

            entity.Property(e => e.AddrInterpolation).HasColumnName("addr:interpolation");

            entity.Property(e => e.AdminLevel).HasColumnName("admin_level");

            entity.Property(e => e.Aerialway).HasColumnName("aerialway");

            entity.Property(e => e.Aeroway).HasColumnName("aeroway");

            entity.Property(e => e.Amenity).HasColumnName("amenity");

            entity.Property(e => e.Area).HasColumnName("area");

            entity.Property(e => e.Barrier).HasColumnName("barrier");

            entity.Property(e => e.Bicycle).HasColumnName("bicycle");

            entity.Property(e => e.Boundary).HasColumnName("boundary");

            entity.Property(e => e.Brand).HasColumnName("brand");

            entity.Property(e => e.Bridge).HasColumnName("bridge");

            entity.Property(e => e.Building).HasColumnName("building");

            entity.Property(e => e.Construction).HasColumnName("construction");

            entity.Property(e => e.Covered).HasColumnName("covered");

            entity.Property(e => e.Culvert).HasColumnName("culvert");

            entity.Property(e => e.Cutting).HasColumnName("cutting");

            entity.Property(e => e.Denomination).HasColumnName("denomination");

            entity.Property(e => e.Disused).HasColumnName("disused");

            entity.Property(e => e.Embankment).HasColumnName("embankment");

            entity.Property(e => e.Foot).HasColumnName("foot");

            entity.Property(e => e.GeneratorSource).HasColumnName("generator:source");

            entity.Property(e => e.Harbour).HasColumnName("harbour");

            entity.Property(e => e.Highway).HasColumnName("highway");

            entity.Property(e => e.Historic).HasColumnName("historic");

            entity.Property(e => e.Horse).HasColumnName("horse");

            entity.Property(e => e.Intermittent).HasColumnName("intermittent");

            entity.Property(e => e.Junction).HasColumnName("junction");

            entity.Property(e => e.Landuse).HasColumnName("landuse");

            entity.Property(e => e.Layer).HasColumnName("layer");

            entity.Property(e => e.Leisure).HasColumnName("leisure");

            entity.Property(e => e.Lock).HasColumnName("lock");

            entity.Property(e => e.ManMade).HasColumnName("man_made");

            entity.Property(e => e.Military).HasColumnName("military");

            entity.Property(e => e.Motorcar).HasColumnName("motorcar");

            entity.Property(e => e.Name).HasColumnName("name");

            entity.Property(e => e.Natural).HasColumnName("natural");

            entity.Property(e => e.Office).HasColumnName("office");

            entity.Property(e => e.Oneway).HasColumnName("oneway");

            entity.Property(e => e.Operator).HasColumnName("operator");

            entity.Property(e => e.OsmId).HasColumnName("osm_id");

            entity.Property(e => e.Place).HasColumnName("place");

            entity.Property(e => e.Population).HasColumnName("population");

            entity.Property(e => e.Power).HasColumnName("power");

            entity.Property(e => e.PowerSource).HasColumnName("power_source");

            entity.Property(e => e.PublicTransport).HasColumnName("public_transport");

            entity.Property(e => e.Railway).HasColumnName("railway");

            entity.Property(e => e.Ref).HasColumnName("ref");

            entity.Property(e => e.Religion).HasColumnName("religion");

            entity.Property(e => e.Route).HasColumnName("route");

            entity.Property(e => e.Service).HasColumnName("service");

            entity.Property(e => e.Shop).HasColumnName("shop");

            entity.Property(e => e.Sport).HasColumnName("sport");

            entity.Property(e => e.Surface).HasColumnName("surface");

            entity.Property(e => e.Toll).HasColumnName("toll");

            entity.Property(e => e.Tourism).HasColumnName("tourism");

            entity.Property(e => e.TowerType).HasColumnName("tower:type");

            entity.Property(e => e.Tracktype).HasColumnName("tracktype");

            entity.Property(e => e.Tunnel).HasColumnName("tunnel");

            entity.Property(e => e.Water).HasColumnName("water");

            entity.Property(e => e.Waterway).HasColumnName("waterway");

            entity.Property(e => e.Way)
                .HasColumnType("geometry(Geometry,3857)")
                .HasColumnName("way");

            entity.Property(e => e.WayArea).HasColumnName("way_area");

            entity.Property(e => e.Wetland).HasColumnName("wetland");

            entity.Property(e => e.Width).HasColumnName("width");

            entity.Property(e => e.Wood).HasColumnName("wood");

            entity.Property(e => e.ZOrder).HasColumnName("z_order");
        });

        modelBuilder.Entity<PlanetOsmPolygonRailway>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("planet_osm_polygon_railway");

            entity.HasIndex(e => e.Way, "planet_osm_polygone_railway_way_idx")
                .HasMethod("gist");

            entity.Property(e => e.Access).HasColumnName("access");

            entity.Property(e => e.AddrHousename).HasColumnName("addr:housename");

            entity.Property(e => e.AddrHousenumber).HasColumnName("addr:housenumber");

            entity.Property(e => e.AddrInterpolation).HasColumnName("addr:interpolation");

            entity.Property(e => e.AdminLevel).HasColumnName("admin_level");

            entity.Property(e => e.Aerialway).HasColumnName("aerialway");

            entity.Property(e => e.Aeroway).HasColumnName("aeroway");

            entity.Property(e => e.Amenity).HasColumnName("amenity");

            entity.Property(e => e.Area).HasColumnName("area");

            entity.Property(e => e.Barrier).HasColumnName("barrier");

            entity.Property(e => e.Bicycle).HasColumnName("bicycle");

            entity.Property(e => e.Boundary).HasColumnName("boundary");

            entity.Property(e => e.Brand).HasColumnName("brand");

            entity.Property(e => e.Bridge).HasColumnName("bridge");

            entity.Property(e => e.Building).HasColumnName("building");

            entity.Property(e => e.Construction).HasColumnName("construction");

            entity.Property(e => e.Covered).HasColumnName("covered");

            entity.Property(e => e.Culvert).HasColumnName("culvert");

            entity.Property(e => e.Cutting).HasColumnName("cutting");

            entity.Property(e => e.Denomination).HasColumnName("denomination");

            entity.Property(e => e.Disused).HasColumnName("disused");

            entity.Property(e => e.Embankment).HasColumnName("embankment");

            entity.Property(e => e.Foot).HasColumnName("foot");

            entity.Property(e => e.GeneratorSource).HasColumnName("generator:source");

            entity.Property(e => e.Harbour).HasColumnName("harbour");

            entity.Property(e => e.Highway).HasColumnName("highway");

            entity.Property(e => e.Historic).HasColumnName("historic");

            entity.Property(e => e.Horse).HasColumnName("horse");

            entity.Property(e => e.Intermittent).HasColumnName("intermittent");

            entity.Property(e => e.Junction).HasColumnName("junction");

            entity.Property(e => e.Landuse).HasColumnName("landuse");

            entity.Property(e => e.Layer).HasColumnName("layer");

            entity.Property(e => e.Leisure).HasColumnName("leisure");

            entity.Property(e => e.Lock).HasColumnName("lock");

            entity.Property(e => e.ManMade).HasColumnName("man_made");

            entity.Property(e => e.Military).HasColumnName("military");

            entity.Property(e => e.Motorcar).HasColumnName("motorcar");

            entity.Property(e => e.Name).HasColumnName("name");

            entity.Property(e => e.Natural).HasColumnName("natural");

            entity.Property(e => e.Office).HasColumnName("office");

            entity.Property(e => e.Oneway).HasColumnName("oneway");

            entity.Property(e => e.Operator).HasColumnName("operator");

            entity.Property(e => e.OsmId).HasColumnName("osm_id");

            entity.Property(e => e.Place).HasColumnName("place");

            entity.Property(e => e.Population).HasColumnName("population");

            entity.Property(e => e.Power).HasColumnName("power");

            entity.Property(e => e.PowerSource).HasColumnName("power_source");

            entity.Property(e => e.PublicTransport).HasColumnName("public_transport");

            entity.Property(e => e.Railway).HasColumnName("railway");

            entity.Property(e => e.Ref).HasColumnName("ref");

            entity.Property(e => e.Religion).HasColumnName("religion");

            entity.Property(e => e.Route).HasColumnName("route");

            entity.Property(e => e.Service).HasColumnName("service");

            entity.Property(e => e.Shop).HasColumnName("shop");

            entity.Property(e => e.Sport).HasColumnName("sport");

            entity.Property(e => e.Surface).HasColumnName("surface");

            entity.Property(e => e.Toll).HasColumnName("toll");

            entity.Property(e => e.Tourism).HasColumnName("tourism");

            entity.Property(e => e.TowerType).HasColumnName("tower:type");

            entity.Property(e => e.Tracktype).HasColumnName("tracktype");

            entity.Property(e => e.Tunnel).HasColumnName("tunnel");

            entity.Property(e => e.Water).HasColumnName("water");

            entity.Property(e => e.Waterway).HasColumnName("waterway");

            entity.Property(e => e.Way)
                .HasColumnType("geometry(Geometry,3857)")
                .HasColumnName("way");

            entity.Property(e => e.WayArea).HasColumnName("way_area");

            entity.Property(e => e.Wetland).HasColumnName("wetland");

            entity.Property(e => e.Width).HasColumnName("width");

            entity.Property(e => e.Wood).HasColumnName("wood");

            entity.Property(e => e.ZOrder).HasColumnName("z_order");
        });

        modelBuilder.Entity<PlanetOsmRoad>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("planet_osm_roads");

            entity.HasIndex(e => e.Way, "planet_osm_roads_way_idx")
                .HasMethod("gist");

            entity.Property(e => e.Access).HasColumnName("access");

            entity.Property(e => e.AddrHousename).HasColumnName("addr:housename");

            entity.Property(e => e.AddrHousenumber).HasColumnName("addr:housenumber");

            entity.Property(e => e.AddrInterpolation).HasColumnName("addr:interpolation");

            entity.Property(e => e.AdminLevel).HasColumnName("admin_level");

            entity.Property(e => e.Aerialway).HasColumnName("aerialway");

            entity.Property(e => e.Aeroway).HasColumnName("aeroway");

            entity.Property(e => e.Amenity).HasColumnName("amenity");

            entity.Property(e => e.Area).HasColumnName("area");

            entity.Property(e => e.Barrier).HasColumnName("barrier");

            entity.Property(e => e.Bicycle).HasColumnName("bicycle");

            entity.Property(e => e.Boundary).HasColumnName("boundary");

            entity.Property(e => e.Brand).HasColumnName("brand");

            entity.Property(e => e.Bridge).HasColumnName("bridge");

            entity.Property(e => e.Building).HasColumnName("building");

            entity.Property(e => e.Construction).HasColumnName("construction");

            entity.Property(e => e.Covered).HasColumnName("covered");

            entity.Property(e => e.Culvert).HasColumnName("culvert");

            entity.Property(e => e.Cutting).HasColumnName("cutting");

            entity.Property(e => e.Denomination).HasColumnName("denomination");

            entity.Property(e => e.Disused).HasColumnName("disused");

            entity.Property(e => e.Embankment).HasColumnName("embankment");

            entity.Property(e => e.Foot).HasColumnName("foot");

            entity.Property(e => e.GeneratorSource).HasColumnName("generator:source");

            entity.Property(e => e.Harbour).HasColumnName("harbour");

            entity.Property(e => e.Highway).HasColumnName("highway");

            entity.Property(e => e.Historic).HasColumnName("historic");

            entity.Property(e => e.Horse).HasColumnName("horse");

            entity.Property(e => e.Intermittent).HasColumnName("intermittent");

            entity.Property(e => e.Junction).HasColumnName("junction");

            entity.Property(e => e.Landuse).HasColumnName("landuse");

            entity.Property(e => e.Layer).HasColumnName("layer");

            entity.Property(e => e.Leisure).HasColumnName("leisure");

            entity.Property(e => e.Lock).HasColumnName("lock");

            entity.Property(e => e.ManMade).HasColumnName("man_made");

            entity.Property(e => e.Military).HasColumnName("military");

            entity.Property(e => e.Motorcar).HasColumnName("motorcar");

            entity.Property(e => e.Name).HasColumnName("name");

            entity.Property(e => e.Natural).HasColumnName("natural");

            entity.Property(e => e.Office).HasColumnName("office");

            entity.Property(e => e.Oneway).HasColumnName("oneway");

            entity.Property(e => e.Operator).HasColumnName("operator");

            entity.Property(e => e.OsmId).HasColumnName("osm_id");

            entity.Property(e => e.Place).HasColumnName("place");

            entity.Property(e => e.Population).HasColumnName("population");

            entity.Property(e => e.Power).HasColumnName("power");

            entity.Property(e => e.PowerSource).HasColumnName("power_source");

            entity.Property(e => e.PublicTransport).HasColumnName("public_transport");

            entity.Property(e => e.Railway).HasColumnName("railway");

            entity.Property(e => e.Ref).HasColumnName("ref");

            entity.Property(e => e.Religion).HasColumnName("religion");

            entity.Property(e => e.Route).HasColumnName("route");

            entity.Property(e => e.Service).HasColumnName("service");

            entity.Property(e => e.Shop).HasColumnName("shop");

            entity.Property(e => e.Sport).HasColumnName("sport");

            entity.Property(e => e.Surface).HasColumnName("surface");

            entity.Property(e => e.Toll).HasColumnName("toll");

            entity.Property(e => e.Tourism).HasColumnName("tourism");

            entity.Property(e => e.TowerType).HasColumnName("tower:type");

            entity.Property(e => e.Tracktype).HasColumnName("tracktype");

            entity.Property(e => e.Tunnel).HasColumnName("tunnel");

            entity.Property(e => e.Water).HasColumnName("water");

            entity.Property(e => e.Waterway).HasColumnName("waterway");

            entity.Property(e => e.Way)
                .HasColumnType("geometry(LineString,3857)")
                .HasColumnName("way");

            entity.Property(e => e.WayArea).HasColumnName("way_area");

            entity.Property(e => e.Wetland).HasColumnName("wetland");

            entity.Property(e => e.Width).HasColumnName("width");

            entity.Property(e => e.Wood).HasColumnName("wood");

            entity.Property(e => e.ZOrder).HasColumnName("z_order");
        });

        modelBuilder.Entity<PlanetOsmRoadsRailway>(entity =>
        {
            entity.HasNoKey();

            entity.ToTable("planet_osm_roads_railway");

            entity.HasIndex(e => e.Way, "planet_osm_roads_railway_way_idx")
                .HasMethod("gist");

            entity.Property(e => e.Access).HasColumnName("access");

            entity.Property(e => e.AddrHousename).HasColumnName("addr:housename");

            entity.Property(e => e.AddrHousenumber).HasColumnName("addr:housenumber");

            entity.Property(e => e.AddrInterpolation).HasColumnName("addr:interpolation");

            entity.Property(e => e.AdminLevel).HasColumnName("admin_level");

            entity.Property(e => e.Aerialway).HasColumnName("aerialway");

            entity.Property(e => e.Aeroway).HasColumnName("aeroway");

            entity.Property(e => e.Amenity).HasColumnName("amenity");

            entity.Property(e => e.Area).HasColumnName("area");

            entity.Property(e => e.Barrier).HasColumnName("barrier");

            entity.Property(e => e.Bicycle).HasColumnName("bicycle");

            entity.Property(e => e.Boundary).HasColumnName("boundary");

            entity.Property(e => e.Brand).HasColumnName("brand");

            entity.Property(e => e.Bridge).HasColumnName("bridge");

            entity.Property(e => e.Building).HasColumnName("building");

            entity.Property(e => e.Construction).HasColumnName("construction");

            entity.Property(e => e.Covered).HasColumnName("covered");

            entity.Property(e => e.Culvert).HasColumnName("culvert");

            entity.Property(e => e.Cutting).HasColumnName("cutting");

            entity.Property(e => e.Denomination).HasColumnName("denomination");

            entity.Property(e => e.Disused).HasColumnName("disused");

            entity.Property(e => e.Embankment).HasColumnName("embankment");

            entity.Property(e => e.Foot).HasColumnName("foot");

            entity.Property(e => e.GeneratorSource).HasColumnName("generator:source");

            entity.Property(e => e.Harbour).HasColumnName("harbour");

            entity.Property(e => e.Highway).HasColumnName("highway");

            entity.Property(e => e.Historic).HasColumnName("historic");

            entity.Property(e => e.Horse).HasColumnName("horse");

            entity.Property(e => e.Intermittent).HasColumnName("intermittent");

            entity.Property(e => e.Junction).HasColumnName("junction");

            entity.Property(e => e.Landuse).HasColumnName("landuse");

            entity.Property(e => e.Layer).HasColumnName("layer");

            entity.Property(e => e.Leisure).HasColumnName("leisure");

            entity.Property(e => e.Lock).HasColumnName("lock");

            entity.Property(e => e.ManMade).HasColumnName("man_made");

            entity.Property(e => e.Military).HasColumnName("military");

            entity.Property(e => e.Motorcar).HasColumnName("motorcar");

            entity.Property(e => e.Name).HasColumnName("name");

            entity.Property(e => e.Natural).HasColumnName("natural");

            entity.Property(e => e.Office).HasColumnName("office");

            entity.Property(e => e.Oneway).HasColumnName("oneway");

            entity.Property(e => e.Operator).HasColumnName("operator");

            entity.Property(e => e.OsmId).HasColumnName("osm_id");

            entity.Property(e => e.Place).HasColumnName("place");

            entity.Property(e => e.Population).HasColumnName("population");

            entity.Property(e => e.Power).HasColumnName("power");

            entity.Property(e => e.PowerSource).HasColumnName("power_source");

            entity.Property(e => e.PublicTransport).HasColumnName("public_transport");

            entity.Property(e => e.Railway).HasColumnName("railway");

            entity.Property(e => e.Ref).HasColumnName("ref");

            entity.Property(e => e.Religion).HasColumnName("religion");

            entity.Property(e => e.Route).HasColumnName("route");

            entity.Property(e => e.Service).HasColumnName("service");

            entity.Property(e => e.Shop).HasColumnName("shop");

            entity.Property(e => e.Sport).HasColumnName("sport");

            entity.Property(e => e.Surface).HasColumnName("surface");

            entity.Property(e => e.Toll).HasColumnName("toll");

            entity.Property(e => e.Tourism).HasColumnName("tourism");

            entity.Property(e => e.TowerType).HasColumnName("tower:type");

            entity.Property(e => e.Tracktype).HasColumnName("tracktype");

            entity.Property(e => e.Tunnel).HasColumnName("tunnel");

            entity.Property(e => e.Water).HasColumnName("water");

            entity.Property(e => e.Waterway).HasColumnName("waterway");

            entity.Property(e => e.Way)
                .HasColumnType("geometry(LineString,3857)")
                .HasColumnName("way");

            entity.Property(e => e.WayArea).HasColumnName("way_area");

            entity.Property(e => e.Wetland).HasColumnName("wetland");

            entity.Property(e => e.Width).HasColumnName("width");

            entity.Property(e => e.Wood).HasColumnName("wood");

            entity.Property(e => e.ZOrder).HasColumnName("z_order");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
