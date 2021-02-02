using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raycaster
{
    class GameObject
    {
        public Texture texture;
        public Vector2d vec;
        public Hitbox hitbox;
        public GameObject() { }
        public GameObject(Vector2d v, Texture t)
        {
            vec = v;
            texture = t;
        }
        public virtual void Update()
        {
            UpdatePosition();
        }
        public void UpdatePosition()
        {
            vec.pos.x += vec.dir.x * vec.vel;
            vec.pos.y += vec.dir.y * vec.vel;
        }
    }
}
