using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;
using System.Windows.Input;
using System.Diagnostics;

namespace raycaster
{
    static class Game
    {
        public static Player player;
        public static List<Map> maps = new List<Map>();
        public static List<Viewport> viewports = new List<Viewport>();
        public static Map activeMap;
        public static Viewport activeViewport;
        public static List<Texture> textures = new List<Texture>();
        public static System.Timers.Timer tickTimer;
        public static int frameCount = 0;
        public static int tickCount = 0;
        public static bool active = true;
        public static bool fullscreen = false;
        public static GameObject testObject;
        public static void Create()
        {
            maps.Clear();
            viewports.Clear();
            activeMap = null;
            activeViewport = null;
            frameCount = 0;

            new Texture(new Bitmap(32,32), "none");

            new Texture(Properties.Resources.test, "test");
            new Texture(Properties.Resources.alpha, "alpha");
            new Texture(Properties.Resources.wall, "wall");
            new Texture(Properties.Resources.mossywall, "mossywall");
            new Texture(Properties.Resources.wallbadge, "wallbadge");
            new Texture(Properties.Resources.wallbanner, "wallbanner");
            new Texture(Properties.Resources.lamp, "lamp");
            new Texture(Properties.Resources.vase, "vase");
            new Texture(Properties.Resources.blob, "blob");
            new Texture(Properties.Resources.grass, "grass");
            new Texture(Properties.Resources.bush, "bush");
            new Texture(Properties.Resources.tree, "tree");
            new Texture(Properties.Resources.camera, "camera");
            new Texture(Properties.Resources.projectile, "projectile");
            new Texture(Properties.Resources.amobox, "amobox");

            testObject = new GameObject(new Vector2d(new Point2d(0, 0), new Direction(0), 0), Texture.Get("test"));
            
            new Map();
            new Map();
            new Map();

            new Viewport(new Point2d(100, 50), Direction.FromDeg(0));
            /*
            new Viewport(new Point2d(50, 50), Direction.FromDeg(0));
            new Viewport(new Point2d(50, 50), Direction.FromDeg(0), 100, 100);
            new Viewport(new Point2d(50, 50), Direction.FromDeg(0)).fov = 90;
            new Viewport(new Point2d(50, 50), Direction.FromDeg(0), 400, 200).fov = 120;
            */

            maps[1].SetToActive();

            maps[0].walls.Add(new Wall(0, 0, 500, 0, Texture.Get("wall")));
            maps[0].walls.Add(new Wall(0, 0, 0, 500, Texture.Get("wall")));
            maps[0].walls.Add(new Wall(0, 500, 500, 500, Texture.Get("wall")));
            maps[0].walls.Add(new Wall(500, 0, 500, 500, Texture.Get("wall")));

            maps[0].walls.Add(new Wall(0, 100, 400, 100, Texture.Get("wall")));

            maps[0].walls.Add(new Wall(200, 0, 200, 20, Texture.Get("wall")));
            maps[0].walls.Add(new Wall(200, 20, 232, 20, Texture.Get("wallbadge")));
            maps[0].walls.Add(new Wall(232, 0, 232, 20, Texture.Get("wall")));

            maps[0].walls.Add(new Wall(200, 100, 200, 80, Texture.Get("wall")));
            maps[0].walls.Add(new Wall(200, 80, 232, 80, Texture.Get("wallbadge")));
            maps[0].walls.Add(new Wall(232, 100, 232, 80, Texture.Get("wall")));

            maps[0].walls.Add(new Wall(300, 0, 300, 20, Texture.Get("wall")));
            maps[0].walls.Add(new Wall(300, 20, 332, 20, Texture.Get("wallbanner")));
            maps[0].walls.Add(new Wall(332, 0, 332, 20, Texture.Get("wall")));

            maps[0].walls.Add(new Wall(300, 100, 300, 80, Texture.Get("wall")));
            maps[0].walls.Add(new Wall(300, 80, 332, 80, Texture.Get("wallbanner")));
            maps[0].walls.Add(new Wall(332, 100, 332, 80, Texture.Get("wall")));

            //maps[0].objects.Add(new Object(new Vector2d(new Point2d(366, 50), new Direction(0), 0), Texture.Get("amobox")));

            maps[0].objects.Add(new GameObject(new Vector2d(new Point2d(266, 10), new Direction(0), 0), Texture.Get("vase")));
            maps[0].objects.Add(new GameObject(new Vector2d(new Point2d(266, 90), new Direction(0), 0), Texture.Get("vase")));
            maps[0].objects.Add(new GameObject(new Vector2d(new Point2d(266, 50), new Direction(0), 0), Texture.Get("lamp")));
            maps[0].objects.Add(new GameObject(new Vector2d(new Point2d(366, 50), new Direction(0), 0), Texture.Get("lamp")));
            


            maps[1].floorTexture = Texture.Get("grass");

            maps[1].walls.Add(new Wall(-250, -250, 250, -250, Texture.Get("mossywall")));
            maps[1].walls.Add(new Wall(-250, -250, -250, 250, Texture.Get("mossywall")));
            maps[1].walls.Add(new Wall(-250, 250, 250, 250, Texture.Get("mossywall")));
            maps[1].walls.Add(new Wall(250, -250, 250, 250, Texture.Get("mossywall")));

            maps[1].objects.Add(new GameObject(new Vector2d(new Point2d(100, 0), new Direction(0), 0), Texture.Get("bush")));
            maps[1].objects.Add(new GameObject(new Vector2d(new Point2d(100, 100), new Direction(0), 0), Texture.Get("bush")));
            maps[1].objects.Add(new GameObject(new Vector2d(new Point2d(125, 0), new Direction(0), 0), Texture.Get("tree")));
            maps[1].objects.Add(new GameObject(new Vector2d(new Point2d(125, 100), new Direction(0), 0), Texture.Get("tree")));
            maps[1].objects.Add(new GameObject(new Vector2d(new Point2d(150, 0), new Direction(0), 0), Texture.Get("tree")));
            maps[1].objects.Add(new GameObject(new Vector2d(new Point2d(150, 100), new Direction(0), 0), Texture.Get("tree")));
            maps[1].objects.Add(new GameObject(new Vector2d(new Point2d(175, 0), new Direction(0), 0), Texture.Get("tree")));
            maps[1].objects.Add(new GameObject(new Vector2d(new Point2d(175, 100), new Direction(0), 0), Texture.Get("tree")));
            maps[1].objects.Add(new GameObject(new Vector2d(new Point2d(200, 0), new Direction(0), 0), Texture.Get("tree")));
            maps[1].objects.Add(new GameObject(new Vector2d(new Point2d(200, 100), new Direction(0), 0), Texture.Get("tree")));

            maps[1].objects.Add(new Enemy(new Vector2d(new Point2d(100, 50), new Direction(0), 0), Texture.Get("camera")));

            maps[2].walls.Add(new Wall(0, 0, 500, 0, Texture.Get("wall")));
            maps[2].walls.Add(new Wall(0, 0, 0, 500, Texture.Get("wall")));
            maps[2].walls.Add(new Wall(0, 500, 500, 500, Texture.Get("wall")));
            maps[2].walls.Add(new Wall(500, 0, 500, 500, Texture.Get("wall")));

            maps[0].objects.Add(testObject);
            maps[1].objects.Add(testObject);
            maps[2].objects.Add(testObject);



            player = new Player(new Vector2d(new Point2d(50, 50), new Direction(0), 0), Texture.Get("none"));
            maps[0].objects.Add(player);
            maps[1].objects.Add(player);
            maps[2].objects.Add(player);

            player.viewport.SetToActive();

            Game.activeViewport = player.viewport;
        }
        public static void Start()
        {
            tickTimer = new System.Timers.Timer(50);
            tickTimer.Elapsed += tick;
            tickTimer.AutoReset = true;
            tickTimer.Enabled = true;

            Renderer.Start();
            //Program.gameWindow.Update();
        }
        public static void tick(object sender, ElapsedEventArgs e)
        {
            foreach (var o in Game.activeMap.objects)
            {
                o.Update();
            }
            Game.tickCount++;
        }
    }
}