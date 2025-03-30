using System;
using UnityEngine;

#nullable disable
public class EnemyAnimationEvent : MonoBehaviour
{
  private Health _enemyHealth;
  [SerializeField]
  private Enemy _enemy;
  [SerializeField]
  private float _charingTime;
  private Animator _animator;

  private void Awake()
  {
    this._animator = this.GetComponent<Animator>();
    this._enemyHealth = this.GetComponent<Health>();
  }

  private void HitActionEnd()
  {
    this._animator.SetBool("Hit", false);
    this._enemyHealth.isHit = false;
  }

  private void AttackEnd() => this._enemy.currnetCoolTime = Time.time;

  private void PlayBladeVFX() => this._enemy.PlayBladeVFX();

  private void StopCharingEffect() => this._enemy.StopCharingEffect();

  private void StartCharingEffect() => this._enemy.PlayCharingEffect();

  private void DamageCast() => this._enemy.Attack();

  private void StopAndPlay()
  {
    MonoSingleton<AnimationManager>.Instance.StopAndWait(this._animator, this._charingTime, startAction: new Action(this.StartCharingEffect));
  }

  private void AnimationEnd()
  {
    this._animator.SetBool(this._animator.GetCurrentAnimatorStateInfo(0).ToString(), false);
  }
}
