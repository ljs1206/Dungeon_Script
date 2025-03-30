using System.Collections.Generic;
using UnityEngine;

#nullable disable
public abstract class Room : MonoBehaviour
{
  public string _roomName;
  public RoomType _roomType;
  public List<Gate> _gates = new List<Gate>();
  [SerializeField]
  protected Transform _enterTrm;
  [SerializeField]
  protected Transform _outTrm;
  [Header("moveRoom")]
  public Room _enterRoom;
  public Room _exitRoom;
  public bool _canOutTrm;

  public virtual void Start()
  {
    foreach (Gate gate in this._gates)
      gate._enterRoom = gate._gateType != GateType.Enter ? this._exitRoom : this._enterRoom;
  }

  public virtual void EnterRoom(bool isActive)
  {
  }

  public virtual void EnterRoom(bool isActive, bool redo)
  {
  }

  public virtual void OutRoom()
  {
  }

  public virtual void RoomUpdate()
  {
  }
}
