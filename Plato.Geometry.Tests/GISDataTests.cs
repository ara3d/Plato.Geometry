using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

// for [Test] attribute, remove if not using NUnit

namespace Plato.Geometry.Tests
{
    /// <summary>
    /// DataPoint holds coordinate + value info.
    /// </summary>
    public class DataPoint
    {
        public double Longitude;
        public double Latitude;
        public int Col;
        public int Row;
        public double Elevation;
    }
}

    
