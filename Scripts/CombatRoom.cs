using UnityEngine.Events;

#nullable disable
public class CombatRoom : Room
{
  public UnityEvent _combatStartAction;
  private bool _isCombatEnd;

  public override void RoomUpdate()
  {
    if (!MonoSingleton<CombatManager>.Instance.CombatEnd)
      return;
    for (int index = 0; index < this._gates.Count; ++index)
      this._gates[index].OpenGate();
    this._isCombatEnd = true;
  }

  public override void EnterRoom(bool isActive)
  {
    MonoSingleton<GameManager>.Instance.currentRoom = (Room) this;
    this._isCombatEnd = false;
    if (!this._isCombatEnd)
    {
      MonoSingleton<CombatManager>.Instance.CombatEnd = false;
      MonoSingleton<CombatManager>.Instance.isCombat = true;
    }
    MonoSingleton<GameManager>.Instance.playerController.enabled = false;
    MonoSingleton<GameManager>.Instance.PlayerTrm.position = this._enterTrm.position;
    MonoSingleton<GameManager>.Instance.playerController.enabled = true;
    if (this._isCombatEnd)
      return;
    for (int index = 0; index < this._gates.Count; ++index)
      this._gates[index].CloseGate();
    this._combatStartAction?.Invoke();
  }

  public override void OutRoom()
  {
  }
}
