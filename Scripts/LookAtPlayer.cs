using UnityEngine;

#nullable disable
public class LookAtPlayer : MonoBehaviour
{
  private RectTransform rectTransform;

  private void Awake() => this.rectTransform = this.GetComponent<RectTransform>();

  private void LateUpdate()
  {
    this.rectTransform.rotation = Quaternion.LookRotation((Camera.main.transform.position - this.rectTransform.position).normalized);
  }
}