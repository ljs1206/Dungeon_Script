using UnityEngine;
using UnityEngine.InputSystem;

#nullable disable
public class PlayerHoldAttackState(Player player, PlayerStateMachine stateMachine, string boolName) : 
  PlayerState(player, stateMachine, boolName)
{
  private float currentTime;
  private Vector3 playerDirection;

  public override void Enter()
  {
    base.Enter();
    this._player.MovementCompo.StopImmediately();
    this.playerDirection = MonoSingleton<CameraManager>.Instance.GetTowardMouseDirection(this._player.transform, this._player.PlayerInput.MousePosition);
    this._player.transform.rotation = Quaternion.LookRotation(this.playerDirection);
    this._player.transform.forward = this.playerDirection;
  }

  public override void UpdateState()
  {
    base.UpdateState();
    if (!Mouse.current.rightButton.isPressed)
    {
      this._player.AnimatorCompo.speed = 1f;
      if ((double) this.currentTime - (double) this._player.PlayerInput.MouseInputTime > 4.0)
        this._player._isFullCharing = true;
    }
    else
      this.currentTime = Time.time;
    if (!this._endTriggerCalled)
      return;
    this._stateMachine.ChangeState(PlayerStateEnum.Idle);
  }

  public override void Exit() => base.Exit();

  public override void AnimationFinishTrigger() => this._endTriggerCalled = true;
}
