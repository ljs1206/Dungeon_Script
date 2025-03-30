#nullable disable
public class TilePuzzleMap : PuzzleRoom
{
  public override void EnterRoom(bool isActive)
  {
    base.EnterRoom(isActive);
    for (int index = 0; index < this._gates.Count; ++index)
      this._gates[index].CloseGate();
    MonoSingleton<GameManager>.Instance.currentRoom = (Room) this;
  }

  public override void OutRoom()
  {
  }

  public override void RoomUpdate()
  {
    if (!this._puzzleEnd)
      return;
    for (int index = 0; index < this._gates.Count; ++index)
      this._gates[index].OpenGate();
  }
}