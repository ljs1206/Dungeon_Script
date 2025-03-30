using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#nullable disable
[CreateAssetMenu(menuName = "SO/Stat")]
public class AgentStat : ScriptableObject
{
  public Stat strength;
  public Stat agility;
  public Stat damage;
  public Stat maxHealth;
  public Stat criticalChance;
  public Stat criticalDamage;
  public Stat armor;
  public Stat evasion;
  protected Agent _owner;
  protected Dictionary<StatType, Stat> _statDictionary;

  public virtual void SetOwner(Agent owner) => this._owner = owner;

  public virtual void IncreaseStatFor(int value, float duration, Stat targetStat)
  {
    this._owner.StartCoroutine(this.StatModifyCoroutine(value, duration, targetStat));
  }

  protected IEnumerator StatModifyCoroutine(int value, float duration, Stat targetStat)
  {
    targetStat.AddModifier(value);
    yield return (object) new WaitForSeconds(duration);
    targetStat.RemoveModifier(value);
  }

  protected virtual void OnEnable()
  {
    this._statDictionary = new Dictionary<StatType, Stat>();
    System.Type type = typeof (AgentStat);
    foreach (StatType key in Enum.GetValues(typeof (StatType)))
    {
      try
      {
        string name = this.LowerFirstChar(key.ToString());
        FieldInfo field = type.GetField(name);
        this._statDictionary.Add(key, field.GetValue((object) this) as Stat);
      }
      catch (Exception ex)
      {
        Debug.LogError((object) ("There are no stat - " + key.ToString() + " " + ex.Message));
      }
    }
  }

  private string LowerFirstChar(string input)
  {
    return string.Format("{0}{1}", (object) char.ToLower(input[0]), (object) input.Substring(1));
  }

  public int GetDamage() => this.damage.GetValue() + this.strength.GetValue() * 2;

  public bool CanEvasion()
  {
    return this.IsHitPercent(this.evasion.GetValue() + this.agility.GetValue() * 10);
  }

  public int ArmoredDamage(int incomingDamage)
  {
    return Mathf.Max(1, incomingDamage - Mathf.FloorToInt((float) this.armor.GetValue() * 0.5f));
  }

  public bool IsCritical(ref int incomingDamage)
  {
    if (!this.IsHitPercent(this.criticalChance.GetValue()))
      return false;
    incomingDamage = Mathf.FloorToInt((float) (incomingDamage * this.criticalDamage.GetValue()) * 0.0001f);
    return true;
  }

  protected bool IsHitPercent(int statValue) => UnityEngine.Random.Range(1, 10000) < statValue;

  public void AddModifier(StatType type, int value)
  {
    this._statDictionary[type].AddModifier(value);
  }

  public void RemoveModifier(StatType type, int value)
  {
    this._statDictionary[type].RemoveModifier(value);
  }
}
