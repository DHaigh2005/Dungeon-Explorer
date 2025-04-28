using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Statistics
    {
        public static int NumOfMonstersKilled { get; private set; }
        public static List<Monster> MonstersKilledList { get; private set; }
        static Statistics()
        {
            MonstersKilledList = new List<Monster>();
            NumOfMonstersKilled = 0;
        }

        public static void KillMonster(Monster monster)
        {
            NumOfMonstersKilled += 1;
            MonstersKilledList.Add(monster);
        }

        public static void ShowKills()
        {
            Console.WriteLine($"Monsters killed: {NumOfMonstersKilled}");
            Console.WriteLine();
            foreach (var monster in MonstersKilledList)
            {
                Console.WriteLine(monster.Name);
            }

        }
    }
}
