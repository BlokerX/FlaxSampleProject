using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Game.PlayerScripts.InventorySystem.Items
{
    public class Ball : ItemBase
    {
        public Ball()
        {
            ItemID = "ball";
            Name = "Ball";
            Type = ItemType.Miscellaneous;
            Rarity = ItemRarity.Uncommon;
            Description = "A simple ball. Can be used for playing or as a projectile.";
            Icon = FlaxEngine.Content.LoadAsync<Texture>("Textures/ball_icon");
            MaxStack = 10;
            Weight = 0.1f;
            Value = 5.0f;
        }
    }
}
