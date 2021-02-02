using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace raycaster
{
    public partial class DebugWindow : Form
    {
        public PaintEventArgs paintE;
        public DebugWindow()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
        }

        private void DebugWindow_Load(object sender, EventArgs e)
        {

        }

        private void DebugWindow_Paint(object sender, PaintEventArgs e)
        {
            int y = 0;
            int x = 0;
            int sx = 0;
            for (int i = Math.Max(0, Game.activeMap.objects.Count - 20); i < Game.activeMap.objects.Count; i++)
            {
                GameObject obj = Game.activeMap.objects[i];
                x = 0 + sx * 20;
                e.Graphics.DrawString(i + " [" + obj.ToString() + "]", new Font("Arial", 12), new SolidBrush(Color.White), x * 20, y * 20, new StringFormat());
                System.Reflection.FieldInfo[] fields = obj.GetType().GetFields();
                y++; x++;
                e.Graphics.DrawString(obj.vec.ToString(), new Font("Arial", 12), new SolidBrush(Color.White), x * 20, y * 20, new StringFormat());
                y++; x++;
                e.Graphics.DrawString("pos.x = " + obj.vec.pos.x + " pos.y = " + obj.vec.pos.y, new Font("Arial", 12), new SolidBrush(Color.White), x * 20, y * 20, new StringFormat());
                y++;
                e.Graphics.DrawString("dir.deg = " + obj.vec.dir.deg, new Font("Arial", 12), new SolidBrush(Color.White), x * 20, y * 20, new StringFormat());
                y++;
                e.Graphics.DrawString("vel = " + obj.vec.vel, new Font("Arial", 12), new SolidBrush(Color.White), x * 20, y * 20, new StringFormat());
                y++; x--;
                e.Graphics.DrawString(obj.texture.ToString(), new Font("Arial", 12), new SolidBrush(Color.White), x * 20, y * 20, new StringFormat());
                y++; x++;
                e.Graphics.DrawString("name = " + obj.texture.name, new Font("Arial", 12), new SolidBrush(Color.White), x * 20, y * 20, new StringFormat());
                y++;
                if (y * 20 > this.Height - 100) { y = 0; sx++; }
            }
        }
    }
}
