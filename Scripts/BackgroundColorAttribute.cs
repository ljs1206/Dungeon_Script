using UnityEngine;

#nullable disable
public class BackgroundColorAttribute : PropertyAttribute
{
  public float r;
  public float g;
  public float b;
  public float a;

  public BackgroundColorAttribute() => this.r = this.g = this.b = this.a = 1f;

  public BackgroundColorAttribute(float aR, float aG, float aB, float aA)
  {
    this.r = aR;
    this.g = aG;
    this.b = aB;
    this.a = aA;
  }

  public Color Color => new Color(this.r, this.g, this.b, this.a);
}
