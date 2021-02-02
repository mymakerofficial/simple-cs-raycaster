using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raycaster
{
    class Character : GameObject
    {
        public int maxHealth = 0;
        public int health = 0;
        public double speed = 4;
        public int ammoCount = 10;
        public Viewport viewport;
        public Character() { }
        public Character(Vector2d v, Texture t)
        {
            this.vec = v;
            this.texture = t;
            hitbox = new Hitbox(16);
        }
        public override void Update()
        {
            this.hitbox.pos = this.vec.pos;
            if (this.viewport != null) this.viewport.vec = this.vec;
        }
        public void Move(double z, double x)
        {
            this.vec.pos.x += this.vec.dir.x * z;
            this.vec.pos.y += this.vec.dir.y * z;
            this.vec.pos.x += Direction.FromDeg(this.vec.dir.deg + 90).x * x;
            this.vec.pos.y += Direction.FromDeg(this.vec.dir.deg + 90).y * x;
        }
        public void Shoot()
        {
            if(ammoCount > 0)
            {
                Game.activeMap.objects.Add(new Projectile(new Vector2d(this.vec.pos, this.vec.dir, 3), Texture.Get("projectile")));
                this.ammoCount--;
            }
        }
    }
}
