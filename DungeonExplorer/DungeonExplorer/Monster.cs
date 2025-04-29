using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Monster : Creature
    {
        protected Room CurrentRoom { get; set; }
        public int GiveDamage { get; set; }
        protected static Random random = new Random();
        public Monster(string name, int lowestHealth, int highestHealth, int lowestDamage, int highestDamage) : base(name, random.Next(lowestHealth, highestHealth))
        {
            GiveDamage = random.Next(lowestDamage, highestDamage);
        }
        public bool AttackDelay { get; set; } = true;
        public void DisplayEnemyHealth()
        {
            double healthPercentage = (double)Health / MaxHealth;

            if (healthPercentage > 0.7)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (healthPercentage > 0.3)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"{Name}'s Health: {Health}/{MaxHealth}");
            Console.ResetColor();
        }
    }

    public class Dragon : Monster
    {
        // 50% chance to do the first attack
        public Dragon() : base("Dragon", 160, 200, 20, 30)
        {
            AttackDelay = random.Next(2) == 1;
        }
    }

    public class CaveRat : Monster
    {
        // never does the first attack
        public CaveRat() : base("Cave Rat", 10, 20, 1, 3)
        {
            AttackDelay = true;
        }
    }

    public class Spider : Monster
    {
        // 25% chance to do the first attack
        public Spider() : base("Spider", 10, 15, 15, 25)
        {
            AttackDelay = random.Next(4) != 0;
        }
    }

    public class Bear : Monster
    {
        // 33% chance to do the first attack
        public Bear() : base("Bear", 120, 160, 12, 18)
        {
            AttackDelay = random.Next(3) == 0;
        }
    }

    public class Bandit : Monster
    {
        // 33% chance to do the first attack
        public Bandit() : base("Bandit", 60, 80, 8, 14)
        {
            AttackDelay = random.Next(3) == 1;
        }
    }

    public class Ghost : Monster
    {
        // 50% chance to do the first attack
        public Ghost() : base("Ghost", 40, 60, 6, 12)
        {
            AttackDelay = random.Next(2) == 1;
        }
    }

    public class GiantBat : Monster
    {
        // 33% chance to do the first attack
        public GiantBat() : base("Giant Bat", 20, 30, 3, 6)
        {
            AttackDelay = random.Next(3) != 0;
        }
    }

    public class Troll : Monster
    {
        // 25% chance to do the first attack
        public Troll() : base("Troll", 140, 180, 10, 18)
        {
            AttackDelay = random.Next(4) != 0;
        }
    }

    public class Assassin : Monster
    {
        // 80% chance to do the first attack
        public Assassin() : base("Assassin", 50, 70, 15, 22)
        {
            AttackDelay = random.Next(5) != 0;
        }
    }
    public class Dwarf : Monster
    {
        // 80% chance to do the first attack
        public Dwarf() : base("Dwarf", 15, 30, 5, 15)
        {
            AttackDelay = random.Next(5) != 0;
        }
    }
}

