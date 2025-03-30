using UnityEngine;

#nullable disable
public class PlayerDashState(Player player, PlayerStateMachine stateMachine, string boolName) : 
  PlayerState(player, stateMachine, boolName)
{
  public override void Enter()
  {
    base.Enter();
    this._player.gameObject.layer = 15;
    Vector3 movingDirection = this.GetMovingDirection();
    this._player.transform.forward = movingDirection;
    this._player.MovementCompo.SetMovement(movingDirection * this._player.dashSpeed);
  }

  private Vector3 GetMovingDirection()
  {
    Vector3 zero = Vector3.zero;
    Vector3 keyInput = this._player.PlayerInput.KeyInput;
    return (double) keyInput.magnitude <= 0.0 ? this._player.transform.forward : keyInput.normalized;
  }

  public override void UpdateState()
  {
    base.UpdateState();
    if (!this._endTriggerCalled)
      return;
    this._player.gameObject.layer = 7;
    this._stateMachine.ChangeState(PlayerStateEnum.Idle);
  }

  public override void Exit() => base.Exit();

  public override void AnimationFinishTrigger() => this._endTriggerCalled = true;
}
