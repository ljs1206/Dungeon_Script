using UnityEngine;

#nullable disable
public class VariousRotateObject : MonoBehaviour
{
  public Vector3 RotateOffset;
  private Vector3 RotateMulti;
  public float m_delay;
  private float m_Time;

  private void Awake() => this.m_Time = Time.time;

  private void Update()
  {
    if ((double) Time.time < (double) this.m_Time + (double) this.m_delay)
      return;
    this.RotateMulti = Vector3.Lerp(this.RotateMulti, this.RotateOffset, Time.deltaTime);
    this.transform.rotation *= Quaternion.Euler(this.RotateMulti);
  }
}