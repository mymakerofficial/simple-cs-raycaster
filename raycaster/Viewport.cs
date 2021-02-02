using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace raycaster
{
    class Viewport
    {
        public Vector2d vec;
        public int fov = 60;
        public int renderDistance = 200;
        public int width = 220;
        public int height = 176;
        public Bitmap bitmap;
        public Bitmap backgroundBitmap;
        public Bitmap wallsBitmap;
        public Bitmap objectsBitmap;
        public List<Ray> rays = new List<Ray>();
        public bool rendering = false;
        public int renderingTime = 0;
        public bool renderTextures = true;
        public Viewport(Vector2d v)
        {
            Game.viewports.Add(this);
            this.bitmap = new Bitmap(this.width, this.height);
            this.vec = v;
        }
        public Viewport(Point2d pos, Direction dir)
        {
            Game.viewports.Add(this);
            this.bitmap = new Bitmap(this.width, this.height);
            this.vec = new Vector2d(pos, dir, 0);
        }
        public Viewport(Point2d pos, Direction dir, int w, int h)
        {
            Game.viewports.Add(this);
            this.width = w;
            this.height = h;
            this.bitmap = new Bitmap(this.width, this.height);
            this.vec = new Vector2d(pos, dir, 0);
        }
        public void SetDir(Direction dir)
        {
            this.vec.dir = dir;
        }
        public void SetToActive()
        {
            Game.activeViewport = this;
        }
        public void Cast()
        {
            if (this.rays.Count != this.width)
            {
                this.rays.Clear();
                for (int i = 0; i < this.width; i++)
                {
                    this.rays.Add(new Ray(vec.pos, Direction.FromDeg(0)));
                }
            }
            for (int i = 0; i < this.rays.Count; i++)
            {
                this.rays[i].Set(this.vec.pos, Direction.FromDeg((vec.dir.deg - fov / 2) + i * ((double)this.fov / (double)this.rays.Count)));
                foreach (Wall w in Game.activeMap.walls)
                {
                    this.rays[i].Cast(w);
                }
            }
        }
        public Bitmap Render()
        {
            int startTime = new DateTime().Millisecond;
            this.Cast();
            Bitmap bm = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bm);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            this.backgroundBitmap = this.RenderBackground();
            this.wallsBitmap = this.RenderWalls();
            this.objectsBitmap = this.RenderObjects();
            g.DrawImage(this.backgroundBitmap, 0, 0);
            g.DrawImage(this.wallsBitmap, 0, 0);
            g.DrawImage(this.objectsBitmap, 0, 0);

            g.Dispose();
            int endTime = new DateTime().Millisecond;
            this.renderingTime = endTime - startTime;

            this.bitmap = bm;
            return this.bitmap;
        }
        public Bitmap RenderBackground()
        {
            Bitmap bm = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bm);

            g.FillRectangle(new SolidBrush(Game.activeMap.ceilingTexture.color), 0, 0, width, height / 2);
            g.FillRectangle(new SolidBrush(Game.activeMap.floorTexture.color), 0, height / 2, width, height / 2);
            /*
            for (int y = 0; y <=  height/2; y++)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb((int)(((double)y / ((double)height / 2)) * 255), 0, 0, 0)), 0, y, width, 1);
                g.FillRectangle(new SolidBrush(Color.FromArgb((int)(((double)y / ((double)height / 2)) * 255), 0, 0, 0)), 0, height - y, width, 1);
            }
            */

            g.Dispose();
            return bm;
        }
        public Bitmap RenderWalls()
        {
            Bitmap bm = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bm);
            for (int i = 0; i < this.rays.Count; i++)
            {
                if (rays[i].intersectingWall != null)
                {
                    Bitmap s = RenderRaySlice(rays[i]);
                    g.DrawImage(s, i, this.height / 2 - s.Height / 2);
                }
            }
            g.Dispose();
            return bm;
        }
        public Bitmap RenderRaySlice(Ray ray)
        {
            double perpDist = this.vec.pos.DistanceTo(ray.intersectPoint) * Math.Cos(this.vec.dir.rad - ray.vec.dir.rad);

            int w = 1;
            int h = (int)(((double)this.height / perpDist) * 50);

            Texture texture = ray.intersectingWall.texture;

            double textureScale = (double)h / (texture.image.Height * 2);
            double texturePos = ray.intersectPoint.DistanceTo(ray.intersectingWall.line.a);

            int a = (int)((1 - ((double)h / (double)this.height)) * 255);

            Bitmap b = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(b);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            //g.FillRectangle(new SolidBrush(ray.intersectingWall.color), 0, 0, w, h);
            //g.DrawImage(texture.repeatedImage, (int)(-texturePos % texture.image.Width * textureScale), 0, (int)(texture.repeatedImage.Width * textureScale), (int)(texture.repeatedImage.Height * textureScale));
            if(this.renderTextures && textureScale < 6) g.DrawImage(texture.imageShades[Math.Min(255,Math.Max(0,255-a))], new Rectangle(0, 0, w, h), new Rectangle((int)(texturePos % texture.image.Width), 0, texture.image.Width, texture.image.Height), GraphicsUnit.Pixel);
            //g.FillRectangle(new SolidBrush(Color.FromArgb(Math.Max(0, a), 0, 0, 0)), 0, 0, w, h);
            g.Dispose();

            return b;
        }
        public Bitmap RenderObjects()
        {
            Bitmap bm = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bm);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            List<GameObject> o = new List<GameObject>();
            o.Add(Game.activeMap.objects[0]);
            for(int i = 1; i < Game.activeMap.objects.Count; i++)
            {
                double dist = Math.Abs(this.vec.pos.DistanceTo(Game.activeMap.objects[i].vec.pos));
                int index = 0;
                for(int a = 0;a < o.Count; a++)
                {
                    if(dist < Math.Abs(this.vec.pos.DistanceTo(o[a].vec.pos)))
                    {
                        index = a+1;
                    }
                }
                o.Insert(index, Game.activeMap.objects[i]);
            }

            foreach (GameObject obj in o)
            {
                Texture texture = obj.texture;

                Direction objDir = Direction.FromPos(this.vec.pos.x, this.vec.pos.y, obj.vec.pos.x, obj.vec.pos.y);

                Direction dir = Direction.FromDeg(-objDir.deg - this.vec.dir.deg + 90);


                double dist = Math.Abs(this.vec.pos.DistanceTo(obj.vec.pos));

                int h = (int)(((double)this.height / dist) * 50);
                double textureScale = Math.Abs((double)h / (texture.image.Height * 2));
                int w = (int)((double)texture.image.Width * textureScale*2);

                int relSpriteScreenX = (int)((double)dir.deg / ((double)this.fov / (double)this.rays.Count));
                relSpriteScreenX = relSpriteScreenX > this.width ? (int)(-(360 - (double)dir.deg) / ((double)this.fov / (double)this.rays.Count)) : relSpriteScreenX;
                relSpriteScreenX = relSpriteScreenX < -this.width ? (int)((360 + (double)dir.deg) / ((double)this.fov / (double)this.rays.Count)) : relSpriteScreenX;
                int spriteScreenX = this.width / 2 + relSpriteScreenX - w/2;

                int a = (int)((1 - ((double)h / (double)this.height)) * (double)byte.MaxValue);

                for (int x = Math.Max(0,spriteScreenX - w); x < Math.Min(this.width,spriteScreenX + w); x++)
                {
                    if (this.vec.pos.DistanceTo(this.rays[x].intersectPoint) * Math.Cos(this.vec.dir.rad - this.rays[x].vec.dir.rad) > Math.Abs(dist) || texture.name == "test") g.DrawImage(texture.imageShades[Math.Min(255, Math.Max(0, 255 - a))], new Rectangle(x, this.height / 2 - h / 2, 1, h), new Rectangle((int)((double)(x - spriteScreenX) / textureScale / 2), 0, 1, texture.image.Height), GraphicsUnit.Pixel);
                }
                /*
                if (texture.name != "camera")
                {
                    g.DrawString(Math.Round(dir.deg).ToString(), new Font("Arial", 12), new SolidBrush(Color.Purple), 20, 0, new StringFormat());
                    g.DrawString(Math.Round(objDir.deg).ToString(), new Font("Arial", 12), new SolidBrush(Color.Purple), 50, 0, new StringFormat());
                    g.DrawString(Math.Round(dist).ToString(), new Font("Arial", 12), new SolidBrush(Color.Purple), 20, 20, new StringFormat());
                    g.DrawString(spriteScreenX.ToString(), new Font("Arial", 12), new SolidBrush(Color.Purple), 20, 40, new StringFormat());
                    g.DrawString(relSpriteScreenX.ToString(), new Font("Arial", 12), new SolidBrush(Color.Blue), 20, 60, new StringFormat());
                }
                */
                //g.DrawImage(sprite.texture.image, spriteScreenX - w / 2, this.height / 2 - h / 2, w, h);
                //g.DrawImage(texture.repeatedImage, new Rectangle(0, 0, w, h), new Rectangle((int)(texturePos % texture.image.Width), 0, texture.image.Width, texture.image.Height), GraphicsUnit.Pixel);
            }

            g.Dispose();
            return bm;
        }
    }
}