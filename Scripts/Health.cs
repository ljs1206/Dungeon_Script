using UnityEngine;
using UnityEngine.Events;

#nullable disable
public class Health : MonoBehaviour, IDamageable
{
  public UnityEvent OnHitEvent;
  public UnityEvent OnDeadEvent;
  public ActionData actionData;
  [HideInInspector]
  public bool isHit;
  [SerializeField]
  private HpBar _hpBar;
  private Agent _owner;
  private int _currentHealth;

  public void Initialize(Agent agent)
  {
    this._owner = agent;
    this.actionData = new ActionData();
    this._currentHealth = this._owner.Stat.maxHealth.GetValue();
    this.isHit = false;
  }

  public void ApplyDamage(
    int damage,
    Vector3 hitPoint,
    Vector3 normal,
    float knockbackPower,
    Agent dealer,
    DamageType damageType)
  {
    if (this._owner.isDead)
      return;
    this.isHit = true;
    Vector3 vector3 = hitPoint + new Vector3(0.0f, 1f, 0.0f);
    if (this._owner.Stat.CanEvasion())
      return;
    this.actionData.hitNormal = normal;
    this.actionData.hitPoint = hitPoint;
    this.actionData.lastDamageType = damageType;
    this.actionData.isCritical = dealer.Stat.IsCritical(ref damage);
    damage = this._owner.Stat.ArmoredDamage(damage);
    this._currentHealth = Mathf.Clamp(this._currentHealth - damage, 0, this._owner.Stat.maxHealth.GetValue());
    this.OnHitEvent?.Invoke();
    Debug.Log((object) this._currentHealth);
    if (this._currentHealth > 0)
      return;
    this.OnDeadEvent?.Invoke();
  }

  private void ApplyKnockback(Vector3 force) => this._owner.MovementCompo.GetKnockback(force);
}
