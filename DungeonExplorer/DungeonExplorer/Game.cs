using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Media;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Net.Http.Headers;

namespace DungeonExplorer
{
    internal class Game
    {
        private List<Room> Room;
        private Player player;

        public Game()
        {
            player = new Player("Name", 100);
            Room = new List<Room>();
            CreateRooms();

        }

        List<string> WeaponPhrase = new List<string>()
        {
            " is found laying in a chest.", "is conveniently placed on a table in the center of the room.", " looks as if it has been here for centuries."
        };


        
        Weapon blacksmithsHammer = new Weapon("Blacksmith's Hammer", "A massive hammer forged in the molten heart of the blacksmiths forge. It shines with power, capable of excessive amounts of damage", 35);
        Weapon battleAxe = new Weapon("Battle Axe", "a heavy axe with a big blade. Great for smashing armour", 18);
        Weapon crossbow = new Weapon("Crossbow", "A ranged weapon, good for piercing monster's flesh.", 22);
        Weapon spear = new Weapon("Spear", "A sharp spear with a long oak shaft.", 18);
        Weapon rock = new Weapon("Rock", "A rough heavy rock. Not the best weapon, but better than using fists.", 5);

        Flask weak = new Flask("Weak Healing Flask", "Regenerates 10 Health", 10);
        Flask medium = new Flask("Medium Healing Flask", "Regenerates 20 Health", 20);
        Flask strong = new Flask("Strong Healing Flask", "Regenerates 30 Health", 30);

        private void SpawnMonsters()
        {
            List<Monster> monsters = new List<Monster>
            {
                new Dragon(),
                new CaveRat(),
                new Spider(),
                new Bear(),
                new Bandit(),
                new Ghost(),
                new GiantBat(),
                new Troll(),
                new Assassin(),
            };

            Random random = new Random();

            int roomCount = Room.Count;

            monsters = monsters.OrderBy(x => random.Next()).ToList();

            for (int i = 0; i < roomCount; i++)
            {
                var monster = monsters[i];
                var room = Room[i];

                room.MakeNewCreature(monster);
            }

        }
        public void MonsterAppear(List<Monster> monsters)
        {
         foreach (var monster in monsters)
            {
                if (monster != null)
                {
                    Console.WriteLine($"{monster.Name} has appeared.");
                }
            }   
        }

        public void WeaponAppear(List<Weapon> weapons)
        {
            Random random = new Random();


            foreach (var weapon in weapons)
            {
                if (weapon != null)
                {
                    string randomPhrase = WeaponPhrase[random.Next(WeaponPhrase.Count)];
                    Console.WriteLine($"A {weapon.Name}{randomPhrase}");
                }
            }
        }

        private void ExitsAppear(Dictionary<string, Room> exits)
        {
            Console.Write("There are doors to the ");

            int exitCount = exits.Count;
            if (exitCount == 0)
            {
                Console.WriteLine("none.");
                return;
            }

            var directions = exits.Keys.ToList();
            if (exitCount == 1)
            {
                Console.WriteLine(directions[0]);
            }
            // Handle the case where there are exactly two exits
            else if (exitCount == 2)
            {
                Console.WriteLine($"{directions[0]} and {directions[1]}");
            }
            // Handle the case where there are more than two exits
            else
            {
                string allExits = string.Join(", ", directions.Take(exitCount - 1));
                Console.WriteLine($"{allExits}, and {directions[exitCount - 1]}");
            }
        }

