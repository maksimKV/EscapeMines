using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Point : IEquatable<Point>
    {
        public int x { get; set; }
        public int y { get; set; }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(Point toCompare)
        {
            return this.x == toCompare.x &&
                   this.y == toCompare.y;
        }
    }
}
