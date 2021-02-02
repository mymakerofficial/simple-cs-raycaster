using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace raycaster
{
    static class Gui
    {
        public static int width = 220;
        public static int height = 176;
        public static Bitmap bitmap;
        public static Bitmap Render()
        {
            Bitmap bm = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bm);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            for(int i = 1;i <= Game.player.ammoCount; i++)
            {
                g.DrawImage(Properties.Resources.amobar, 8, height - i * 8 - 16);
            }

            for (int i = 1; i <= 10; i++)
            {
                g.DrawImage(Properties.Resources.healthbar, width - 24, height - i * 8 - 16);
            }

            g.Dispose();

            bitmap = bm;
            return bitmap;
        }
    }
}
