using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class Creature: IDamageable
    {
        public string Name { get; set; }
        protected int Health { get; set; }
        protected int MaxHealth { get; set; }
        public Creature(string name, int health)
        {
            Name = name;
            Health = health;
            MaxHealth = health;
        }


        public bool NotDead()
        {
            return Health > 0;
        }
        public void ReceiveDamage(int damage)
        {
            Health -= damage;
        }
        public int ReturnHealth()
        {
            return Health;
        }
        public void DisplayHealth()
        {
            double healthPercentage = (double)Health / MaxHealth;

            if (healthPercentage > 0.7)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (healthPercentage > 0.3)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"{Name} Health: {Health}/{MaxHealth}");
            Console.ResetColor();
        }
    }
}
    

