using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raycaster
{
    class Map
    {
        public string name = "Level";
        public int number = 0;
        public List<Wall> walls = new List<Wall>();
        public List<GameObject> objects = new List<GameObject>();
        public Texture floorTexture = Texture.Get("wall");
        public Texture ceilingTexture = Texture.Get("wall");
        public Map()
        {
            Game.maps.Add(this);
        }
        public void SetToActive()
        {
            Game.activeMap = this;
        }
    }
}
