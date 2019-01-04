# NetTopologySuite.Diagnostics

Spatial tracing and debugger visualizers for NetTopology geometries.

.Net Standard replacement of [SqlServerSpatial.Toolkit](https://github.com/xfischer/SqlServerSpatial.Toolkit)

- Only enables tracing for the moment. A viewer will soon be ported.

using SqlServerSpatial.Toolkit;

```
using NetTopologySuite.Diagnostics.Tracing;

// Enable tracing
SpatialTrace.Enable(); 
// Trace sample geometry instance. 
// Works with any IGeometry instance, and IEnumerable<IGeometry>
SpatialTrace.TraceGeometry(geometry, "Sample geometry with default style");

// Change styling
SpatialTrace.SetLineWidth(3); // Current stroke style is 3px wide
SpatialTrace.SetFillColor(Color.FromArgb(128, 255, 0, 0)); // Fills with red

// Style is applied to subsequent traces 
SpatialTrace.TraceGeometry(geometry, "Some text");

// Reset style
SpatialTrace.ResetStyle();
```
