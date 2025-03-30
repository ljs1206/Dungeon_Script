using System;
using UnityEngine;

#nullable disable
public class PlayerFallState(Player player, PlayerStateMachine stateMachine, string boolName) : 
  PlayerState(player, stateMachine, boolName)
{
  public override void Enter()
  {
    base.Enter();
    this._player.PlayerInput.MovementEvent += new Action<Vector3>(this.HandleMovementEvent);
  }

  public override void Exit()
  {
    this._player.PlayerInput.MovementEvent -= new Action<Vector3>(this.HandleMovementEvent);
    base.Exit();
  }

  private void HandleMovementEvent(Vector3 movement)
  {
    this._player.MovementCompo.SetMovement(Quaternion.Euler(0.0f, -45f, 0.0f) * movement * this._player.moveSpeed * 0.5f);
  }

  public override void UpdateState()
  {
    base.UpdateState();
    if (!this._player.MovementCompo.IsGround)
      return;
    this._stateMachine.ChangeState(PlayerStateEnum.Idle);
  }
}
