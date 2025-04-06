using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Game.PlayerScripts.InventorySystem.Items
{
    public abstract class ItemBase : Script
    {
        [Serialize] public string ItemID;
        [Serialize] public string Name;
        
        [Serialize] public ItemType Type; // e.g. Weapon, Armor, Consumable, etc.
        [Serialize] public ItemRarity Rarity; // e.g. Common, Uncommon, Rare, etc.

        [Serialize] public string Description;

        [Serialize] public Texture Icon;

        [Serialize] public int MaxStack = 1;
        
        [Serialize] public float Weight = 0.0f;
        [Serialize] public float Value = 0.0f;

        public enum ItemRarity
        {
            Common,
            Uncommon,
            Rare,
            Epic,
            Legendary
        }

        public enum ItemType
        {
            Consumable,
            Tool,
            Weapon,
            Ammo,
            Armor,
            BuildingMaterial,
            Miscellaneous,
            QuestItem
        }
    }
}
