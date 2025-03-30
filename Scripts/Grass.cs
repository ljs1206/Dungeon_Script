using UnityEngine;

#nullable disable
public class Grass : MonoBehaviour
{
  private DissovleEffect[] _visual;

  private void Awake() => this._visual = this.GetComponentsInChildren<DissovleEffect>();

  public void StartDissolveCoroutine()
  {
    for (int index = 0; index < this._visual.Length; ++index)
      this.StartCoroutine(this._visual[index].StartDissolveCoroutine());
  }

  private void OnDestroy() => this.transform.parent.GetComponent<PuzzleRoom>()._puzzleEnd = true;
}
