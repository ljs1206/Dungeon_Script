using UnityEngine;

#nullable disable
public class DestroyObject : MonoBehaviour
{
  public float m_DestroytTime;
  private float m_Time;

  private void Start() => this.m_Time = Time.time;

  private void Update()
  {
    if ((double) Time.time <= (double) this.m_Time + (double) this.m_DestroytTime)
      return;
    Object.Destroy((Object) this.gameObject);
  }
}