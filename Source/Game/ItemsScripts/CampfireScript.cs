using FlaxEngine;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Game;

/// <summary>
/// CampfireScript Script.
/// </summary>
public class CampfireScript : Script, IHealingItem, IDamagingItem
{
    public Collider DamagingTrigger { get; set; }
    public Collider HealingTrigger { get; set; }
    
    public int HealPower { get; set; } = 1; // per interval
    public float HealingInterval { get; set; } = 0.25f; // in seconds


    public int DamagePower { get; set; } = 1; // per interval
    public float DamageInterval { get; set; } = 0.25f; // in seconds

    public bool IsOn { get; set; } = true;

    private List<ObjectAndTimer<PlayerStats>> _playersInHealingZone = new List<ObjectAndTimer<PlayerStats>>();
    private List<ObjectAndTimer<PlayerStats>> _playersInDangareZone = new List<ObjectAndTimer<PlayerStats>>();

    public override void OnStart()
    {
        if (HealingTrigger is not null)
        {
            HealingTrigger.TriggerEnter += OnHealingTrigger_TriggerEnter;
            HealingTrigger.TriggerExit += OnHealingTrigger_TriggerExit;
        }

        if (DamagingTrigger is not null)
        {
            DamagingTrigger.TriggerEnter += OnDamagingTrigger_TriggerEnter;
            DamagingTrigger.TriggerExit += OnDamagingTrigger_TriggerExit;
        }
    }

    public override void OnFixedUpdate()
    {
        // Damaging
        if (_playersInDangareZone.Count > 0)
        {
            foreach (var player in _playersInDangareZone)
            {
                if (player.Timer >= DamageInterval)
                {
                    GetDamage(player.ObjectValue, DamagePower);
                    player.Timer = 0;
                }
                else player.Timer += Time.DeltaTime;
            }
        }
        // Healing
        else if (_playersInHealingZone.Count > 0)
        {
            foreach (var player in _playersInHealingZone)
            {
                if (player.Timer >= HealingInterval)
                {
                    Heal(player.ObjectValue, HealPower);
                    player.Timer = 0;
                }
                else player.Timer += Time.DeltaTime;
            }
        }
    }

    private void OnHealingTrigger_TriggerEnter(PhysicsColliderActor actor)
    {
        if(actor.GetScript<PlayerStats>() is null)
            return;
        _playersInHealingZone.Add(new ObjectAndTimer<PlayerStats>(actor.GetScript<PlayerStats>()));
    }

    private void OnHealingTrigger_TriggerExit(PhysicsColliderActor actor)
    {
        for (int i = 0; i < _playersInHealingZone.Count; i++)
            if (_playersInHealingZone[i].ObjectValue == actor.GetScript<PlayerStats>())
                _playersInHealingZone.RemoveAt(i);
    }

    private void OnDamagingTrigger_TriggerEnter(PhysicsColliderActor actor)
    {
        if (actor.GetScript<PlayerStats>() is null)
            return;
        _playersInDangareZone.Add(new ObjectAndTimer<PlayerStats>(actor.GetScript<PlayerStats>()));
    }
    
    private void OnDamagingTrigger_TriggerExit(PhysicsColliderActor actor)
    {
        for(int i = 0; i < _playersInDangareZone.Count; i++)
            if (_playersInDangareZone[i].ObjectValue == actor.GetScript<PlayerStats>())
                _playersInDangareZone.RemoveAt(i);
    }

    #region Interfaces

    public void Heal(PlayerStats playerStats, int healthPoints)
    {
        playerStats.Heal(healthPoints);
    }

    public void GetDamage(PlayerStats playerStats, int healthPoints)
    {
        playerStats.TakeDamage(healthPoints);
    }

    #endregion

}
