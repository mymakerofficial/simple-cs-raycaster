using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
//using System.Windows.Input;

namespace raycaster
{
    public partial class GameWindow : Form
    {
        public GameWindow()
        {
            InitializeComponent();
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            //this.WindowState = FormWindowState.Normal;
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
            Cursor.Hide();
        }

        private void GameWindow_Paint(object sender, PaintEventArgs e)
        {
            Game.player.Input();

            e.Graphics.DrawImage(Renderer.bitmap, 0, 0);

            if (Game.fullscreen)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
            }
            /*
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

            if(Game.activeViewport.bitmap != null)
            {
                double s = (double)Height / (double)(Game.activeViewport.bitmap.Height);
                if ((double)Game.activeViewport.bitmap.Width * s > Width) s = (double)Width / (double)Game.activeViewport.bitmap.Width;
                e.Graphics.DrawImage(Game.activeViewport.bitmap, (int)((double)Width / 2 - (double)Game.activeViewport.bitmap.Width * s / 2), (int)((double)Height / 2 - (double)Game.activeViewport.bitmap.Height * s / 2), (int)((double)Game.activeViewport.bitmap.Width * s), (int)((double)Game.activeViewport.bitmap.Height * s));
                if (!Game.active) e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(150, 0, 0, 0)), (int)((double)Width / 2 - (double)Game.activeViewport.bitmap.Width * s / 2), (int)((double)Height / 2 - (double)Game.activeViewport.bitmap.Height * s / 2), (int)((double)Game.activeViewport.bitmap.Width * s), (int)((double)Game.activeViewport.bitmap.Height * s));
            }
            */
            //e.Graphics.DrawImage(Game.activeViewport.wallsBitmap, 0, Height - Game.activeViewport.wallsBitmap.Height);
            //e.Graphics.DrawImage(Game.activeViewport.objectsBitmap, Game.activeViewport.wallsBitmap.Width, Height - Game.activeViewport.objectsBitmap.Height);

            /*
            s = (double)Height / (double)Gui.bitmap.Height;
            if ((double)Gui.bitmap.Width * s > Width) s = (double)Width / (double)Gui.bitmap.Width;
            e.Graphics.DrawImage(Gui.bitmap, (int)((double)Width / 2 - (double)Gui.bitmap.Width * s / 2), (int)((double)Height / 2 - (double)Gui.bitmap.Height * s / 2), (int)((double)Gui.bitmap.Width * s), (int)((double)Gui.bitmap.Height * s));
            */

            /*
            e.Graphics.DrawString("activeViewportIndex: " + Game.viewports.IndexOf(Game.activeViewport) + " / " + Game.viewports.Count(), new Font("Arial", 12), new SolidBrush(Color.Yellow), 10, 10, new StringFormat());
            e.Graphics.DrawString("x: " + Math.Round(Game.activeViewport.vec.pos.x) + " y:" + Math.Round(Game.activeViewport.vec.pos.y) + " deg:" + Math.Round(Game.activeViewport.vec.dir.deg), new Font("Arial", 12), new SolidBrush(Color.Yellow), 10, 30, new StringFormat());
            e.Graphics.DrawString("x: " + Math.Round(Game.player.vec.pos.x) + " y:" + Math.Round(Game.player.vec.pos.y) + " deg:" + Math.Round(Game.player.vec.dir.deg), new Font("Arial", 12), new SolidBrush(Color.White), 200, 30, new StringFormat());
            e.Graphics.DrawString("width: " + Game.activeViewport.bitmap.Width + " height:" + Game.activeViewport.bitmap.Height, new Font("Arial", 12), new SolidBrush(Color.Yellow), 10, 50, new StringFormat());
            e.Graphics.DrawString("renderTextures: " + Game.activeViewport.renderTextures + " fov: " + Game.activeViewport.fov, new Font("Arial", 12), new SolidBrush(Color.Yellow), 10, 70, new StringFormat());
            e.Graphics.DrawString("screenWidth: " + Width + " screenHeight: " + Height, new Font("Arial", 12), new SolidBrush(Color.LightBlue), 10, 100, new StringFormat());

            e.Graphics.DrawString("activeMapIndex: " + Game.maps.IndexOf(Game.activeMap), new Font("Arial", 12), new SolidBrush(Color.LightGreen), 10, 130, new StringFormat());
            e.Graphics.DrawString("walls: " + Game.activeMap.walls.Count + " objects: " + Game.activeMap.objects.Count, new Font("Arial", 12), new SolidBrush(Color.LightGreen), 10, 150, new StringFormat());

            if (Game.activeViewport.rays.Count > 10)
            {
                Wall facingWall = Game.activeViewport.rays[Game.activeViewport.rays.Count / 2].intersectingWall;

                e.Graphics.DrawString("Facing Wall: ", new Font("Arial", 12), new SolidBrush(Color.Orange), 10, 180, new StringFormat());
                if (facingWall != null)
                {
                    e.Graphics.DrawString("aX: " + Math.Round(facingWall.line.a.x) + " aY: " + Math.Round(facingWall.line.a.y) + " bX: " + Math.Round(facingWall.line.b.x) + " bY: " + Math.Round(facingWall.line.b.y), new Font("Arial", 12), new SolidBrush(Color.Orange), 10, 200, new StringFormat());
                    e.Graphics.DrawString("dist: " + Game.activeViewport.rays[Game.activeViewport.rays.Count / 2].vec.pos.DistanceTo(Game.activeViewport.rays[Game.activeViewport.rays.Count / 2].intersectPoint), new Font("Arial", 12), new SolidBrush(Color.Orange), 10, 220, new StringFormat());
                    e.Graphics.DrawString("texture: " + facingWall.texture.name, new Font("Arial", 12), new SolidBrush(Color.Orange), 10, 240, new StringFormat());
                }
                else
                {
                    e.Graphics.DrawString("null", new Font("Arial", 12), new SolidBrush(Color.Orange), 10, 200, new StringFormat());
                }
            }
            */

