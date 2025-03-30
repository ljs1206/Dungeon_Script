using System.Collections;
using UnityEngine;

#nullable disable
public class PlayerBlinkMatFeedback : Feedback
{
  [SerializeField]
  private Material _blinkMat;
  [SerializeField]
  private Material _originMat;
  [SerializeField]
  private SkinnedMeshRenderer _meshRenderer;
  [SerializeField]
  private float _blinkTime;
  private Coroutine _coroutine;

  public override void CreateFeedback()
  {
    this._originMat = this._meshRenderer.material;
    this._coroutine = this.StartCoroutine(this.BlinkCoro());
  }

  private IEnumerator BlinkCoro()
  {
    this._meshRenderer.material = this._blinkMat;
    yield return (object) new WaitForSeconds(this._blinkTime);
    Debug.Log((object) this._originMat);
    this._meshRenderer.material = this._originMat;
  }

  public override void FinishFeedback()
  {
    if (this._coroutine != null)
      this.StopCoroutine(this._coroutine);
    this._meshRenderer.material = this._originMat;
  }
}
