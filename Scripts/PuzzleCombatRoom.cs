using UnityEngine;
using UnityEngine.Events;

#nullable disable
public class PuzzleCombatRoom : Room
{
  public UnityEvent _combatStartAction;
  public bool _isCombatEnd;
  [SerializeField]
  private CollectOrbPuzzleRoom _COUR;
  [SerializeField]
  private Orb _orbCompo;

  public override void RoomUpdate()
  {
    if (this._isCombatEnd || !MonoSingleton<CombatManager>.Instance.CombatEnd)
      return;
    this.CombatEndFnc();
  }

  public void CombatEndFnc()
  {
    foreach (Gate gate in this._gates)
    {
      this.OutRoom();
      gate.OpenGate();
    }
  }

  public override void EnterRoom(bool isActive, bool redo)
  {
    MonoSingleton<GameManager>.Instance.currentRoom = (Room) this;
    this._isCombatEnd = !redo;
    if (this._isCombatEnd)
      return;
    if (!this._isCombatEnd)
    {
      MonoSingleton<CombatManager>.Instance.CombatEnd = false;
      MonoSingleton<CombatManager>.Instance.isCombat = true;
    }
    if (isActive)
    {
      MonoSingleton<GameManager>.Instance.playerController.enabled = false;
      MonoSingleton<GameManager>.Instance.PlayerTrm.position = this._enterTrm.position;
      MonoSingleton<GameManager>.Instance.playerController.enabled = true;
    }
    for (int index = 0; index < this._gates.Count; ++index)
      this._gates[index].CloseGate();
    this._combatStartAction?.Invoke();
  }

  public override void OutRoom()
  {
    ++this._COUR._currentCount;
    this._orbCompo.ActiveOrb((OrbType) this._COUR._currentCount);
    this._isCombatEnd = true;
  }
}
