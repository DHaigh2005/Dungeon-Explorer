using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal interface ICollectable
    {
        void PickUp(Player player);
        void Use(Player player);
    }
}
