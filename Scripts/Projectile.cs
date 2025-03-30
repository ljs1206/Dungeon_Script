using ObjectPooling;
using UnityEngine;

#nullable disable
public class Projectile : PoolableMono
{
  public override void ResetItem()
  {
  }

  private void OnTriggerEnter(Collider other)
  {
    IDamageable component1;
    if (!other.TryGetComponent<IDamageable>(out component1) || !other.gameObject.CompareTag("Player"))
      return;
    Vector3 vector3 = other.transform.position - this.transform.position;
    RaycastHit hitInfo;
    int num = Physics.Raycast(this.transform.position, vector3.normalized, out hitInfo, vector3.sqrMagnitude, 1 << other.gameObject.layer) ? 1 : 0;
    Vector3 normal = vector3.normalized;
    if (num != 0)
      normal = hitInfo.normal;
    Player component2;
    if (other.TryGetComponent<Player>(out component2) && !component2.CanHit)
      return;
    int damage = component2.Stat.GetDamage();
    float knockbackPower = 3f;
    component1.ApplyDamage(damage, other.transform.position, normal, knockbackPower, (Agent) component2, DamageType.Melee);
    Object.Destroy((Object) this.gameObject);
  }
}