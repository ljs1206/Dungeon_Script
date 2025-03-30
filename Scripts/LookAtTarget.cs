using UnityEngine;

#nullable disable
public class LookAtTarget : MonoBehaviour
{
  public Transform Target;

  private void Update() => this.transform.LookAt(this.Target);
}