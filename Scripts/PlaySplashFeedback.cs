using ObjectPooling;
using UnityEngine;

#nullable disable
public class PlaySplashFeedback : Feedback
{
  [SerializeField]
  private LayerMask _whatIsGround;
  [SerializeField]
  [ColorUsage(true, true)]
  private Color _bloodColor;
  [SerializeField]
  private DamageType _targetType;

  public override void CreateFeedback()
  {
    ActionData actionData = this._owner.HealthCompo.actionData;
    if (actionData.lastDamageType != this._targetType)
      return;
    SplashEffectPlayer splashEffectPlayer = MonoSingleton<PoolManager>.Instance.Pop(PoolingType.VFX_Splash) as SplashEffectPlayer;
    splashEffectPlayer.transform.position = actionData.hitPoint;
    RaycastHit hitInfo;
    if (Physics.Raycast(actionData.hitPoint, Vector3.down, out hitInfo, 10f, (int) this._whatIsGround))
      splashEffectPlayer.SetCustomData(this._bloodColor, -hitInfo.distance);
    splashEffectPlayer.StartPlay(6f);
  }

  public override void FinishFeedback()
  {
  }
}