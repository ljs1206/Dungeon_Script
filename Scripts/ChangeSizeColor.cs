using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class ChangeSizeColor : MonoBehaviour
{
  public Gradient color;
  public Color m_changeColor;
  public GameObject m_obj;
  private Renderer[] m_rnds;
  private float color_Value;
  private bool isChangeColor;
  public Image m_ColorHandler;
  public Text m_intensityfactor;
  private float intensity = 2f;

  private void Update()
  {
    this.m_changeColor = this.color.Evaluate(this.color_Value);
    this.m_ColorHandler.color = this.m_changeColor;
    if (!this.isChangeColor || !((Object) this.m_obj != (Object) null))
      return;
    this.m_rnds = this.m_obj.GetComponentsInChildren<Renderer>(true);
    foreach (Renderer rnd in this.m_rnds)
    {
      for (int index = 0; index < rnd.materials.Length; ++index)
      {
        rnd.materials[index].SetColor("_TintColor", this.m_changeColor * this.intensity);
        rnd.materials[index].SetColor("_Color", this.m_changeColor * this.intensity);
        rnd.materials[index].SetColor("_RimColor", this.m_changeColor * this.intensity);
      }
    }
  }

  public void ChangeEffectColor(float value) => this.color_Value = value;

  public void CheckIsColorChange(bool value) => this.isChangeColor = value;

  public void CheckColorState()
  {
    if (this.isChangeColor)
      this.isChangeColor = false;
    else
      this.isChangeColor = true;
  }

  public void GetIntensityFactor()
  {
    float num = float.Parse(this.m_intensityfactor.text.ToString());
    if ((double) num > 0.0)
      this.intensity = num;
    else
      this.intensity = 0.0f;
  }
}
