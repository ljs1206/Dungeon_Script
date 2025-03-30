using UnityEngine;

#nullable disable
public class ScaleFactorApplyToMaterial : MonoBehaviour
{
  private ParticleSystemRenderer ps;
  private float value;
  private float m_scaleFactor;
  private float m_changedFactor;

  private void Awake()
  {
    this.ps = this.GetComponent<ParticleSystemRenderer>();
    this.value = this.ps.material.GetFloat("_NoiseScale");
    this.m_scaleFactor = 1f;
  }

  private void Update()
  {
    this.m_changedFactor = VariousEffectsScene.m_gaph_scenesizefactor;
    if ((double) this.m_scaleFactor == (double) this.m_changedFactor || (double) this.m_changedFactor > 1.0)
      return;
    this.m_scaleFactor = this.m_changedFactor;
    if ((double) this.m_scaleFactor <= 0.5)
      this.ps.material.SetFloat("_NoiseScale", this.value * 0.25f);
    else
      this.ps.material.SetFloat("_NoiseScale", this.value * this.m_scaleFactor);
  }
}