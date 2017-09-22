using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;

namespace Treasure_
{
    public class Tile
    {
        public int type = 2;
        public int number = 0;
        public bool up = false;
        public bool down = false;
        public bool left = false;
        public bool right = false;
        public Tile nextRiver;
        public Vector2 position = new Vector2();
    }
}
