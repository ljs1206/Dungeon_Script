using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

#nullable disable
public class FindPlayer : Conditional
{
  [SerializeField]
  private FindType _selFindType;
  [SerializeField]
  private FOV _fov;
  public CircieOverlapSetting _circieOverlapSetting;
  private Collider[] _findObjs;

  public override TaskStatus OnUpdate()
  {
    switch (this._selFindType)
    {
      case FindType.SpawnNow:
        return TaskStatus.Success;
      case FindType.FOV:
        return (bool) (Object) this._fov.CheckFleidView() ? TaskStatus.Success : TaskStatus.Failure;
      case FindType.InCircle:
        Physics.OverlapSphereNonAlloc(this.Owner.transform.position, this._circieOverlapSetting._radius, this._findObjs, (int) this._circieOverlapSetting._whatIsPlayer);
        return (bool) (Object) this._findObjs[0] ? TaskStatus.Success : TaskStatus.Failure;
      default:
        return TaskStatus.Failure;
    }
  }
}