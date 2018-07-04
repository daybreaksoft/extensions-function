using System;
using System.Collections.Generic;
using System.Text;

namespace Daybreaksoft.Extensions.Functions.Tests.Requires
{
    public class Model1
    {
        [Attribute1]
        public string P1 { get; set; }

        [Attribute2]
        public string P2 { get; set; }

        [Attribute2]
        public string P3 { get; set; }

        [Attribute2]
        public string P4 { get; set; }

        public string P5 { get; set; }

        public int P6 { get; set; }

        public string PP { get; set; }

        public Model3 P8 { get; set; }

        public List<Model4> P9 { get; set; }

        public object P10 { get; set; }
    }
}
