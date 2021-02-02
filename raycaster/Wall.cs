using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace raycaster
{
    class Wall
    {
        public Line2d line;
        public Color color;
        public Texture texture;
        public string id;
        public Wall(double x1, double y1, double x2, double y2, Texture texture)
        {
            this.Set(new Line2d(new Point2d(x1, y1), new Point2d(x2, y2)), texture);
        }
        public Wall(Point2d vector1, Point2d vector2, Texture texture)
        {
            this.Set(new Line2d(vector1, vector2), texture);
        }
        public Wall(double x1, double y1, double x2, double y2)
        {
            this.Set(new Line2d(new Point2d(x1, y1), new Point2d(x2, y2)), Texture.Get("alpha"));
        }
        public Wall(Point2d vector1, Point2d vector2)
        {
            this.Set(new Line2d(vector1, vector2), Texture.Get("alpha"));
        }

        public void Set(Line2d line, Texture texture)
        {
            this.line = line;
            this.texture = texture;
            this.color = texture.color;
        }

        public static Wall GetById(string id)
        {
            foreach (Wall w in raycaster.Game.activeMap.walls)
            {
                if (w.id == id) return w;
            }
            return null;
        }
    }
}
