using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

#nullable disable
public class IsDead : Conditional
{
  [SerializeField]
  private Enemy _enemy;

  public override TaskStatus OnUpdate()
  {
    return this._enemy.isDead ? TaskStatus.Success : TaskStatus.Failure;
  }
}