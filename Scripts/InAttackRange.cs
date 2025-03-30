using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

#nullable disable
public class InAttackRange : Conditional
{
  [SerializeField]
  private float _attackRange;
  private Transform _targetTrm;

  public override void OnAwake()
  {
    base.OnAwake();
    this._targetTrm = MonoSingleton<GameManager>.Instance.PlayerTrm;
  }

  public override TaskStatus OnUpdate()
  {
    Vector3 position1 = this._targetTrm.position;
    Vector3 position2 = this.Owner.transform.position;
    position1.y = 0.0f;
    position2.y = 0.0f;
    return (double) Vector3.Distance(this._targetTrm.position, this.Owner.transform.position) <= (double) this._attackRange ? TaskStatus.Success : TaskStatus.Failure;
  }
}