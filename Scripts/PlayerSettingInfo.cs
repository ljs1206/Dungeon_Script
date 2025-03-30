using UnityEngine;

#nullable disable
public class PlayerSettingInfo : MonoBehaviour
{
  public PlayerType playerType;
  [Header("Warrior Setting")]
  public PlayerSetting warriorSetting;
  [Header("Arher Setting")]
  public PlayerSetting archerSetting;
  [Header("Bard Setting")]
  public PlayerSetting bardSetting;
  [Header("Wizard Setting")]
  public PlayerSetting wizardSetting;
}