using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Timers;
using System.IO;

namespace raycaster
{
    static class Renderer
    {
        public static Bitmap bitmap = new Bitmap(816, 489);
        public static Graphics g;
        public static int width = 816;
        public static int height = 489;
        public static System.Timers.Timer timer;
        public static void Start()
        {
            timer = new System.Timers.Timer(50);
            timer.Elapsed += Render;
            timer.AutoReset = true;
            timer.Enabled = true;

            bitmap = new Bitmap(width, height);
            g = Graphics.FromImage(bitmap);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
        }
        public static void Render(object sender, ElapsedEventArgs e)
        {
            Game.activeViewport.Render();
            Gui.Render();

            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, width, height);

            double s = (double)height / (double)(Game.activeViewport.bitmap.Height);
            if ((double)Game.activeViewport.bitmap.Width * s > width) s = (double)width / (double)Game.activeViewport.bitmap.Width;

            g.DrawImage(Game.activeViewport.bitmap, (int)((double)width / 2 - (double)Game.activeViewport.bitmap.Width * s / 2), (int)((double)height / 2 - (double)Game.activeViewport.bitmap.Height * s / 2), (int)((double)Game.activeViewport.bitmap.Width * s), (int)((double)Game.activeViewport.bitmap.Height * s));

            //g.DrawImage(Gui.bitmap, (int)((double)width / 2 - (double)Game.activeViewport.bitmap.Width * s / 2), (int)((double)height / 2 - (double)Game.activeViewport.bitmap.Height * s / 2), (int)((double)Game.activeViewport.bitmap.Width * s), (int)((double)Game.activeViewport.bitmap.Height * s));


            //Program.gameWindow.Invalidate();
            //Program.gameWindow.Update();

            //DebugOverlay();
        }
        public static void Print()
        {
            bitmap.Save("screenshot.png", ImageFormat.Png);
        }
        public static void DebugOverlay()
        {
            float xo = 250;
            float yo = 250;
            foreach (Wall w in Game.activeMap.walls)
            {
                g.DrawLine(new Pen(Color.FromArgb(150, 0, 0, 0), 5), (float)w.line.a.x + xo, (float)w.line.a.y + yo, (float)w.line.b.x + xo, (float)w.line.b.y + yo);
                g.DrawLine(new Pen(w.texture.color), (float)w.line.a.x + xo, (float)w.line.a.y + yo, (float)w.line.b.x + xo, (float)w.line.b.y + yo);
            }

            foreach (var o in Game.activeMap.objects)
            {
                System.Reflection.FieldInfo[] fields = o.GetType().GetFields();

                Hitbox hitbox = null;

                foreach (System.Reflection.FieldInfo f in fields)
                {
                    var v = f.GetValue(o);
                    if (f.Name == "hitbox")
                    {
                        hitbox = (Hitbox)v;
                    }
                }
                int x = (int)o.vec.pos.x + (int)xo;
                int y = (int)o.vec.pos.y + (int)yo;
                if (x > 0 && y > 0)
                {
                    g.DrawImage(o.texture.image, x - o.texture.image.Width / 2, y - o.texture.image.Height / 2);
                    g.DrawLine(new Pen(Color.FromArgb(200, 255, 0, 0), 3), x, y, x + (float)(o.vec.dir.x * 16), y + (float)(o.vec.dir.y * 16));

                    //if (hitbox != null) e.Graphics.DrawArc(new Pen(Color.FromArgb(255, 255, 0, 0), 2), x - (float)hitbox.r/2, y - (float)hitbox.r / 2, (float)hitbox.r, (float)hitbox.r, 0, 360);
                }
            }
        }
    }
}
