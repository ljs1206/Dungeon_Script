using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

#nullable disable
public abstract class BaseAttackAction : Action
{
  [SerializeField]
  private Enemy _enemy;

  public override void OnStart()
  {
    base.OnStart();
    this._enemy.currnetCoolTime = Time.time;
  }
}