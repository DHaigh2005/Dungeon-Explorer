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
using System.Diagnostics.Eventing.Reader;

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
            player.CurrentRoom = Room[0];
        }

        List<string> WeaponPhrase = new List<string>()
        {
            " is found laying in a chest.", " is conveniently placed on a table in the center of the room.", " looks as if it has been here for centuries."
        };


        
        Weapon blacksmithsHammer = new Weapon("Blacksmith's Hammer", "A massive hammer forged in the molten heart of the blacksmiths forge. It shines with power, capable of excessive amounts of damage", 35);
        Weapon battleAxe = new Weapon("Battle Axe", "a heavy axe with a big blade. Great for smashing armour", 18);
        Weapon crossbow = new Weapon("Crossbow", "A ranged weapon, good for piercing monster's flesh.", 22);
        Weapon spear = new Weapon("Spear", "A sharp spear with a long oak shaft.", 18);
        Weapon rock = new Weapon("Rock", "A rough heavy rock. Not the best weapon, but better than using fists.", 5);
        Weapon cursedSword = new Weapon("Cursed Sword", "A dark blade covered in mysterious ancient text.", 0);

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
                new Dwarf(),
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
        public void MonsterAppear(List<Creature> monsters)
        {
         foreach (var creature in monsters)
            {
                if (creature is Monster monster)
                {
                    Console.WriteLine($"{monster.Name} has appeared.");
                }
                else
                {
                    Console.WriteLine("No monsters are present.");
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
            Room forgottenGarden = new Room("Forgotten Garden", "You are in a garden that seems to have been forgotten by time. The air smells of decay and death, yet the vines and ivy are strangely still thriving.");
            Room grandLibrary = new Room("Grand Library", "The shelves are stacked high with ancient scriptures. The scent of paper, and a burning fireplace fill the air.");
            Room ruinedTemple = new Room("Ruined Temple", "The temple stands in ruins, completely covered in moss. What used to be a majestic altar is now just a pile of broken stone.");
            Room undergroundSewer = new Room("Underground Sewer", "You enter the sewer system. The air is heavy with the stench of refuse and stagnant water. The lack of light makes it almost impossible to see where the sewers lead.");
            Room darkHallway = new Room("Dark Hallway", "The hallway stretches out, dimly lit stone walls almost completely covered by moss. The few torches in the room cast long, haunting shadows.");
            Room prisonCells = new Room("Prison Cells", "Iron bars seperate you from the darkness, where faint banging sounds can be heard. The cells are cold and damp with chains hanging on the wall. Some still occupied by the remains of prisoners that have been long forgotten about.");
            Room tortureChamber = new Room("Torture Chamber", "The stone walls are stained thick with blood. Metal contraptions are scattered across the floor. This sends a cold chill down your spine.");
            Room alchemistsLab = new Room("Alchemists Lab", "The scent of strange herbs fill the air. Shelves are lined with glass vials and dusty books filled with forbidden formulas. On a nearby table, multiple healing flasks sit neatly arranged.");
            Room blacksmithsForge = new Room("Blacksmith's Forge", "The heat from an ancient forge makes the room incredibly uncomfortable. A glowing sword rests amongst an anvil. The weapon glistens as you step closer, almost begging to be used in action.");
            Room trapRoom = new Room("Obsidian Hall", "The room is eerily silent.The black stone walls glisten and reflect in random directions. You feel a draught of wind, but can not tell where from.");

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
            Room.Add(trapRoom);
            // Add exits between rooms
            forgottenGarden.AddRoomExit("North", grandLibrary);
            forgottenGarden.AddRoomExit("East", ruinedTemple);
            forgottenGarden.AddRoomExit("South", trapRoom);

            trapRoom.AddRoomExit("North", forgottenGarden);

            grandLibrary.AddRoomExit("South", forgottenGarden);
            grandLibrary.AddRoomExit("East", undergroundSewer);

            ruinedTemple.AddRoomExit("West", forgottenGarden);
            ruinedTemple.AddRoomExit("North", undergroundSewer);

            undergroundSewer.AddRoomExit("South", ruinedTemple);
            undergroundSewer.AddRoomExit("West", grandLibrary);
            undergroundSewer.AddRoomExit("East", darkHallway);
            
            darkHallway.AddRoomExit("West", undergroundSewer);
            darkHallway.AddRoomExit("North", prisonCells);
            
            prisonCells.AddRoomExit("South", darkHallway);
            prisonCells.AddRoomExit("North", alchemistsLab);
            prisonCells.AddRoomExit("East", tortureChamber);


            tortureChamber.AddRoomExit("West", darkHallway);
            tortureChamber.AddRoomExit("North", blacksmithsForge);

            
            alchemistsLab.AddRoomExit("South", prisonCells);
            alchemistsLab.AddRoomExit("East", blacksmithsForge);
            
            blacksmithsForge.AddRoomExit("West", alchemistsLab);
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
            trapRoom.GiveItem(new List<Item>
            {
                cursedSword
            });
        }

        private void DisplayGameInfo()
        {
            //give game info
            Console.WriteLine($"Room: {player.CurrentRoom.Name}.");
            Console.WriteLine();
            player.DisplayHealth();
            Console.WriteLine();
            Console.WriteLine(player.CurrentRoom.Description);
            Console.WriteLine();
            ExitsAppear(player.CurrentRoom.RoomExit);
            Console.WriteLine();
            var monsters = player.CurrentRoom.Creature;
            var roomWeapons = player.CurrentRoom.ReturnItem().OfType<Weapon>().ToList();
            var roomFlasks = player.CurrentRoom.ReturnItem().OfType<Flask>().ToList();
            MonsterAppear(monsters);
            Console.WriteLine();
            WeaponAppear(roomWeapons);
            Console.WriteLine();
            FlaskAppear(roomFlasks);
            Console.WriteLine("-----------------------------------");
        }

        public void Start()
        {
            Console.WriteLine("Greetings young traveller.");
            Console.WriteLine("If you wish to proceed, please inform me of your name.\n");
            string playerName = Console.ReadLine();
            player.Name = playerName;
            Console.WriteLine();
            Console.WriteLine($"Welcome {player.Name}. What awaits you is an abandoned dungeon, that hasnt seen a human soul in years. Who knows what lurks beyond these doors.");
            Console.WriteLine("Press any key when you are ready to enter.");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("You open the stiff rotting doors and step in. They slam shut behind you. There is no turning back.");

            SpawnMonsters();
            DisplayGameInfo();
            ///ADD EVENT LOGIC HERE

            bool GameRunning = true;
            while (GameRunning)
            {
                var roomWeapons = player.CurrentRoom.ReturnItem().OfType<Weapon>().ToList();
                var roomFlasks = player.CurrentRoom.ReturnItem().OfType<Flask>().ToList();
                string choice = PlayerChoice();

                // Player chooses a direction
                if (choice == "north")
                {
                    var exits = player.CurrentRoom.ReturnRoomExit();
                    if (exits.ContainsKey("North"))
                    {
                        player.CurrentRoom = exits["North"];
                        Console.Clear();
                        Console.WriteLine("You venture through the northern exit.");
                        DisplayGameInfo();
                    }
                    else
                    {
                        Console.WriteLine("You cannot go North.");
                    }
                }
                else if (choice == "south")
                {
                    var exits = player.CurrentRoom.ReturnRoomExit();
                    if (exits.ContainsKey("South"))
                    {
                        player.CurrentRoom = exits["South"];
                        Console.Clear();
                        Console.WriteLine("You venture through the southern exit");
                        DisplayGameInfo();
                    }
                    else
                    {
                        Console.WriteLine("You cannot go South.");
                    }
                }
                else if (choice == "east")
                {
                    var exits = player.CurrentRoom.ReturnRoomExit();
                    if (exits.ContainsKey("East"))
                    {
                        player.CurrentRoom = exits["East"];
                        Console.Clear();
                        Console.WriteLine("You venture through the eastern exit");
                        DisplayGameInfo();
                    }
                    else
                    {
                        Console.WriteLine("You cannot go East.");
                    }
                }
                else if (choice == "west")
                {
                    var exits = player.CurrentRoom.ReturnRoomExit();
                    if (exits.ContainsKey("West"))
                    {
                        player.CurrentRoom = exits["West"];
                        Console.Clear();
                        Console.WriteLine("You venture through the western exit");
                        DisplayGameInfo();
                    }
                    else
                    {
                        Console.WriteLine("You cannot go West.");
                    }
                }
                else if (choice == "pick up")
                {
                    Console.Clear();
                    var currentItems = player.CurrentRoom.ReturnItem();
                    if (currentItems.Count == 0)
                    {
                        DisplayGameInfo();
                        Console.WriteLine("There are no items in this room.");
                    }
                    else
                    {
                        foreach (var flask in roomFlasks)
                        {
                            player.GiveFlask(flask);
                            player.CurrentRoom.ScrapItem(flask);
                        }
                        foreach(var weapon in roomWeapons)
                        {
                            if (weapon.Name == "Cursed Sword")
                            {
                                DisplayGameInfo();
                                Console.WriteLine("As your hands grip the cursed sword, the ground beneath you shakes. The entire room crumbles.");
                                Console.WriteLine();
                                player.PlayerDies();
                            }
                            player.GiveWeapon(weapon);
                            player.CurrentRoom.ScrapItem(weapon);
                        }
                        DisplayGameInfo();

                    }
                    
                }
                else if (choice == "inventory")
                {
                    var playerWeapons = player.ReturnWeapon();
                    var playerFlasks = player.ReturnFlask();
                    Console.Clear();
                    DisplayGameInfo();
                    Console.WriteLine($"{player.Name}'s inventory:");
                    Console.WriteLine();
                    Console.WriteLine("Weapons:");
                    Console.WriteLine();
                    if (playerWeapons.Count > 0)
                    {
                        foreach (var weapon in playerWeapons)
                        {
                            Console.WriteLine($"{weapon.Name} - {weapon.GiveDamage}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You have no weapons.");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Flasks:");
                    Console.WriteLine();
                    if (playerFlasks.Count > 0)
                    {
                        foreach(var flask in playerFlasks)
                        {
                            Console.WriteLine($"{flask.Name} - {flask.Description}");
                        }
                    }
                    
                }
                else if (choice == "attack")
                {
                    var monster = player.CurrentRoom.Creature.FirstOrDefault(c => c is Monster) as Monster;
                    
                    if(monster == null)
                    {
                        Console.WriteLine("There are no monsters to attack.");
                    }
                    else
                    {
                        Console.Clear();
                        DisplayGameInfo();
                        var monstersList = new List<Monster> { monster };

                        player.FightCreature(monstersList, player);
                        Console.Clear();
                        DisplayGameInfo();
                    }
                }

                else if (choice == "heal")
                {
                    var playerFlasks = player.ReturnFlask();
                    Console.Clear();
                    DisplayGameInfo();
                    Console.WriteLine("Which flask would you like to use?");
                    Console.WriteLine();
                    int count = 1;
                    foreach (var flask in playerFlasks)
                    {
                        Console.WriteLine($"{count}: {flask.Name} - {flask.Description}");
                        count += 1;
                    }
                    Console.WriteLine("\nPlease enter a number:");
                    string input = Console.ReadLine()?.Trim();
                    if (int.TryParse(input, out int inputNumber))
                    {
                        int flaskChoice = inputNumber - 1;
                        
                        if (flaskChoice >= 0 && flaskChoice < playerFlasks.Count)
                        {
                            Flask chosenFlask = playerFlasks[flaskChoice];
                            Console.Clear();
                            DisplayGameInfo();
                            int healedAmount = player.UseFlask(chosenFlask);
                            Console.WriteLine($"You take a drink from the {chosenFlask.Name}. You health went up by {healedAmount}");
                            Console.WriteLine($"You now have {player.ReturnHealth()} Health.");
                        }
                        
                        else
                        {
                            Console.WriteLine("Invalid flask number.");
                            Console.WriteLine();
                            Console.WriteLine("Please press enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                            DisplayGameInfo();
                        }
                    }

                }
                else if (choice == "statistics")
                {
                    Console.Clear();
                    DisplayGameInfo();
                    Statistics.ShowKills();
                }

          
            }
        }

        private string PlayerChoice()
        {

            List<string> possibleChoices = new List<string> {};
            var currentItems = player.CurrentRoom.ReturnItem();
            var exits = player.CurrentRoom.ReturnRoomExit();
            var playerItems = player.ReturnInv();
            var playerFlasks = player.ReturnFlask();
            var monster = player.CurrentRoom.Creature.FirstOrDefault(Creature => Creature is Monster);
            var playerWeapons = player.ReturnWeapon();
            if (monster != null)
            if (exits.ContainsKey("North"))
            {
                possibleChoices.Add("north");
            }
            if (exits.ContainsKey("East"))
            {
                possibleChoices.Add("east");
            }
            if (exits.ContainsKey("South"))
            {
                possibleChoices.Add("south");
            }
            if (exits.ContainsKey("West"))
            {
                possibleChoices.Add("west");
            }
            if (monster != null && playerWeapons.Count > 0)
            {
                possibleChoices.Add("attack");
            }
            if (playerItems.Count > 0)
            {
                possibleChoices.Add("inventory");
            }
            if (currentItems.Count > 0)
            {
                possibleChoices.Add("pick up");
            }
            if (playerFlasks.Count > 0)
            {
                possibleChoices.Add("heal");
            }
            if(Statistics.MonstersKilledList.Count > 0)
            {
                possibleChoices.Add("statistics");
            }
            Console.WriteLine();
            Console.WriteLine($"Choices:");
            foreach (var choice in possibleChoices)
            {
                Console.WriteLine(choice);
            }
            Console.WriteLine();
            string input = Console.ReadLine()?.ToLower().Trim();
            while (!possibleChoices.Contains(input))
            {
                Console.WriteLine("Invalid input. Please try again.");
                Console.WriteLine();
                input = Console.ReadLine()?.ToLower().Trim();
            }
            return input;

        }
        

    }

}