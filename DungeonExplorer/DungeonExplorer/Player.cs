using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

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
            Console.WriteLine("You have lost the game!");
            Console.ReadLine();
            Environment.Exit(0);
        }

        public void MonsterDeath(Monster monster)
        {
            Console.WriteLine($"With one final blow, {Name} has killed {monster.Name}");
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
            return Inv.OfType<Weapon>().ToList();
        }
        public List<Miscellaneous> ReturnMiscellaneous()
        {
            return Inv.OfType<Miscellaneous>().ToList();
        }
        public List<Flask> ReturnFlask()
        {
            return Inv.OfType<Flask>().ToList();
        }
        public void UseFlask(Flask flask)
        {
            Health += flask.HealAmount;
        }
        public void UseMiscellaneous(Miscellaneous miscellaneous)
        {
            Inv.Remove(miscellaneous);
        }

        private Weapon UseBestWeapon() // USES LINQ I THINK
        {
            return Inv.OfType<Weapon>()
                            .OrderByDescending(weapon => weapon.GiveDamage)
                            .FirstOrDefault();
        }

  

        public void FightCreature(List<Monster> monsters, Player player)
        {
            Random random = new Random();
            Monster monster = monsters[0];
            int phase = 1;
            Console.WriteLine($"Fighting against {monster.Name}");

            while (player.NotDead() && monster.NotDead())
            {
                Console.WriteLine();
                Console.WriteLine($"Phase {phase}");

                if (!monster.AttackDelay)
                {
                    Console.WriteLine($"You have been attacked by {monster.Name}. You have recieved {monster.GiveDamage} damage.");
                    player.ReceiveDamage(monster.GiveDamage);

                    if (!player.NotDead())
                    {
                        PlayerDeath(monster);
                    }

                    int DamageDealt = player.UseBestWeapon().ReturnGiveDamage();
                    Console.WriteLine($"You have attacked {monster.Name}. You dealt {DamageDealt} damage.");
                    monster.ReceiveDamage(DamageDealt);

                    if (!monster.NotDead())
                    {
                        player.MonsterDeath(monster);
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
                        break;
                    }
                    Console.WriteLine($"You have been attacked by {monster.Name}. You have recieved {monster.GiveDamage} damage.");
                    player.ReceiveDamage(monster.GiveDamage);
                    if (!player.NotDead())
                    {
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