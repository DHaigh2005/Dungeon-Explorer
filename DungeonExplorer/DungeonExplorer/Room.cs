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
        public Dictionary<string, Room> RoomExit { get; set; }
        public Room(string name, string description)
        {
            Name = name;
            Description = description;
            Item = new List<Item>();
            Creature = new List<Creature>();
            RoomExit = new Dictionary<string, Room>();
        }

        public void GiveItem(List<Item> item)
        {
            Item = item;
        }
        public List<Item> ReturnItem()
        {
            return Item;
        }
        public void AddRoomExit(string direction, Room room)
        {
            RoomExit[direction] = room;
        }
        public void GiveTheDescription(string description)
        {
            Description = description;
        }

        public Dictionary<string, Room> ReturnRoomExit()
        {
            return RoomExit;
        }

        public string ReturnTheDescription()
        {
            return Description;
        }
        public void MakeNewCreature(Monster monster)
        {
            Creature.Add(monster);
        }

        public void ScrapMonster(Monster monster)
        {
            Creature.Remove(monster);
        }
        
    }
}
