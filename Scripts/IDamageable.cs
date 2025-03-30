using UnityEngine;

#nullable disable
public interface IDamageable
{
  void ApplyDamage(
    int damage,
    Vector3 hitPoint,
    Vector3 normal,
    float knockbackPower,
    Agent dealer,
    DamageType damageType);
}