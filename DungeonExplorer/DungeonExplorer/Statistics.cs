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

        public static void GameComplete()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Congratulations traveller. You have successfully cleared this dungeon of all evil.");
            Console.WriteLine();
            Console.WriteLine("Press any button to exit.");
            Console.ReadLine();
            Environment.Exit(0);

        }

        public static void ShowKills()
        {
            Console.WriteLine($"Monsters killed: {NumOfMonstersKilled}/10");
            Console.WriteLine();
            foreach (var monster in MonstersKilledList)
            {
                Console.WriteLine(monster.Name);
            }

        }
    }
}
