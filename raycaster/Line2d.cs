using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raycaster
{
    struct Line2d
    {
        public Point2d a;
        public Point2d b;
        public double length;
        public Line2d(double ax, double ay, double bx, double by)
        {
            this.a = new Point2d(ax, ay);
            this.b = new Point2d(bx, by);
            this.length = this.a.DistanceTo(b);
        }
        public Line2d(Point2d a, Point2d b)
        {
            this.a = a;
            this.b = b;
            this.length = this.a.DistanceTo(b);
        }
    }
}
