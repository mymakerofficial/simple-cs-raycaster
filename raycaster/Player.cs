using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace raycaster
{
    class Player : Character
    {
        public Player(Vector2d v, Texture t)
        {
            speed = 1;
            vec = v;
            texture = t;
            viewport = new Viewport(this.vec);
            viewport.SetToActive();
        }
        public override void Update()
        {
            if (viewport != null) viewport.vec = vec;
            if (hitbox != null) hitbox.pos = vec.pos;
        }
        public void Input()
        {
            this.Move(
                (Keyboard.IsKeyDown(System.Windows.Input.Key.W) ? this.speed : 0) +
                (Keyboard.IsKeyDown(System.Windows.Input.Key.S) ? -this.speed : 0),
                (Keyboard.IsKeyDown(System.Windows.Input.Key.D) ? this.speed : 0) +
                (Keyboard.IsKeyDown(System.Windows.Input.Key.A) ? -this.speed : 0)
            );
        }
    }
}
