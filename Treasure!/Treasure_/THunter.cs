using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Treasure_
{
    public class THunter
    {
        public int number;
        public static Random rnd = new Random();
        public int homeAmmunition = 15;
        public int ammunition = 3;
        public bool wasKilled = false;
        public Vector2 pPos = new Vector2();
        public bool haveTreasure = false;
        public Color color  = new Color(rnd.Next() % 256, rnd.Next() % 256, rnd.Next() % 256, 256);
        public List <string> hodi = new List<string>();
        public List<int> killedPlayersInt = new List<int>();
        public THunter()
        {
            hodi.Add("Start!");
        }
    }
}
