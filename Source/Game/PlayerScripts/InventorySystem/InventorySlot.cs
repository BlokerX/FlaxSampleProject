using Game.Game.PlayerScripts.InventorySystem.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Game.PlayerScripts.InventorySystem
{
    public class InventorySlot
    {
        public ItemBase Item;
        public int Quantity;
        public bool isEmpty => Item == null;
    }
}
