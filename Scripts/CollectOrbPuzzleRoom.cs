using UnityEngine;

#nullable disable
public class CollectOrbPuzzleRoom : PuzzleRoom
{
  [SerializeField]
  private int _fightCount;
  public int _currentCount;
  public Orb _orb;

  public override void EnterRoom(bool isActive)
  {
    base.EnterRoom(isActive);
    for (int index = 0; index < this._gates.Count; ++index)
      this._gates[index].CloseGate();
    MonoSingleton<GameManager>.Instance.currentRoom = (Room) this;
  }

  public override void RoomUpdate()
  {
    if (this._currentCount < this._fightCount)
      return;
    this._puzzleEnd = true;
    foreach (Gate gate in this._gates)
      gate.OpenGate();
  }
}