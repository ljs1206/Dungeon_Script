using UnityEngine;

#nullable disable
public class GameManager : MonoSingleton<GameManager>
{
  public Player playerCompo;
  public Room currentRoom;

  public Transform PlayerTrm { get; set; }

  public CharacterController playerController { get; private set; }

  public override void Awake()
  {
    base.Awake();
    this.currentRoom = (Room) null;
    this.PlayerTrm = GameObject.FindWithTag("Player").transform;
    this.playerCompo = this.PlayerTrm.GetComponent<Player>();
    this.playerController = this.PlayerTrm.GetComponent<CharacterController>();
  }

  public void Update()
  {
    if (!((Object) this.currentRoom != (Object) null))
      return;
    this.currentRoom.RoomUpdate();
  }
}
