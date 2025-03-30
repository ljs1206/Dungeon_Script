using ObjectPooling;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SpawnEnemy : MonoBehaviour
{
  private Transform[] _spawnPoints;
  private bool isCombatStart;
  public List<SpawnPortal> _spawnPortalList = new List<SpawnPortal>();

  private void Awake()
  {
    this._spawnPoints = this.GetComponentsInChildren<Transform>();
    this.isCombatStart = false;
  }

  private void Update()
  {
    if (this.isCombatStart && MonoSingleton<CombatManager>.Instance.CombatEnd)
      this.isCombatStart = false;
    if (!this.isCombatStart || this._spawnPortalList.Count != 0)
      return;
    MonoSingleton<CombatManager>.Instance.CombatEnd = true;
  }

  public void SpawnEvent()
  {
    this._spawnPoints = this.GetComponentsInChildren<Transform>();
    this._spawnPoints = this.GetComponentsInChildren<Transform>();
    for (int index = 1; index < this._spawnPoints.Length; ++index)
    {
      SpawnPortal spawnPortal = MonoSingleton<PoolManager>.Instance.Pop(PoolingType.SpawnPortal) as SpawnPortal;
      this._spawnPortalList.Add(spawnPortal);
      spawnPortal.transform.position = this._spawnPoints[index].position;
      spawnPortal.transform.SetParent(this.transform);
    }
    this.isCombatStart = true;
  }
}
