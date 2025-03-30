#nullable disable
public class PlayerHitState(Player player, PlayerStateMachine stateMachine, string boolName) : 
  PlayerState(player, stateMachine, boolName)
{
  public override void Enter()
  {
    base.Enter();
    this._player.CanHit = false;
    this._player.MovementCompo.StopImmediately();
    this._player.PlayerInput.SetPlayerInput(false);
  }

  public override void UpdateState()
  {
    base.UpdateState();
    if (!this._endTriggerCalled)
      return;
    this._stateMachine.ChangeState(PlayerStateEnum.Idle);
  }

  public override void Exit()
  {
    base.Exit();
    this._player.CanHitChange();
    this._player.PlayerInput.SetPlayerInput(true);
  }

  public override void AnimationFinishTrigger() => this._endTriggerCalled = true;
}
