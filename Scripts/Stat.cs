using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[Serializable]
public class Stat
{
  [SerializeField]
  private int _baseValue;
  public List<int> modifiers;
  public bool isPercent;

  public int GetValue()
  {
    int baseValue = this._baseValue;
    foreach (int modifier in this.modifiers)
      baseValue += modifier;
    return baseValue;
  }

  public void AddModifier(int value)
  {
    if (value == 0)
      return;
    this.modifiers.Add(value);
  }

  public void RemoveModifier(int value)
  {
    if (value == 0)
      return;
    this.modifiers.Remove(value);
  }

  public void SetDefalutValue(int value) => this._baseValue = value;
}
