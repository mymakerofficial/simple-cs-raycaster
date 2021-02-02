using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace raycaster
{
    class Enemy : Character
    {
        public Enemy(Vector2d v, Texture t)
        {
            this.vec = v;
            this.texture = t;
            hitbox = new Hitbox(16);
        }
        public override void Update()
        {
            this.AIcontroll();
        }
        public void AIcontroll()
        {
            this.vec.dir = Direction.FromDeg(-Direction.FromPos(this.vec.pos, Game.player.vec.pos).deg + 90);
            //this.Shoot();
            if(this.vec.pos.DistanceTo(Game.player.vec.pos) > 124)
            {
                this.Move(3, 0);
            }
        }
    }
}
