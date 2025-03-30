using System;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
[Serializable]
public struct EnemyInfo
{
  public string name;
  public int power;
  public int speed;
  public int health;
  public Sprite sprite;
  public UnityEvent skill;
}