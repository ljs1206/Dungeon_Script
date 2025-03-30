using UnityEngine;

#nullable disable
public class DelayActive : MonoBehaviour
{
  public GameObject[] m_activeObj;
  public float m_delayTime;
  private float m_time;

  private void Start() => this.m_time = Time.time;

  private void Update()
  {
    if ((double) Time.time <= (double) this.m_time + (double) this.m_delayTime)
      return;
    for (int index = 0; index < this.m_activeObj.Length; ++index)
    {
      if ((Object) this.m_activeObj[index] != (Object) null)
        this.m_activeObj[index].SetActive(true);
    }
  }
}