using FlaxEngine;
using FlaxEngine.GUI;
using Game.Game.PlayerScripts.InventorySystem;
using System.Collections.Generic;

namespace Game;

/// <summary>
/// InventoryHotbarManager Script.
/// </summary>
public class InventoryHotbarManager : Script
{
    public Inventory inventory;
    public int HotbarSolts = 10;

    public UIControl SlotsParent;

    public List<UIControl> Slots = new List<UIControl>(new UIControl[10]);
    public UIControl CurrentSlot => Slots[inventory.CurrentSlotIndex];
    public Border CurrentSlotBorder => CurrentSlot.Get<Border>();
    public UIControl GetSlot(int index) => Slots[index];
    public Border GetSlotBorder(int index) => GetSlot(index).Get<Border>();

    public override void OnEnable()
    {
        // Load items from inventory to UI
        LoadItems();
    }

    public override void OnStart()
    {
        SelectSlot(inventory.CurrentSlotIndex);
        Debug.Log($"Current Slot: {CurrentSlot.Name}");
    }

    public override void OnUpdate()
    {
        // Check if the player is pressing the hotbar keys (1-0)
        for(int i = 0; i < HotbarSolts; i++)
        {
            if (Input.GetKeyDown((KeyboardKeys)(KeyboardKeys.Alpha0 + i)))
            {
                SelectSlot(i);
            }
        }
    }

    // Select the item in the hotbar
    public void SelectSlot(int slotIndex)
    {
        var slot = GetSlot(slotIndex);
        var border = slot.Get<Border>();

        foreach (var s in Slots)
        {
            if (s != slot)
            {
                var b = s.Get<Border>();
                b.BorderColor = new Color(0.25f, 0.25f, 0.25f, 0.25f);
                b.BackgroundColor = Color.Transparent;
            }
        }

        border.BorderColor = Color.DimGray;
        border.BackgroundColor = new Color(0.25f, 0.25f, 0.25f, 0.5f);

    }

    // Scroll up to select the next item in the hotbar
    public void ScrollUp()
    {
        inventory.CurrentSlotIndex++;
        if (inventory.CurrentSlotIndex >= HotbarSolts)
        {
            inventory.CurrentSlotIndex = 0;
        }
    }

    // Scroll down to select the previous item in the hotbar
    public void ScrollDown()
    {
        inventory.CurrentSlotIndex--;
        if (inventory.CurrentSlotIndex < 0)
        {
            inventory.CurrentSlotIndex = HotbarSolts - 1;
        }
    }

    public void LoadItems()
    {
        // Load items from inventory to UI
        for (int i = 0; i < HotbarSolts; i++)
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
