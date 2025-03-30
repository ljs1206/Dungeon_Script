using UnityEngine;
using UnityEngine.VFX;

#nullable disable
public class EnemyVFX : AgentVFX
{
  [SerializeField]
  private ParticleSystem[] _bladeParticles;
  [SerializeField]
  private VisualEffect _footStep;
  [SerializeField]
  private ParticleSystem _charingParticles;

  public void PlayBladeVFX(int comboIndex) => this._bladeParticles[comboIndex].Play();

  public void CharingParticlesPlay()
  {
    this._charingParticles.gameObject.SetActive(true);
    this._charingParticles.Play();
  }

  public void StopBladeVFX()
  {
    foreach (ParticleSystem bladeParticle in this._bladeParticles)
      bladeParticle.Stop();
  }

  public void StopCharingEffect()
  {
    this._charingParticles.gameObject.SetActive(false);
    this._charingParticles.Stop();
  }

  public void UpdateFootStep(bool value)
  {
    if (value)
      this._footStep.Play();
    else
      this._footStep.Stop();
  }
}
