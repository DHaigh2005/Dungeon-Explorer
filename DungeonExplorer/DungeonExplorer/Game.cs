using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Media;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DungeonExplorer
{
    internal class Game
    {
        private List<Room> Room;
        private Player player;

        public Game()
        {
            player = new Player("Name", 100);
            Rooms = new List<Room>();
            CreateRooms();

        }

        List<string> WeaponPhrase = new List<string>()
        {
            " is found laying in a chest.", "is conveniently placed on a table in the center of the room.", " looks as if it has been here for centuries."
        };

        private void CreateRooms()
        {
            Room ForgottenGarden = new Room("Forgotten Garden", "You step into a garden that seems to have been forgotten by time. The air smells of decay and death, yet the vines and ivy are strangely still thriving.");
            Room GrandLibrary = new Room("Grand Library", "The shelves are stacked high with ancient scriptures. The scent of paper, and a burning fireplace fill the air.");
            Room = new Room("", "");
            Room = new Room("", "");
            Room = new Room("", "");
            Room = new Room("", "");
            Room = new Room("", "");
            Room = new Room("", "");

        }
    }
}
