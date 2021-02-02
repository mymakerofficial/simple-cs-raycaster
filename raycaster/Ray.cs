using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raycaster
{
    class Ray
    {
        public Vector2d vec;
        public Point2d intersectPoint = new Point2d(0, 0);
        public Wall intersectingWall;
        public bool intersecting = false;
        public Ray(Point2d pos, Direction dir)
        {
            this.vec = new Vector2d(pos, dir, 0);
        }
        public void Set(Point2d pos, Direction dir)
        {
            this.vec = new Vector2d(pos, dir, 0);
            this.intersectPoint = new Point2d(0, 0);
            this.intersectingWall = null;
            this.intersecting = false;
        }
        public bool Cast(Wall wall)
        {
            double x1 = wall.line.a.x;
            double y1 = wall.line.a.y;
            double x2 = wall.line.b.x;
            double y2 = wall.line.b.y;

            double x3 = this.vec.pos.x;
            double y3 = this.vec.pos.y;
            double x4 = this.vec.pos.x + this.vec.dir.x;
            double y4 = this.vec.pos.y + this.vec.dir.y;

            double den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            if (den == 0) { return false; }

            double t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / den;
            double u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / den;
            if (t > 0 && t < 1 && u > 0)
            {
                Point2d i = new Point2d(x1 + t * (x2 - x1), y1 + t * (y2 - y1));
                if (!this.intersecting || i.DistanceTo(vec.pos) < this.intersectPoint.DistanceTo(vec.pos))
                {
                    this.intersectPoint = i;
                    this.intersectingWall = wall;
                }
                this.intersecting = true;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}