        public void FlaskAppear(List<Flask> flasks)
        {
            foreach (var flask in flasks)
            {
                if (flask != null)
                {
                    Console.WriteLine($"There is a {flask.Name}");
                }
            }
        }
        private void CreateRooms()
        {
            // Create rooms
            Room forgottenGarden = new Room("Forgotten Garden", "You step into a garden that seems to have been forgotten by time. The air smells of decay and death, yet the vines and ivy are strangely still thriving.");
            Room grandLibrary = new Room("Grand Library", "The shelves are stacked high with ancient scriptures. The scent of paper, and a burning fireplace fill the air.");
            Room ruinedTemple = new Room("Ruined Temple", "The temple stands in ruins, completely covered in moss. What used to be a majestic altar is now just a pile of broken stone.");
            Room undergroundSewer = new Room("Underground Sewer", "You enter the sewer system. The air is heavy with the stench of refuse and stagnant water. The lack of light makes it almost impossible to see where the sewers lead.");
            Room darkHallway = new Room("Dark Hallway", "The hallway stretches out, dimly lit stone walls almost completely covered by moss. The few torches in the room cast long, haunting shadows.");
            Room prisonCells = new Room("Prison Cells", "Iron bars seperate you from the darkness, where faint banging sounds can be heard. The cells are cold and damp with chains hanging on the wall. Some still occupied by the remains of prisoners that have been long forgotten about.");
            Room tortureChamber = new Room("Torture Chamber", "The stone walls are stained thick with blood. Metal contraptions are scattered across the floor. This sends a cold chill down your spine.");
            Room alchemistsLab = new Room("Alchemists Lab", "The scent of strange herbs fill the air. Shelves are lined with glass vials and dusty books filled with forbidden formulas. On a nearby table, multiple healing flasks sit neatly arranged.");
            Room blacksmithsForge = new Room("Blacksmith's Forge", "The heat from an ancient forge makes the room incredibly uncomfortable. A glowing sword rests amongst an anvil. The weapon glistens as you step closer, almost begging to be used in action.");


            // Add rooms to the List
            Room.Add(forgottenGarden);
            Room.Add(grandLibrary);
            Room.Add(ruinedTemple);
            Room.Add(undergroundSewer);
            Room.Add(darkHallway);
            Room.Add(prisonCells);
            Room.Add(tortureChamber);
            Room.Add(alchemistsLab);
            Room.Add(blacksmithsForge);
            // Add exits between rooms
            forgottenGarden.AddRoomExit("North", grandLibrary);
            grandLibrary.AddRoomExit("South", forgottenGarden);

            forgottenGarden.AddRoomExit("East", ruinedTemple);
            ruinedTemple.AddRoomExit("West", forgottenGarden);

            ruinedTemple.AddRoomExit("North", undergroundSewer);
            undergroundSewer.AddRoomExit("South", ruinedTemple);

            undergroundSewer.AddRoomExit("East", darkHallway);
            darkHallway.AddRoomExit("West", undergroundSewer);

            darkHallway.AddRoomExit("North", prisonCells);
            prisonCells.AddRoomExit("South", darkHallway);

            darkHallway.AddRoomExit("East", tortureChamber);
            tortureChamber.AddRoomExit("West", darkHallway);

            prisonCells.AddRoomExit("North", alchemistsLab);
            alchemistsLab.AddRoomExit("South", prisonCells);

            alchemistsLab.AddRoomExit("East", blacksmithsForge);
            blacksmithsForge.AddRoomExit("West", alchemistsLab);

            tortureChamber.AddRoomExit("North", blacksmithsForge);
            blacksmithsForge.AddRoomExit("South", tortureChamber);

            forgottenGarden.GiveItem(new List<Item>
            {
                rock
            });
            grandLibrary.GiveItem(new List<Item>
            {
                spear
            });
            ruinedTemple.GiveItem(new List<Item>
            {
                battleAxe
            });
            undergroundSewer.GiveItem(new List<Item>
            {
                weak
            });
            darkHallway.GiveItem(new List<Item>
            {
                medium
            });
            prisonCells.GiveItem(new List<Item>
            {
                crossbow
            });
            tortureChamber.GiveItem(new List<Item>
            {
                strong
            });
            alchemistsLab.GiveItem(new List<Item>
            {
                strong, strong, strong
            });

            blacksmithsForge.GiveItem(new List<Item>
            {
                blacksmithsHammer, weak
            });
        }

        public void Start()
        {
            Console.WriteLine("Greetings young traveller.");
            Console.WriteLine("If you wish to proceed, please inform me of your name.");
            string playerName = Console.ReadLine();
            player.Name = playerName;
            Console.WriteLine($"Welcome {player.Name}. What awaits you is an abandoned dungeon, that hasnt seen a human soul in years. Who knows what lurks beyond these doors.");
            Console.WriteLine("Press any key when you are ready to enter.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("You open the stiff rotting doors and step in. They slam shut behind you. There is no turning back.");

            ///ADD EVENT LOGIC HERE

            bool GameRunning = true;
            while (GameRunning)
            {

            }
        }

        

    }

}