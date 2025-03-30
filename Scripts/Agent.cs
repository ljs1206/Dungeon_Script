using System;
using System.Collections;
using UnityEngine;

#nullable disable
public abstract class Agent : MonoBehaviour
{
  public bool isDead;
  public bool useBT;

  public Animator AnimatorCompo { get; protected set; }

  public IMovement MovementCompo { get; protected set; }

  public AgentVFX VFXCompo { get; protected set; }

  public DamageCaster DamageCasterCompo { get; protected set; }

  public Health HealthCompo { get; protected set; }

  [field: SerializeField]
  public AgentStat Stat { get; protected set; }

  public bool CanStateChangeable { get; protected set; } = true;

  protected virtual void Awake()
  {
    if (!this.useBT)
    {
      this.MovementCompo = this.GetComponent<IMovement>();
      this.MovementCompo.Initialize(this);
    }
    this.AnimatorCompo = this.transform.Find("Visual").GetComponent<Animator>();
    this.VFXCompo = this.transform.Find("AgentVFX").GetComponent<AgentVFX>();
    Transform transform = this.transform.Find("DamageCaster");
    if ((UnityEngine.Object) transform != (UnityEngine.Object) null)
    {
      this.DamageCasterCompo = transform.GetComponent<DamageCaster>();
      this.DamageCasterCompo.InitCaster(this);
    }
    this.Stat = UnityEngine.Object.Instantiate<AgentStat>(this.Stat);
    this.Stat.SetOwner(this);
    this.HealthCompo = this.GetComponent<Health>();
    this.HealthCompo.Initialize(this);
  }

  public Coroutine StartDelayCallback(float time, Action Callback)
  {
    return this.StartCoroutine(this.DelayCoroutine(time, Callback));
  }

  protected IEnumerator DelayCoroutine(float time, Action Callback)
  {
    yield return (object) new WaitForSeconds(time);
    Action action = Callback;
    if (action != null)
      action();
  }

  public virtual void Attack()
  {
  }

  public abstract void SetDead();
}
