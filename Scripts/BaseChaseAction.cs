using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

#nullable disable
public abstract class BaseChaseAction : Action
{
  [SerializeField]
  protected NavMeshAgent _navAgent;

  protected void SetDestinationAction()
  {
    this._navAgent.SetDestination(MonoSingleton<GameManager>.Instance.PlayerTrm.position);
  }
}