using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Diagnostics.Contracts;

namespace DungeonExplorer
{
    public class Player : Creature
    {
        public Room CurrentRoom { get; set; }
        public List<Item> Inv { get; set; }
        public Player(string name, int health) : base(name, health)
        {
            Inv = new List<Item>();
        }

        public void PlayerDeath(Monster monster)
        {
            Console.WriteLine($"With one final blow, {monster.Name} has killed {Name}");
            PlayerDies();
        }

        public void PlayerDies()
        {
            string[] deathMessage = new string[]
            {
                @" __     ______  _    _   _      ____   _____ ______ ",
                @" \ \   / / __ \| |  | | | |    / __ \ / ____|  ____|",
                @"  \ \_/ / |  | | |  | | | |   | |  | | (___ | |__   ",
                @"   \   /| |  | | |  | | | |   | |  | |\___ \|  __|  ",
                @"    | | | |__| | |__| | | |___| |__| |____) | |____ ",
                @"    |_|  \____/ \____/  |______\____/|_____/|______|",
            };

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;

            foreach (var line in deathMessage)
            {
                Console.WriteLine(line);
            }

            Console.ResetColor();
            Console.WriteLine("\nYou have lost the game!");
            Console.WriteLine();
            Console.WriteLine($"You finished with a total of {score} points.");
            Console.ReadLine();
            Environment.Exit(0);
        }

        public void MonsterDeath(Monster monster)
        {
            Console.WriteLine($"With one final blow, {Name} has killed {monster.Name}");
            Statistics.KillMonster(monster);
            AddScore(monster.GiveDamage * 10);
            Console.WriteLine($"{Name} earned {monster.GiveDamage * 10} points.");
        }

        public void SetTheCurrentRoom(Room room)
        {
            CurrentRoom = room;
        }
        public void SetTheInv(List<Item> inv)
        {
            Inv = inv;
        }
        public List<Item> ReturnInv()
        {
            return Inv;
        }
        public void GiveWeapon(Weapon weapon)
        {
            Inv.Add(weapon);
        }
        public void GiveMiscellaneous(Miscellaneous miscellaneous)
        {
            Inv.Add(miscellaneous);
        }
        public void GiveFlask(Flask flask)
        {
            Inv.Add(flask);
        }
        public List<Weapon> ReturnWeapon()
        {
            return Inv.OfType<Weapon>()
                      .OrderBy(w => w.GiveDamage)
                      .ToList();
        }
        public List<Miscellaneous> ReturnMiscellaneous()
        {
            return Inv.OfType<Miscellaneous>().ToList();
        }
        public List<Flask> ReturnFlask()
        {
            return Inv.OfType<Flask>()
                      .OrderBy(f => f.HealAmount)
                      .ToList();
        }
        public int UseFlask(Flask flask)
        {
            int healAmount = flask.HealAmount;
            Health += healAmount;
            Inv.Remove(flask);
            return healAmount;
        }
        public void UseMiscellaneous(Miscellaneous miscellaneous)
        {
            Inv.Remove(miscellaneous);
        }

        private Weapon UseBestWeapon() // USES LINQ
        {
            return Inv.OfType<Weapon>()
                            .OrderByDescending(weapon => weapon.GiveDamage)
                            .FirstOrDefault();
        }

        public int score { get; private set; } = 0;

        public void AddScore(int points)
        {
            score += points;
        }



        public void FightCreature(List<Monster> monsters, Player player)
        {
            Random random = new Random();
            Monster monster = monsters[0];
            int phase = 1;
            Console.WriteLine($"Fighting against {monster.Name}");

            while (player.NotDead() && monster.NotDead())
            {
                
                Console.WriteLine("Press enter to enter next phase.");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine($"Phase {phase}");

                

                if (!monster.AttackDelay)
                {
                    Console.WriteLine($"You have been attacked by {monster.Name}. You have recieved {monster.GiveDamage} damage.");
                    player.ReceiveDamage(monster.GiveDamage);

                    if (!player.NotDead())
                    {
                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue");
                        Console.ReadLine();
                        PlayerDeath(monster);
                    }

                    int DamageDealt = player.UseBestWeapon().ReturnGiveDamage();
                    Console.WriteLine($"You have attacked {monster.Name}. You dealt {DamageDealt} damage.");
                    monster.ReceiveDamage(DamageDealt);

                    if (!monster.NotDead())
                    {
                        player.MonsterDeath(monster);
                        player.CurrentRoom.ScrapMonster(monster);
                        Console.WriteLine("Press enter to continue.");
                        Console.ReadLine() ;
                        break;
                    }

                }

                else
                {
                    int DamageDealt = player.UseBestWeapon().ReturnGiveDamage();
                    Console.WriteLine($"You have attacked {monster.Name}. You dealt {DamageDealt} damage.");
                    monster.ReceiveDamage(DamageDealt);
                    if (!monster.NotDead())
                    {
                        player.MonsterDeath(monster);
                        player.CurrentRoom.ScrapMonster(monster);
                        Console.WriteLine("Press enter to continue.");
                        Console.ReadLine();
                        break;
                    }
                    Console.WriteLine($"You have been attacked by {monster.Name}. You have recieved {monster.GiveDamage} damage.");
                    player.ReceiveDamage(monster.GiveDamage);
                    if (!player.NotDead())
                    {
                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue");
                        Console.ReadLine();
                        PlayerDeath(monster);
                    }

                }
                Console.WriteLine($"{player.Name} has {player.ReturnHealth()} Health");
                Console.WriteLine($"{monster.Name} has {monster.ReturnHealth()} Health");
                phase += 1;

            }       
        }
    }
}