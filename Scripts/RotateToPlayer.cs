using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

#nullable disable
public class RotateToPlayer : Action
{
  public override void OnStart()
  {
    Quaternion rotation = this.Owner.transform.rotation;
    this.Owner.transform.rotation = Quaternion.LookRotation((MonoSingleton<GameManager>.Instance.PlayerTrm.position - this.Owner.transform.position) with
    {
      y = 0.0f
    });
  }

  public override TaskStatus OnUpdate() => TaskStatus.Success;
}