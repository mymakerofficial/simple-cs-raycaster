using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raycaster
{
    struct Vector2d
    {
        public Point2d pos;
        public Direction dir;
        public double vel;
        public Vector2d(Point2d p, Direction d, double vel)
        {
            this.pos = p;
            this.dir = d;
            this.vel = vel;
        }
    }
}
