#nullable disable
public class PlayerDeadState(Player player, PlayerStateMachine stateMachine, string boolName) : 
  PlayerState(player, stateMachine, boolName)
{
  public override void Enter()
  {
    base.Enter();
    this._player.MovementCompo.StopImmediately();
    this._player.PlayerInput.SetPlayerInput(false);
    this._player.gameObject.layer = 10;
  }

  public override void UpdateState()
  {
    base.UpdateState();
    if (!this._endTriggerCalled)
      return;
    this._player.OnGameOverEvent?.Invoke();
  }

  public override void AnimationFinishTrigger() => this._endTriggerCalled = true;
}
