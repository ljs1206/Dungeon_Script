using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

#nullable disable
public class IsHit : Conditional
{
  [SerializeField]
  private Health _health;

  public override TaskStatus OnUpdate()
  {
    return this._health.isHit ? TaskStatus.Success : TaskStatus.Failure;
  }
}