using UnityEngine;

#nullable disable
public abstract class PlayerState
{
  protected PlayerStateMachine _stateMachine;
  protected Player _player;
  protected int _animBoolHash;
  protected bool _endTriggerCalled;

  public PlayerState(Player player, PlayerStateMachine stateMachine, string boolName)
  {
    this._player = player;
    this._stateMachine = stateMachine;
    this._animBoolHash = Animator.StringToHash(boolName);
  }

  public virtual void Enter()
  {
    this._player.AnimatorCompo.SetBool(this._animBoolHash, true);
    this._endTriggerCalled = false;
  }

  public virtual void UpdateState()
  {
  }

  public virtual void Exit() => this._player.AnimatorCompo.SetBool(this._animBoolHash, false);

  public virtual void AnimationFinishTrigger() => this._endTriggerCalled = true;
}
