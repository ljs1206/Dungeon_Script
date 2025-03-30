using System.Collections.Generic;
using UnityEngine;

#nullable disable
[CreateAssetMenu(menuName = "SO/Enemy/Table")]
public class EnemyTable : ScriptableObject
{
  public List<EnemySO> enemySOs = new List<EnemySO>();
}