using UnityEngine;
using UnityEngine.VFX;

#nullable disable
public class PlayerVFX : AgentVFX
{
  [SerializeField]
  private ParticleSystem[] _bladeParticles;
  [SerializeField]
  private ParticleSystem[] _GroundbladeParticles;
  [SerializeField]
  private VisualEffect _footStep;
  [SerializeField]
  private ParticleSystem _collectParticle;

  public void PlayerCillectParticle() => this._collectParticle.Play();

  public void PlayBladeVFX(int comboIndex) => this._bladeParticles[comboIndex].Play();

  public void PlayGroundBladeVFX()
  {
  }

  public void StopBladeVFX()
  {
    foreach (ParticleSystem bladeParticle in this._bladeParticles)
      bladeParticle.Stop();
  }

  public void UpdateFootStep(bool value)
  {
    if (value)
      this._footStep.Play();
    else
      this._footStep.Stop();
  }
}
