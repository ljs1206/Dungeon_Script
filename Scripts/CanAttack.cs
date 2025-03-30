using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

#nullable disable
public class CanAttack : Conditional
{
  [SerializeField]
  private Enemy enemy;

  public override TaskStatus OnUpdate()
  {
    return (double) Time.time - (double) this.enemy.currnetCoolTime >= (double) this.enemy.CoolTime || (double) this.enemy.currnetCoolTime == 0.0 ? TaskStatus.Success : TaskStatus.Failure;
  }
}