using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class Creature: IDamageable
    {
        public string Name { get; set; }
        protected int Health { get; set; }
        public Creature(string name, int health)
        {
            Name = name;
            Health = health;
        }

        public int ReturnHealth()
        {
            return Health;
        }

        public void RecieveDamage(int damage)
        {
            Health -= damage;
        }

        public bool NotDead()
        {
            return Health > 0;
        }
    }
}
