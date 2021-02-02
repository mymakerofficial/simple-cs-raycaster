using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace raycaster
{
    class Texture
    {
        public Image image;
        public Image[] imageShades = new Image[256];
        public Bitmap repeatedImage;
        public Color color;
        public string name;
        public Texture(Image image, string name)
        {
            raycaster.Game.textures.Add(this);
            //this.image = Image.FromStream(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("raycaster.img."+file));
            this.name = name;
            this.image = image;

            this.repeatedImage = new Bitmap(this.image.Width * 10, this.image.Height * 10);
            Graphics g = Graphics.FromImage(this.repeatedImage);
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    g.DrawImage(this.image, x * this.image.Width, y * this.image.Height);
                }
            }

            int re = 0;
            int gr = 0;
            int bl = 0;
            for (int y = 0; y < this.image.Height; y++)
            {
                for (int x = 0; x < this.image.Width; x++)
                {
                    Color c = this.repeatedImage.GetPixel(x, y);
                    re += (int)c.R;
                    gr += (int)c.G;
                    bl += (int)c.B;
                }
            }
            this.color = Color.FromArgb(255, re / (this.image.Height * this.image.Height), gr / (this.image.Height * this.image.Height), bl / (this.image.Height * this.image.Height));
            g.Dispose();

            for(double i = 0;i <= 255; i++)
            {
                imageShades[(int)i] = Adjust(1, 1, 2 - (float)(i)/255);//2 - ((float)(-0.00392 * Math.Pow(i - 255, 2) + 255) / 255)
            }
        }
        public static Texture Get(string name)
        {
            foreach (Texture t in raycaster.Game.textures)
            {
                if (t.name == name) return t;
            }
            return Texture.Get("alpha");
        }
        public Image Adjust(float brightness, float contrast, float gamma)
        {
            Image originalImage = image;
            Bitmap adjustedImage = new Bitmap(image.Width, image.Height);
            //float brightness = 1.0f; // no change in brightness

            float adjustedBrightness = brightness - 1.0f;
            // create matrix that will brighten and contrast the image
            float[][] ptsArray ={
            new float[] {contrast, 0, 0, 0, 0}, // scale red
            new float[] {0, contrast, 0, 0, 0}, // scale green
            new float[] {0, 0, contrast, 0, 0}, // scale blue
            new float[] {0, 0, 0, 1.0f, 0}, // don't scale alpha
            new float[] {adjustedBrightness, adjustedBrightness, adjustedBrightness, 0, 1}};

            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.ClearColorMatrix();
            imageAttributes.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            imageAttributes.SetGamma(gamma, ColorAdjustType.Bitmap);
            Graphics g = Graphics.FromImage(adjustedImage);
            g.DrawImage(originalImage, new Rectangle(0, 0, adjustedImage.Width, adjustedImage.Height), 0, 0, originalImage.Width, originalImage.Height,GraphicsUnit.Pixel, imageAttributes);
            return adjustedImage;
        }
    }
}