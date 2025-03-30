using ObjectPooling;
using UnityEngine;

#nullable disable
public class Enemy : Agent
{
  [HideInInspector]
  public float currnetCoolTime;
  [Header("ProjectTile Setting")]
  [SerializeField]
  private float _projectileSpeed;
  [SerializeField]
  private Transform _spawnTrm;
  [SerializeField]
  private bool _rangeAttack;

  [field: SerializeField]
  public float CoolTime { get; private set; } = 2f;

  public EnemyVFX EnemyVFXCompo => this.VFXCompo as EnemyVFX;

  protected override void Awake() => base.Awake();

  public override void SetDead()
  {
    this.isDead = true;
    this.transform.parent.GetComponent<SpawnPortal>()._currentEnemy.Remove(this);
    this.gameObject.layer = 10;
    MonoSingleton<AnimationManager>.Instance.ChangeAnimationBool(this.AnimatorCompo, "Dead");
  }

  public void PlayBladeVFX() => this.EnemyVFXCompo.PlayBladeVFX(0);

  public void PlayCharingEffect() => this.EnemyVFXCompo.CharingParticlesPlay();

  public void StopCharingEffect() => this.EnemyVFXCompo.StopCharingEffect();

  public override void Attack()
  {
    this.DamageCasterCompo.CastDamage();
    if (!this._rangeAttack)
      return;
    PoolableMono poolableMono = MonoSingleton<PoolManager>.Instance.Pop(PoolingType.Projectile_MagicCircle);
    poolableMono.transform.position = this._spawnTrm.position;
    poolableMono.GetComponent<Rigidbody>().AddForce(this.transform.forward * this._projectileSpeed, ForceMode.Impulse);
  }
}
