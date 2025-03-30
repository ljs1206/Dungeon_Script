using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class HpBar : MonoBehaviour
{
  private Slider _slider;

  private void Awake() => this._slider = this.GetComponent<Slider>();

  public void Changevalue(float value) => this._slider.DOValue(value, 0.3f);
}