            /*
            float xo = 250;
            float yo = 250;
            foreach(Wall w in Game.activeMap.walls)
            {
                e.Graphics.DrawLine(new Pen(Color.FromArgb(150,0,0,0), 5), (float)w.line.a.x + xo, (float)w.line.a.y + yo, (float)w.line.b.x + xo, (float)w.line.b.y + yo);
                e.Graphics.DrawLine(new Pen(w.texture.color), (float)w.line.a.x + xo, (float)w.line.a.y + yo, (float)w.line.b.x + xo, (float)w.line.b.y + yo);
            }

            foreach (var o in Game.activeMap.objects)
            {
                System.Reflection.FieldInfo[] fields = o.GetType().GetFields();

                Hitbox hitbox = null;

                foreach(System.Reflection.FieldInfo f in fields)
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
                    e.Graphics.DrawImage(o.texture.image, x - o.texture.image.Width / 2, y - o.texture.image.Height / 2);
                    e.Graphics.DrawLine(new Pen(Color.FromArgb(200, 0, 0, 0), 3), x, y, x + (float)(o.vec.dir.x * 16), y + (float)(o.vec.dir.y * 16));

                    //if (hitbox != null) e.Graphics.DrawArc(new Pen(Color.FromArgb(255, 255, 0, 0), 2), x - (float)hitbox.r/2, y - (float)hitbox.r / 2, (float)hitbox.r, (float)hitbox.r, 0, 360);
                }
            }
            */
        }


        private void GameWindow_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //Game.Move();
            switch (e.KeyCode)
            {
                case Keys.C:
                    Game.viewports[(Game.viewports.IndexOf(Game.activeViewport) + 1) % Game.viewports.Count].SetToActive();
                    break;
                case Keys.M:
                    Game.maps[(Game.maps.IndexOf(Game.activeMap) + 1) % Game.maps.Count].SetToActive();
                    break;
                case Keys.R:
                    Game.Create();
                    break;
                case Keys.T:
                    Game.activeViewport.renderTextures = !Game.activeViewport.renderTextures;
                    break;
                case Keys.Escape:
                    Game.active = !Game.active;
                    if (Game.active) { Cursor.Hide(); } else { Cursor.Show(); }
                    break;
                case Keys.F11:
                    Game.fullscreen = !Game.fullscreen;
                    break;
                case Keys.F12:
                    Renderer.Print();
                    break;
            }
        }

        private void GameWindow_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (Game.active)
            {
                Game.player.vec.dir = Direction.FromDeg(Game.player.vec.dir.deg - (this.Location.X + this.Size.Width / 2 - Cursor.Position.X) / 10);
                Cursor.Position = new Point(this.Location.X + this.Size.Width / 2, this.Location.Y + this.Size.Height / 2);
            }
        }

        private void GameWindow_MouseDown(object sender, MouseEventArgs e)
        {
            Game.player.Shoot();
        }
    }
}
