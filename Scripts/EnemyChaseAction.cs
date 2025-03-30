using BehaviorDesigner.Runtime.Tasks;

#nullable disable
public class EnemyChaseAction : BaseChaseAction
{
  public override void OnStart()
  {
    base.OnStart();
    this._navAgent.isStopped = false;
  }

  public override TaskStatus OnUpdate()
  {
    this.SetDestinationAction();
    return TaskStatus.Success;
  }
}
