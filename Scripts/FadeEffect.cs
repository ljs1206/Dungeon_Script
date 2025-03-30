using System.Collections;
using UnityEngine;

#nullable disable
public class FadeEffect : MonoBehaviour
{
  [Header("Component")]
  [SerializeField]
  private MathTestScript _mathTestScript;
  [Header("FadeEffectSetting")]
  [SerializeField]
  private float _fadeSpeed;
  [SerializeField]
  private Color _originColor = Color.red;
  private Material _targetMat;
  [HideInInspector]
  public bool _coroStart;

  private void Awake() => this._targetMat = this.GetComponent<MeshRenderer>().material;

  public void StartFadeCoro() => this.StartCoroutine(this.SCFadeCoro());

  private IEnumerator SCFadeCoro()
  {
    this._coroStart = true;
    float currentTime = 0.0f;
    while (this._mathTestScript._isFindTarget)
    {
      currentTime += Time.deltaTime * this._fadeSpeed;
      this._targetMat.color = new Color(this._targetMat.color.r, this._targetMat.color.g, this._targetMat.color.b, (float) (((double) Mathf.Sin(currentTime) + 1.0) * 0.5));
      yield return (object) null;
    }
    this._targetMat.color = this._originColor;
    this._coroStart = false;
  }
}
