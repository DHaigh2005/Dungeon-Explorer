using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Monster : Creature
    {
        public Room CurrentRoom { get; set; }
        public int GiveDamage { get; set; }
        public static Random random = new Random();
        public Monster(string name, int lowestHealth, int highestHealth, int lowestDamage, int highestDamage) : base(name, random.Next(lowestHealth, highestHealth))
        {

        }
    }
    {
    }
}
