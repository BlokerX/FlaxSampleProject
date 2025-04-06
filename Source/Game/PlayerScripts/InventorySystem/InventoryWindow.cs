using System;
using System.Collections.Generic;
using FlaxEngine;
using FlaxEngine.GUI;
using Game.Game.PlayerScripts.InventorySystem;

namespace Game;

/// <summary>
/// InventoryWindow Script.
/// </summary>
public class InventoryWindow : Script
{
    public Inventory inventory; // Reference to the inventory
    public UIControl SlotsParent;

    public override void OnEnable()
    {
        // Load items from inventory to UI
        LoadItems();
    }

    public override void OnUpdate()
    {
        for (int i = 0; i < inventory.Slots.Count; i++)
        {
            var slot = SlotsParent.GetChild(i) as UIControl;
            var onMouseDown = slot.Control.IsMouseOver;
            if (onMouseDown)
            {
                // show all informations about the item in the slot in debug console
                if (inventory.Slots[i] == null) {
                    Debug.Log("No item in this slot");
                }
                else
                {
                    Debug.Log($"Item Name: {inventory.Slots[i].Item.Name}, " +
                              $"Item Description: {inventory.Slots[i].Item.Description}, " +
                              $"Item Quantity: {inventory.Slots[i].Quantity}, " +
                              $"Item Icon: {inventory.Slots[i].Item.Icon}");
                }
            }
        }
    }

    public void LoadItems()
    {
        // Load items from inventory to UI
        for (int i = 0; i < inventory.Slots.Count; i++)
        {
            var slot = SlotsParent.GetChild(i) as UIControl;
            var item = inventory.Slots[i].Item;
            if (item != null)
            {
                // Load item icon and name
                var icon = (slot.GetChild("Image") as UIControl).Get<Image>();
                icon.Brush = new TextureBrush(item.Icon);
            }
        }
    }

}
