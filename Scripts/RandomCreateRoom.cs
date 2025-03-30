using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class RandomCreateRoom : MonoBehaviour
{
  [SerializeField]
  private Room _startRoom;
  public List<Room> _createRooms = new List<Room>();
  [SerializeField]
  private int _spawnRoomCount;
  private Room _lastSpawnRoom;
  private int _spawnCount;
  private Vector3 _spawnPoint;

  private void Awake() => this.RadomCreate();

  public void RadomCreate()
  {
    this._spawnPoint = new Vector3(100f, 0.0f, 0.0f);
    for (int index1 = 0; index1 < this._spawnRoomCount; ++index1)
    {
      int index2 = Random.Range(0, this._createRooms.Count);
      if ((Object) this._lastSpawnRoom == (Object) null)
      {
        Room room = Object.Instantiate<Room>(this._createRooms[index2], this._spawnPoint, Quaternion.identity);
        this._startRoom._enterRoom = room;
        room._exitRoom = this._startRoom;
        this._lastSpawnRoom = room;
        this._spawnPoint.x += 100f;
      }
      else
      {
        Room room = Object.Instantiate<Room>(this._createRooms[index2], this._spawnPoint, Quaternion.identity);
        this._lastSpawnRoom._enterRoom = room;
        room._exitRoom = this._lastSpawnRoom;
        this._lastSpawnRoom = room;
        this._spawnPoint.x += 100f;
        ++this._spawnCount;
      }
    }
  }
}
