using System.Collections;
using UnityEngine;

#nullable disable
public class DissovleEffect : MonoBehaviour
{
  private readonly int _dissolveHeightHash = Shader.PropertyToID("_DissolveHeight");
  private MeshRenderer _meshRenderer;

  private void Awake() => this._meshRenderer = this.GetComponent<MeshRenderer>();

  public IEnumerator StartDissolveCoroutine()
  {
    DissovleEffect dissovleEffect = this;
    float currentTime = 0.0f;
    float totalTime = 2f;
    Material mat = dissovleEffect._meshRenderer.material;
    for (; (double) currentTime / (double) totalTime <= 1.0; currentTime += Time.deltaTime)
    {
      float num = Mathf.Lerp(2f, -2f, currentTime / totalTime);
      mat.SetFloat(dissovleEffect._dissolveHeightHash, num);
      yield return (object) null;
    }
    yield return (object) new WaitForSeconds(0.2f);
    Object.Destroy((Object) dissovleEffect.gameObject);
    Object.Destroy((Object) dissovleEffect.transform.parent.gameObject);
  }
}