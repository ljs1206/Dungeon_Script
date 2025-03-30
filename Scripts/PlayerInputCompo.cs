using System;
using UnityEngine;
using UnityEngine.InputSystem;

#nullable disable
public class PlayerInputCompo : MonoBehaviour
{
  private PlayerInput _playerInput;
  private InputAction _inputMoveAction;
  private bool _playerInputEnabled = true;
  [SerializeField]
  private Player _player;

  public event Action<Vector3> MovementEvent;

  public event Action AttackEvent;

  public event Action HoldAttackEvent;

  public event Action DashEvent;

  public Vector3 MousePosition { get; private set; }

  public Vector3 KeyInput { get; private set; }

  public float MouseInputTime { get; private set; }

  private void Awake()
  {
    this._playerInput = this.GetComponent<PlayerInput>();
    this._inputMoveAction = this._playerInput.actions.FindAction("Move", false);
  }

  public void SetPlayerInput(bool enabled) => this._playerInputEnabled = enabled;

  private void Update()
  {
    if (!this._playerInputEnabled)
      return;
    this.CheckMoveInput();
    this.CheckMouseInput();
    this.CheckDashInput();
  }

  private void CheckDashInput()
  {
    if (!Keyboard.current.spaceKey.wasPressedThisFrame)
      return;
    Action dashEvent = this.DashEvent;
    if (dashEvent == null)
      return;
    dashEvent();
  }

  private void CheckMouseInput()
  {
    this.MousePosition = (Vector3) Mouse.current.position.ReadValue();
    if (Mouse.current.leftButton.isPressed)
    {
      Action attackEvent = this.AttackEvent;
      if (attackEvent != null)
        attackEvent();
    }
    if (!Mouse.current.rightButton.wasPressedThisFrame)
      return;
    Action holdAttackEvent = this.HoldAttackEvent;
    if (holdAttackEvent != null)
      holdAttackEvent();
    this.MouseInputTime = Time.time;
  }

  private void CheckMoveInput()
  {
    Vector3 vector3 = (Vector3) this._inputMoveAction.ReadValue<Vector2>();
    this.KeyInput = new Vector3(vector3.x, 0.0f, vector3.y);
    Action<Vector3> movementEvent = this.MovementEvent;
    if (movementEvent == null)
      return;
    movementEvent(this.KeyInput.normalized);
  }
}
