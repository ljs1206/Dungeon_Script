using ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

#nullable disable
public class EffectPlayer : PoolableMono
{
  [SerializeField]
  protected List<ParticleSystem> _particles;
  [SerializeField]
  protected List<VisualEffect> _effects;

  public void StartPlay(float time)
  {
    if (this._particles != null)
      this._particles.ForEach((Action<ParticleSystem>) (p => p.Play()));
    if (this._effects != null)
      this._effects.ForEach((Action<VisualEffect>) (e => e.Play()));
    this.StartCoroutine(this.TimerCoroutine(time));
  }

  private IEnumerator TimerCoroutine(float time)
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    EffectPlayer effectPlayer = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      MonoSingleton<PoolManager>.Instance.Push((PoolableMono) effectPlayer);
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) new WaitForSeconds(time);
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;
  }

  public override void ResetItem()
  {
    if (this._particles != null)
      this._particles.ForEach((Action<ParticleSystem>) (p => p.Simulate(0.0f)));
    if (this._effects == null)
      return;
    this._effects.ForEach((Action<VisualEffect>) (e => e.Stop()));
  }
}
