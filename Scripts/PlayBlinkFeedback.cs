using System.Collections;
using UnityEngine;

#nullable disable
public class PlayBlinkFeedback : Feedback
{
  [SerializeField]
  private MeshRenderer[] _targetRenderer;
  [SerializeField]
  private SkinnedMeshRenderer[] _targetSRenderer;
  [SerializeField]
  private float _blinkTime = 0.2f;
  [SerializeField]
  private Material _originMat;
  [SerializeField]
  private Material _blinkMaterial;
  [SerializeField]
  private float _blackRedBlinkTime = 0.1f;
  [Header("Color")]
  [ColorUsage(true, true)]
  public Color _white;
  [ColorUsage(true, true)]
  public Color _red;
  [ColorUsage(true, true)]
  public Color _black;
  [ColorUsage(true, true)]
  public Color _origin;
  private readonly int _blinkValueHash = Shader.PropertyToID("_BlinkValue");
  private Material _targetMaterial;
  private Coroutine _coroutine;

  private void Awake()
  {
  }

  public override void CreateFeedback()
  {
    this._coroutine = this.StartCoroutine(this.BlinkCoroutine());
  }

  private IEnumerator BlinkCoroutine()
  {
    for (int index = 0; index < this._targetRenderer.Length; ++index)
      this._targetRenderer[index].material = this._blinkMaterial;
    for (int index = 0; index < this._targetSRenderer.Length; ++index)
      this._targetSRenderer[index].material = this._blinkMaterial;
    yield return (object) new WaitForSeconds(this._blinkTime);
    for (int index = 0; index < this._targetRenderer.Length; ++index)
    {
      this._targetRenderer[index].material = this._originMat;
      this._targetRenderer[index].material.SetColor("_MainColor", this._black);
    }
    for (int index = 0; index < this._targetSRenderer.Length; ++index)
    {
      this._targetSRenderer[index].material = this._originMat;
      this._targetSRenderer[index].material.SetColor("_MainColor", this._black);
    }
    yield return (object) new WaitForSeconds(this._blackRedBlinkTime);
    for (int index = 0; index < this._targetRenderer.Length; ++index)
      this._targetRenderer[index].material.SetColor("_MainColor", this._red);
    for (int index = 0; index < this._targetSRenderer.Length; ++index)
      this._targetSRenderer[index].material.SetColor("_MainColor", this._red);
    yield return (object) new WaitForSeconds(this._blackRedBlinkTime);
    for (int index = 0; index < this._targetRenderer.Length; ++index)
      this._targetRenderer[index].material.SetColor("_MainColor", this._black);
    for (int index = 0; index < this._targetSRenderer.Length; ++index)
      this._targetSRenderer[index].material.SetColor("_MainColor", this._black);
    yield return (object) new WaitForSeconds(this._blackRedBlinkTime);
    for (int index = 0; index < this._targetRenderer.Length; ++index)
      this._targetRenderer[index].material.SetColor("_MainColor", this._red);
    for (int index = 0; index < this._targetSRenderer.Length; ++index)
      this._targetSRenderer[index].material.SetColor("_MainColor", this._red);
    yield return (object) new WaitForSeconds(this._blackRedBlinkTime);
    for (int index = 0; index < this._targetRenderer.Length; ++index)
      this._targetRenderer[index].material.SetColor("_MainColor", this._origin);
    for (int index = 0; index < this._targetSRenderer.Length; ++index)
      this._targetSRenderer[index].material.SetColor("_MainColor", this._origin);
  }

  public override void FinishFeedback()
  {
    if (this._coroutine != null)
      this.StopCoroutine(this._coroutine);
    for (int index = 0; index < this._targetRenderer.Length; ++index)
    {
      this._targetRenderer[index].material = this._originMat;
      this._targetRenderer[index].material.SetColor("_MainColor", this._origin);
    }
    for (int index = 0; index < this._targetSRenderer.Length; ++index)
    {
      this._targetSRenderer[index].material = this._originMat;
      this._targetSRenderer[index].material.SetColor("_MainColor", this._origin);
    }
  }
}
