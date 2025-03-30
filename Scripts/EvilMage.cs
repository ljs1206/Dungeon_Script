using UnityEngine;

#nullable disable
public class EvilMage : Agent
{
  [HideInInspector]
  public float currnetCoolTime;

  [field: SerializeField]
  public float CoolTime { get; private set; } = 2f;

  public EnemyVFX EnemyVFXCompo => this.VFXCompo as EnemyVFX;

  protected override void Awake() => base.Awake();

  public override void SetDead()
  {
    this.isDead = true;
    this.gameObject.layer = 10;
    MonoSingleton<AnimationManager>.Instance.ChangeAnimationBool(this.AnimatorCompo, "Dead");
  }

  public void PlayBladeVFX() => this.EnemyVFXCompo.PlayBladeVFX(0);

  public void PlayCharingEffect() => this.EnemyVFXCompo.CharingParticlesPlay();

  public void StopCharingEffect() => this.EnemyVFXCompo.StopCharingEffect();

  public override void Attack() => this.DamageCasterCompo.CastDamage();
}
