using System;
using System.Collections.Generic;
using FlaxEngine;
using Game.Game.PlayerScripts.InventorySystem.Items;

namespace Game.Game.PlayerScripts.InventorySystem;

/// <summary>
/// Inventory Script.
/// </summary>
public class Inventory : Script
{
    public List<InventorySlot> Slots = new List<InventorySlot>(new InventorySlot[50]);
    public int MaxSlots = 50;
    public int CurrentSlotIndex = 0;
    public InventorySlot currentSlot => Slots[CurrentSlotIndex];

    public bool TryAddItem(ItemBase item)
    {
        // Current slot
        if (TryAddToExistSlot(item, currentSlot))
            return true;

        // Find slot with same item ID (for each)
        foreach (var slot in Slots)
        {
            if (TryAddToExistSlot(item, slot))
                return true;
        }

        // Find empty slot
        foreach (var slot in Slots)
        {
            if (slot.isEmpty)
            {
                AddItem(item, slot);
                return true;
            }
        }

        // No empty slot found
        Debug.Log("No empty slot found");
        return false;
    }

    private bool TryAddToExistSlot(ItemBase item, InventorySlot slot)
    {
        if (slot.Item.ItemID == item.ItemID)
        {
            if (slot.Quantity < item.MaxStack)
            {
                AddItem(item, slot);
                return true;
            }
        }
        return false;
    }

    public void AddItem(ItemBase item, InventorySlot inventorySlot)
    {
        if (inventorySlot.isEmpty)
        {
            inventorySlot.Item = item;
            inventorySlot.Quantity = 1;
        }
        else
        {
            inventorySlot.Quantity++;
        }
        Debug.Log($"Added {item.Name} to inventory. Quantity: {inventorySlot.Quantity}");
    }

    public void RemoveItem(InventorySlot inventorySlot)
    {
        // Check if the current slot is empty
        if (inventorySlot.isEmpty)
        {
            Debug.Log("No item to remove");
            return;
        }

        // Remove one item from the current slot
        inventorySlot.Quantity--;
        // todo remove item from inventory and throw it on the ground

        // If the quantity is 0, clear the slot
        if (inventorySlot.Quantity <= 0)
        {
            inventorySlot.Item = null;
            inventorySlot.Quantity = 0;
        }
        Debug.Log($"Removed item from inventory. Quantity: {inventorySlot.Quantity}");
    }

    public void RemoveItemFromCurrentSlot() => RemoveItem(currentSlot);

    public void DestroyItem(InventorySlot inventorySlot)
    {
        // Check if the inventory slot is empty
        if (inventorySlot.isEmpty)
        {
            Debug.Log("No item to destroy");
            return;
        }
        // Destroy the item
        inventorySlot.Item = null;
        inventorySlot.Quantity = 0;
        Debug.Log($"Destroyed item from inventory");
    }

    public void DestroyItemFromCurrentSlot() => DestroyItem(currentSlot);

    public void SwapSlots(InventorySlot slot1, InventorySlot slot2)
    {
        // Check if the slots are valid
        if (slot1 == null || slot2 == null)
        {
            Debug.Log("Invalid slot");
            return;
        }

        // Swap the slots
        var temp = slot1;
        slot1 = slot2;
        slot2 = temp;
    }

    public void SwapSlots(int slotIndex1, int slotIndex2) =>
        SwapSlots(Slots[slotIndex1], Slots[slotIndex2]);
}
