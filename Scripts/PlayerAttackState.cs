using System;
using UnityEngine;

#nullable disable
public class PlayerAttackState(Player player, PlayerStateMachine stateMachine, string boolName) : 
  PlayerState(player, stateMachine, boolName)
{
  private int _comboCounter;
  private float _lastAttackTime;
  private float _comboWindow = 0.4f;
  private readonly int _comboCounterHash = Animator.StringToHash("ComboCounter");
  private Coroutine _delayCoroutine;

  public override void Enter()
  {
    base.Enter();
    this._player.PlayerInput.DashEvent += new Action(this.HandleDashEvent);
    if (this._comboCounter > 1 | (double) Time.time >= (double) this._lastAttackTime + (double) this._comboWindow)
      this._comboCounter = 0;
    this._player.currentComboCounter = this._comboCounter;
    this._player.AnimatorCompo.speed = this._player.attackSpeed;
    this._player.AnimatorCompo.SetInteger(this._comboCounterHash, this._comboCounter);
    Vector3 towardMouseDirection = MonoSingleton<CameraManager>.Instance.GetTowardMouseDirection(this._player.transform, this._player.PlayerInput.MousePosition);
    this._player.transform.rotation = Quaternion.LookRotation(towardMouseDirection);
    this._player.transform.forward = towardMouseDirection;
    float num = this._player.attackMovement[this._comboCounter];
    this._player.MovementCompo.SetMovement(towardMouseDirection * num);
    this._delayCoroutine = this._player.StartDelayCallback(0.1f, (Action) (() => this._player.MovementCompo.StopImmediately()));
  }

  private void HandleDashEvent()
  {
    if (!this._player.MovementCompo.IsGround)
      return;
    this._player.SwordMoveOrigin();
    this._stateMachine.ChangeState(PlayerStateEnum.Dash);
  }

  public override void Exit()
  {
    ++this._comboCounter;
    this._lastAttackTime = Time.time;
    this._player.AnimatorCompo.speed = 1f;
    if (this._delayCoroutine != null)
      this._player.StopCoroutine(this._delayCoroutine);
    this._player.SwordMoveOrigin();
    this._player.PlayerInput.DashEvent -= new Action(this.HandleDashEvent);
    base.Exit();
  }

  public override void UpdateState()
  {
    base.UpdateState();
    if (!this._endTriggerCalled)
      return;
    this._player.SwordMoveOrigin();
    this._stateMachine.ChangeState(PlayerStateEnum.Idle);
  }

  public override void AnimationFinishTrigger() => this._endTriggerCalled = true;
}
