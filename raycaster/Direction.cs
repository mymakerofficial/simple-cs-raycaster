using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raycaster
{
    struct Direction
    {
        public double rad;
        public double deg;
        public double x;
        public double y;
        public Direction(double deg)
        {
            this.deg = deg % 360;
            this.rad = this.deg * Math.PI / 180;
            this.x = Math.Cos(this.rad);
            this.y = Math.Sin(this.rad);
        }
        public static Direction FromDeg(double deg)
        {
            return new Direction(deg);
        }
        public static Direction FromRad(double rad)
        {
            return new Direction(rad * 180 / Math.PI);
        }
        public static Direction FromPos(double x1, double y1, double x2, double y2)
        {
            return Direction.FromRad(Math.Atan2(x2 - x1, y2 - y1));
        }
        public static Direction FromPos(Point2d p1, Point2d p2)
        {
            return Direction.FromRad(Math.Atan2(p2.x - p1.x, p2.y - p1.y));
        }
    }
}
