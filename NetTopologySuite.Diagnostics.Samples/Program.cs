using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GeoAPI.Geometries;
using NetTopologySuite.Diagnostics.Tracing;
using NetTopologySuite.IO;
using static NetTopologySuite.Diagnostics.Samples.SampleData;

namespace NetTopologySuite.Diagnostics.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestVariousGeometries();

            TestWorld(true, true);

            Console.WriteLine("Done !");
        }

        static void TestVariousGeometries()
        {
            WKTReader _wktReader = new WKTReader();
            _wktReader.DefaultSRID = 4326;
            IGeometry simplePoint = new Geometries.Point(1, 47) { SRID = 4326 };

            IGeometry multiPoint = _wktReader.Read("MULTIPOINT((1 47),(1 46),(0 46),(0 47),(1 47))");
            IGeometry lineString = _wktReader.Read("LINESTRING(1 47,1 46,0 46,0 47,1 47)");
            IGeometry multiLineString = _wktReader.Read("MULTILINESTRING((0.516357421875 47.6415668949958,0.516357421875 47.34463879017405,0.977783203125 47.22539733216678,1.175537109375 47.463611506072866,0.516357421875 47.6415668949958),(0.764923095703125 47.86549372980948,0.951690673828125 47.82309640371982,1.220855712890625 47.79911736820551,1.089019775390625 47.69015026565801,1.256561279296875 47.656860648589))");
            IGeometry simplePoly = _wktReader.Read("POLYGON((1 47,1 46,0 46,0 47,1 47))");
            IGeometry polyWithHole = _wktReader.Read(@"
                    POLYGON(
                    (0.516357421875 47.6415668949958,0.516357421875 47.34463879017405,0.977783203125 47.22539733216678,1.175537109375 47.463611506072866,0.516357421875 47.6415668949958),
                    (0.630340576171875 47.54944962456812,0.630340576171875 47.49380564962583,0.729217529296875 47.482669772098674,0.731964111328125 47.53276262898896,0.630340576171875 47.54944962456812)
                    )");
            IGeometry multiPolygon = _wktReader.Read(@"
                    MULTIPOLYGON (
                        ((40 40, 20 45, 45 30, 40 40)),
                        ((20 35, 45 20, 30 5, 10 10, 10 30, 20 35), (30 20, 20 25, 20 15, 30 20)),
                        ((0.516357421875 47.6415668949958,0.516357421875 47.34463879017405,0.977783203125 47.22539733216678,1.175537109375 47.463611506072866,0.516357421875 47.6415668949958),(0.630340576171875 47.54944962456812,0.630340576171875 47.49380564962583,0.729217529296875 47.482669772098674,0.731964111328125 47.53276262898896,0.630340576171875 47.54944962456812))
                    )");

            IGeometry geomCol = _wktReader.Read(@"
                    GEOMETRYCOLLECTION (
                        POLYGON((0.516357421875 47.6415668949958,0.516357421875 47.34463879017405,0.977783203125 47.22539733216678,1.175537109375 47.463611506072866,0.516357421875 47.6415668949958),(0.630340576171875 47.54944962456812,0.630340576171875 47.49380564962583,0.729217529296875 47.482669772098674,0.731964111328125 47.53276262898896,0.630340576171875 47.54944962456812)),
                        LINESTRING(0.764923095703125 47.86549372980948,0.951690673828125 47.82309640371982,1.220855712890625 47.79911736820551,1.089019775390625 47.69015026565801,1.256561279296875 47.656860648589),
                        POINT(0.767669677734375 47.817563762851776)
                    )");

            SpatialTrace.Enable();
            SpatialTrace.SetFillColor(Color.FromArgb(128, 0, 0, 255)); // Fill with blue
            SpatialTrace.TraceGeometry(simplePoint, "simplePoint");
            SpatialTrace.SetFillColor(Color.FromArgb(128, 255, 0, 0)); // Fill with red
            SpatialTrace.TraceGeometry(multiPoint, "multiPoint");
            SpatialTrace.ResetStyle();
            SpatialTrace.TraceGeometry(lineString, "lineString");
            SpatialTrace.TraceGeometry(multiLineString, "multiLineString");
            SpatialTrace.TraceGeometry(simplePoly, "simplePoly");
            SpatialTrace.TraceGeometry(polyWithHole, "polyWithHole");
            SpatialTrace.TraceGeometry(multiPolygon, "multiPolygon");
            SpatialTrace.TraceGeometry(geomCol, "geomCol");
            //SpatialTrace.ShowDialog();
            //SpatialTrace.Clear();
        }

        static void TestWorld(bool useLabels = false, bool useIndents = false)
        {
            SpatialTrace.Enable();
            SpatialTrace.Clear();

            List<Country> v_countries = GetCountries();

            SpatialTrace.Enable();
            foreach (var countryByContinent in v_countries.GroupBy(p => p.Continent))
            {
                if (useIndents) SpatialTrace.Indent(countryByContinent.Key);

                foreach (var country in countryByContinent)
                {
                    SpatialTrace.TraceGeometry(country.Geometry, country.Name, country.Name);
                }

                if (useIndents) SpatialTrace.Unindent();
            }

            //SpatialTrace.ShowDialog();
        }

    }
}
