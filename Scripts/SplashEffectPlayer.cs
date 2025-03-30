using UnityEngine;
using UnityEngine.VFX;

#nullable disable
public class SplashEffectPlayer : EffectPlayer
{
  private readonly int _deltaYName = Shader.PropertyToID("YDelta");
  private readonly int _colorName = Shader.PropertyToID("Color");

  public void SetCustomData(Color color, float yDelta)
  {
    foreach (VisualEffect effect in this._effects)
    {
      effect.SetFloat(this._deltaYName, yDelta);
      effect.SetVector4(this._colorName, (Vector4) color);
    }
  }
}