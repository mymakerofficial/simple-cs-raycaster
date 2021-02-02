using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raycaster
{
    class Hitbox
    {
        public Point2d pos;
        public double r = 4;
        public Hitbox(double r)
        {
            this.r = r;
        }
        public List<Point2d> IntersectingWall()
        {
            List<Point2d> points = new List<Point2d>();

            foreach(Wall w in Game.activeMap.walls)
            {
                Point2d p = this.pos.PerpendicularPointOnLine(w.line);
                if (p.DistanceTo(this.pos) < r) points.Add(p); 
            }

            return points;
        }
    }
}
