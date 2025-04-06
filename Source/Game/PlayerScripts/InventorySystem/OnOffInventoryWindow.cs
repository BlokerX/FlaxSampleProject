using System;
using System.Collections.Generic;
using FlaxEngine;
using Game.Game.PlayerScripts.InventorySystem;

namespace Game;

/// <summary>
/// OnOffInventoryWindow Script.
/// </summary>
public class OnOffInventoryWindow : Script
{
    public KeyboardKeys ToggleKey = KeyboardKeys.I; // Key to toggle the inventory window
    public Actor InventoryWindow; // Reference to the inventory window actor
    public Actor FullScreenUI; // Reference to the full-screen UI actor
    public Actor HUD; // Reference to the HUD actor

    public override void OnStart()
    {
        // Ensure the inventory window is initially inactive
        InventoryWindow.IsActive = false;
        FullScreenUI.IsActive = false;
    }

    public override void OnUpdate()
    {
        if(Input.GetKeyDown(ToggleKey))
        {
            if(!InventoryWindow.IsActive)
            {
                HUD.IsActive = false;

                FullScreenUI.IsActive = true;
                InventoryWindow.IsActive = true;

                Screen.CursorVisible = true;
                Screen.CursorLock = CursorLockMode.None;
            }
            else
            {
                InventoryWindow.IsActive = false;
                FullScreenUI.IsActive = false;

                HUD.IsActive = true;

                Screen.CursorVisible = false;
                Screen.CursorLock = CursorLockMode.Locked;
            }
        }
    }
}
