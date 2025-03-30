using System;

#nullable disable
public abstract class PlayerGroundState : PlayerState
{
  public PlayerGroundState(Player player, PlayerStateMachine stateMachine, string boolName)
    : base(player, stateMachine, boolName)
  {
  }

  public override void Enter()
  {
    base.Enter();
    this._player.PlayerInput.AttackEvent += new Action(this.HandleAttackEvent);
    this._player.PlayerInput.DashEvent += new Action(this.HandleDashEvent);
  }

  public override void Exit()
  {
    this._player.PlayerInput.AttackEvent -= new Action(this.HandleAttackEvent);
    this._player.PlayerInput.DashEvent -= new Action(this.HandleDashEvent);
    base.Exit();
  }

  private void HandleDashEvent()
  {
    if (!this._player.MovementCompo.IsGround)
      return;
    this._stateMachine.ChangeState(PlayerStateEnum.Dash);
  }

  private void HandleAttackEvent()
  {
    if (!this._player.MovementCompo.IsGround)
      return;
    this._stateMachine.ChangeState(PlayerStateEnum.Attack);
  }

  public override void UpdateState()
  {
    base.UpdateState();
    if (this._player.MovementCompo.IsGround)
      return;
    this._stateMachine.ChangeState(PlayerStateEnum.Fall);
  }
}
