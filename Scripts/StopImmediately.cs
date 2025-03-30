using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

#nullable disable
public class StopImmediately : Action
{
  [SerializeField]
  private NavMeshAgent _navAgent;

  public override void OnStart()
  {
    this._navAgent.isStopped = true;
    this._navAgent.velocity = Vector3.zero;
  }

  public override TaskStatus OnUpdate() => TaskStatus.Success;
}
