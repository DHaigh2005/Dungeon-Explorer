using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ThingHappened { get; set; } = false;

        public List<Item> Item { get; set; }
        public List<Creature> Creature { get; set; }
        public Dictionary<string, Room> Exit { get; set; }
        public Room(string name, string description)
        {
            Name = name;
            Description = description;
            Item = new List<Item>();
            Creature = new List<Creature>();
            Exit = new Dictionary<string, Room>();
        }
        public void AddExit(Room room)
        {
            Exit.Add(room);
        }
        public void GiveTheDescription(string, description)
        {
            Description = description;
        }
        public void MakeNewCreature(Creature creature)
        {
            
        }
        
    }
    {
    }
}
