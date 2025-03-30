using System;
using UnityEngine;

#nullable disable
public class PlayerIdleState(Player player, PlayerStateMachine stateMachine, string boolName) : 
  PlayerGroundState(player, stateMachine, boolName)
{
  public override void Enter()
  {
    base.Enter();
    this._player.PlayerInput.MovementEvent += new Action<Vector3>(this.HandleMovementEvent);
    this._player.MovementCompo.StopImmediately();
  }

  public override void Exit()
  {
    this._player.PlayerInput.MovementEvent -= new Action<Vector3>(this.HandleMovementEvent);
    base.Exit();
  }

  private void HandleMovementEvent(Vector3 movement)
  {
    float num = 0.05f;
    if ((double) movement.sqrMagnitude <= (double) num)
      return;
    this._stateMachine.ChangeState(PlayerStateEnum.Run);
  }
}
