using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raycaster
{
    class Projectile : GameObject
    {
        public Projectile(Vector2d v, Texture t)
        {
            this.vec = new Vector2d(new Point2d(v.pos.x + v.dir.x * 10, v.pos.y + v.dir.y * 10),v.dir,v.vel);
            this.texture = t;
        }
    }
}
