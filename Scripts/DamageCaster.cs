using UnityEngine;

#nullable disable
public class DamageCaster : MonoBehaviour
{
  [Header("OverlapSetting")]
  [SerializeField]
  [Range(0.5f, 3f)]
  private float _casterRadius = 1f;
  [SerializeField]
  [Range(0.0f, 1f)]
  private float _casterInterpolation = 0.5f;
  [SerializeField]
  [Range(0.0f, 3f)]
  private float _castingRange = 1f;
  public LayerMask targetLayer;
  private RaycastHit[] _hitInfo;
  private Agent _owner;

  public void InitCaster(Agent agent) => this._owner = agent;

  public bool CastDamage()
  {
    Vector3 startPos = this.GetStartPos();
    bool flag = Physics.SphereCast(startPos, this._casterRadius, this.transform.forward, out RaycastHit _, this._castingRange, (int) this.targetLayer);
    this._hitInfo = Physics.SphereCastAll(startPos, this._casterRadius, this.transform.forward, this._castingRange, (int) this.targetLayer);
    if (flag)
    {
      for (int index = 0; index < this._hitInfo.Length; ++index)
      {
        Box component1;
        if (this._hitInfo[index].collider.TryGetComponent<Box>(out component1))
        {
          component1.BreakBox();
          return true;
        }
        Grass component2;
        if (this._hitInfo[index].collider.TryGetComponent<Grass>(out component2) && MonoSingleton<GameManager>.Instance.playerCompo._attackType == AttackType.Fire)
        {
          component2.StartDissolveCoroutine();
          return true;
        }
        IDamageable component3;
        if (this._hitInfo[index].collider.TryGetComponent<IDamageable>(out component3))
        {
          int damage = this._owner.Stat.GetDamage();
          float knockbackPower = 3f;
          Player component4;
          if (this._hitInfo[index].collider.TryGetComponent<Player>(out component4))
          {
            if (!component4.CanHit)
              return false;
            component3.ApplyDamage(damage, this._hitInfo[index].point, this._hitInfo[index].normal, knockbackPower, this._owner, DamageType.Melee);
          }
          else
            component3.ApplyDamage(damage, this._hitInfo[index].point, this._hitInfo[index].normal, knockbackPower, this._owner, DamageType.Melee);
        }
      }
    }
    return flag;
  }

  private Vector3 GetStartPos()
  {
    return this.transform.position + this.transform.forward * -this._casterInterpolation * 2f;
  }
}
