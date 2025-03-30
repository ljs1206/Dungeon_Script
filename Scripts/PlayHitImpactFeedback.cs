using ObjectPooling;
using UnityEngine;

#nullable disable
public class PlayHitImpactFeedback : Feedback
{
  [SerializeField]
  private float _playTime = 1.5f;

  public override void CreateFeedback()
  {
    EffectPlayer effectPlayer = MonoSingleton<PoolManager>.Instance.Pop(PoolingType.VFX_Hit) as EffectPlayer;
    ActionData actionData = this._owner.HealthCompo.actionData;
    Quaternion rotation = Quaternion.LookRotation(actionData.hitNormal * -1f);
    effectPlayer.transform.SetPositionAndRotation(actionData.hitPoint, rotation);
    effectPlayer.StartPlay(this._playTime);
  }

  public override void FinishFeedback()
  {
  }
}
