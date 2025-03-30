using UnityEngine;

#nullable disable
public class PlayShakeCamera : Feedback
{
  [SerializeField]
  private float _amplitude;
  [SerializeField]
  private float _shakeTime;
  [SerializeField]
  private float _frequency;

  public override void CreateFeedback()
  {
    MonoSingleton<CameraManager>.Instance.CameraShake(this._amplitude, this._shakeTime, this._frequency);
  }

  public override void FinishFeedback()
  {
  }
}