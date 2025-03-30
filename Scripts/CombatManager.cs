#nullable disable
public class CombatManager : MonoSingleton<CombatManager>
{
  public bool isCombat { get; set; }

  public bool CombatEnd { get; set; } = true;
}