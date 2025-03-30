using ObjectPooling;
using UnityEngine;

#nullable disable
public class PlaySlashEffectFeedback : Feedback
{
  [SerializeField]
  private float _playTime = 0.8f;
  [SerializeField]
  private DamageType _targetType;

  public override void CreateFeedback()
  {
    ActionData actionData = this._owner.HealthCompo.actionData;
    if (actionData.lastDamageType != this._targetType)
      return;
    EffectPlayer effectPlayer = MonoSingleton<PoolManager>.Instance.Pop(PoolingType.VFX_Slash) as EffectPlayer;
    effectPlayer.transform.position = actionData.hitPoint;
    effectPlayer.StartPlay(this._playTime);
  }

  public override void FinishFeedback()
  {
  }
}