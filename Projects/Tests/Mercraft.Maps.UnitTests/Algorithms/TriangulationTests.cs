﻿using System.Linq;
using Mercraft.Core;
using Mercraft.Core.Algorithms;
using Mercraft.Core.Scene;
using Mercraft.Maps.Osm;
using Mercraft.Maps.Osm.Data;
using Mercraft.Maps.Osm.Visitors;
using NUnit.Framework;

namespace Mercraft.Maps.UnitTests.Algorithms
{
    /// <summary>
    ///     These tests test functionality which seems to be depricated in near future (?)
    /// </summary>
    [TestFixture]
    public class TriangulationTests
    {
        [Test]
        public void CanTriangulateNonStandard()
        {
            // ARRANGE
            var verticies = new[]
            {
                new MapPoint(669.0f, -181.5f),
                new MapPoint(671.2f, -188.2f),
                new MapPoint(682.9f, -184.4f),
                new MapPoint(688.9f, -202.4f),
                new MapPoint(670.0f, -208.6f),
                new MapPoint(664.1f, -190.5f),
                new MapPoint(671.2f, -188.2f)
            };
            var triangulator = new Triangulator(verticies);

            // ACT
            var triangles = triangulator.Triangulate();
        }

        [Test]
        public void CanTriangulateAreasAndWays()
        {
            // ARRANGE
            var dataSource = new PbfIndexListElementSource(TestHelper.TestBigPbfIndexListPath,
                new TestPathResolver());

            var bbox = BoundingBox.CreateBoundingBox(TestHelper.BerlinGeoCenter, 1000);

            var scene = new MapScene();

            var elementManager = new ElementManager();

            elementManager.VisitBoundingBox(bbox, dataSource, new WayVisitor(scene));

            // ACT & ARRANGE
            foreach (var area in scene.Areas)
            {
                var verticies = PolygonHelper.GetVerticies2D(TestHelper.BerlinGeoCenter, area.Points.ToList());
                var triangles = PolygonHelper.GetTriangles3D(verticies);
            }

            foreach (var way in scene.Ways)
            {
                var verticies = PolygonHelper.GetVerticies2D(TestHelper.BerlinGeoCenter, way.Points.ToList());
                var triangles = PolygonHelper.GetTriangles3D(verticies);
            }
        }
    }
}