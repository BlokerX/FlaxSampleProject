namespace Game;
/// <summary>
/// IDamagingItem interface.
/// </summary>
public interface IDamagingItem
{
    public void GetDamage(PlayerStats playerStats, int healthPoints);
}
