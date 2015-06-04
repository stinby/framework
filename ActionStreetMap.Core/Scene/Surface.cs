﻿using System.Collections.Generic;

namespace ActionStreetMap.Core.Scene
{
    /// <summary> Represents surface. </summary>
    public class Surface
    {
        /// <summary> Gets or sets gradient key. </summary>
        public string GradientKey { get; set; }

        /// <summary> Gets or sets map points for this surcafe. </summary>
        public List<MapPoint> Points { get; set; }

        /// <summary> Gets or sets points for holes inside this surcafe. </summary>
        public List<List<MapPoint>> Holes { get; set; }

        /// <summary> Gets or sets elevation noise. </summary>
        internal float ElevationNoise { get; set; }

        /// <summary> Gets or sets color noise.  </summary>
        internal float ColorNoise { get; set; }
    }
}
