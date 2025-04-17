using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class Item
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public Item(string name, string type, string description)
        {
            Name = name;
            Type = type;
            Description = description;
        }
    }

    public class Arms : Item
    {
        public int GiveDamage { get; set; }
        public Arms(string name, string description, int giveDamage) : base(name, "Arms", description)
        {
            GiveDamage = giveDamage;
        }

        public int ReturnGiveDamage()
        {
            return GiveDamage;
        }
    }

    public class Flask : Item
    {
        public int HealAmount { get; set; }
        public Flask(string name, string description, int healAmount) : base(name, "Flask", description)
        {
            HealAmount = healAmount;
        }
    }

    public class Miscellaneous : Item
    {
        public Miscellaneous(string name, string description) : base(name, "Miscellaneous", description)
        {

        }
    }
}
