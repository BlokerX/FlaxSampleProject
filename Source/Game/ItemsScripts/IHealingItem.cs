namespace Game;
/// <summary>
/// IHealingItem interface.
/// </summary>
public interface IHealingItem
{
    public void Heal(PlayerStats playerStats, int healthPoints);
}
