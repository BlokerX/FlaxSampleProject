using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// Inventory Script.
/// </summary>
public class Inventory : Script
{
    // todo nie działa : do zrobienia
    public Actor ItemStorage { get; set; }
    public Actor MainHand { get; set; }

    public List<Actor> items = new List<Actor>();
    public int maxItems = 10;
    public int currentItems = 0;
    public int currentItemIndex = 0;

    // funkcja dodająca przedmiot do ekwipunku
    public void AddItem(Actor item)
    {
        if (currentItems < maxItems)
        {
            items.Add(item);
            currentItems++;
            item.Parent = ItemStorage;
            item.Position = Vector3.Zero;
            item.Rotation = Quaternion.Identity;
        }
        else
        {
            Debug.Log("Inventory is full");
        }
    }

    // funkcja usuwająca przedmiot z ekwipunku
    public void RemoveItem(Actor item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            currentItems--;
            item.Parent = null;
        }
        else
        {
            Debug.Log("Item not found in inventory");
        }
    }

    // funkcja zmieniająca aktualny przedmiot
    public void ChangeCurrentItem(int index)
    {
        if (index >= 0 && index < currentItems)
        {
            if (MainHand != null)
            {
                MainHand.Parent = null;
            }
            MainHand = items[index];
            MainHand.Parent = ItemStorage;
            MainHand.Position = Vector3.Zero;
            MainHand.Rotation = Matrix.Default;
        }
        else
        {
            Debug.Log("Invalid item index");
        }
    }

    // funkcja zmieniająca aktualny przedmiot na następny
    public void NextItem()
    {
        currentItemIndex++;
        if (currentItemIndex >= currentItems)
        {
            currentItemIndex = 0;
        }
        ChangeCurrentItem(currentItemIndex);
    }

    // funkcja zmieniająca aktualny przedmiot na poprzedni
    public void PreviousItem()
    {
        currentItemIndex--;
        if (currentItemIndex < 0)
        {
            currentItemIndex = currentItems - 1;
        }
        ChangeCurrentItem(currentItemIndex);
    }

    // funkcja sprawdzająca czy ekwipunek jest pełny
    public bool IsFull()
    {
        return currentItems >= maxItems;
    }

    // funkcja sprawdzająca czy ekwipunek jest pusty
    public bool IsEmpty()
    {
        return currentItems == 0;
    }

    // funkcja zwracająca aktualny przedmiot
    public Actor GetCurrentItem()
    {
        if (currentItems > 0)
        {
            return items[currentItemIndex];
        }
        else
        {
            return null;
        }
    }

}
