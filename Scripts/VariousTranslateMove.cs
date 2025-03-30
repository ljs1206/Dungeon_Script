using UnityEngine;

#nullable disable
public class VariousTranslateMove : MonoBehaviour
{
  public float m_power;
  public float m_reduceTime;
  public bool m_fowardMove;
  public bool m_rightMove;
  public bool m_upMove;
  public float m_changedFactor;
  private float m_Time;

  private void Start() => this.m_Time = Time.time;

  private void Update()
  {
    this.m_changedFactor = VariousEffectsScene.m_gaph_scenesizefactor;
    if (this.m_fowardMove)
      this.transform.Translate(this.transform.forward * this.m_power * this.m_changedFactor);
    if (this.m_rightMove)
      this.transform.Translate(this.transform.right * this.m_power * this.m_changedFactor);
    if (this.m_upMove)
      this.transform.Translate(this.transform.up * this.m_power * this.m_changedFactor);
    if ((double) this.m_Time + (double) this.m_reduceTime >= (double) Time.time || (double) this.m_reduceTime == 0.0)
      return;
    this.m_power -= Time.deltaTime / 10f;
    this.m_power = Mathf.Clamp01(this.m_power);
  }
}