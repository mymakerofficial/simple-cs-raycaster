using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raycaster
{
    struct Point2d
    {
        public double x;
        public double y;

        public Point2d(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double DistanceTo(double x, double y)
        {
            return Math.Sqrt(Math.Pow(this.x - x, 2) + Math.Pow(this.y - y, 2));
        }
        public double DistanceTo(Point2d p)
        {
            return this.DistanceTo(p.x, p.y);
        }
        public Point2d PerpendicularPointOnLine(Line2d line)
        {
            Point2d d = new Point2d(0,0);

            Line2d ac = new Line2d(this,line.a);
            
            

            return d;
        }
    }
}
