﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mercraft.Core;
using Mercraft.Core.Algorithms;
using Mercraft.Core.MapCss.Domain;
using Mercraft.Core.Scene.Models;
using Mercraft.Explorer.Helpers;
using UnityEngine;

namespace Mercraft.Explorer.Builders
{
    public class FlatModelBuilder : ModelBuilder
    {
        public override GameObject BuildArea(GeoCoordinate center, Rule rule, Area area)
        {
            base.BuildArea(center, rule, area);
            GameObject gameObject = new GameObject();
           /* // TODO remove this assertion after handling this case above
            if (area.Points.Length < 3)
            {
                Debug.LogError("Area contains less than 3 points: " + area);
                return null;
            }*/

            gameObject.name = String.Format("Flat {0}", area);

            var floor = rule.GetZIndex(area);

            var verticies = PolygonHelper.GetVerticies2D(center, area.Points);

            var mesh = new Mesh();
            mesh.vertices = PolygonHelper.GetVerticies(verticies, floor);
            mesh.uv = PolygonHelper.GetUV(verticies);
            mesh.triangles = PolygonHelper.GetTriangles(verticies);

            var meshFilter = gameObject.AddComponent<MeshFilter>();
            meshFilter.mesh.Clear();
            meshFilter.mesh = mesh;
            meshFilter.mesh.RecalculateNormals();

            return gameObject;
        }

        public override GameObject BuildWay(GeoCoordinate center, Rule rule, Way way)
        {
            base.BuildWay(center, rule, way);
            GameObject gameObject = new GameObject();
            var zIndex = rule.GetZIndex(way);

            gameObject.name = String.Format("Flat {0}", way);

            var points = PolygonHelper.GetVerticies2D(center, way.Points);

            var lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = rule.GetMaterial(way);
            lineRenderer.material.color = rule.GetFillColor(way);
            lineRenderer.SetVertexCount(points.Length);


            for (int i = 0; i < points.Length; i++)
            {
                lineRenderer.SetPosition(i, new Vector3(points[i].x, zIndex, points[i].y));
            }

            var width = rule.GetWidth(way);
            lineRenderer.SetWidth(width, width);

            return gameObject;
        }
    }
}