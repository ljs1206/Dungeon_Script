using System;
using UnityEngine;

#nullable disable
public class PlayerRunState(Player player, PlayerStateMachine stateMachine, string boolName) : 
  PlayerGroundState(player, stateMachine, boolName)
{
  private Vector3 _movementDirection;

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
    float num = 0.05f;
    if ((double) movement.sqrMagnitude < (double) num)
      this._stateMachine.ChangeState(PlayerStateEnum.Idle);
    else
      this._movementDirection = movement.normalized;
  }

  public override void UpdateState()
  {
    base.UpdateState();
    this._player.MovementCompo.SetMovement(this._movementDirection * this._player.moveSpeed);
  }
}
