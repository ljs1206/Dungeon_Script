using DG.Tweening;
using System;
using UnityEngine;

#nullable disable
public class PlayerAnimationTrigger : MonoBehaviour
{
  [SerializeField]
  private Player _player;
  [SerializeField]
  private float _downTime;
  [SerializeField]
  private float _downYpos;
  private Animator _animator;

  private void Awake() => this._animator = this.GetComponent<Animator>();

  private void AnimationEnd()
  {
    this._player.StateMachine.CurrentState.AnimationFinishTrigger();
    this._animator.SetBool("StandUp", false);
  }

  private void PlayVFX() => this._player.PlayBladeVFX();

  private void DamageCast() => this._player.Attack();

  private void SetAnimatorTimeZero() => this._animator.speed = 0.0f;

  private void DownStart() => this.transform.DOLocalMoveY(this._downYpos, 0.3f);

  private void StopAndWait()
  {
    MonoSingleton<AnimationManager>.Instance.StopAndWait(this._animator, this._downTime, new Action(this.SetStandUp));
  }

  public void SetStandUp()
  {
    this.transform.localPosition = Vector3.zero;
    this._animator.SetBool("StandUp", true);
  }

  private void StopImmediately() => this._player.MovementCompo.StopImmediately();
}
