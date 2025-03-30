sing ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SpawnPortal : PoolableMono
{
  [Header("SpawnSetting")]
  [SerializeField]
  private float _spawnCount;
  [SerializeField]
  private float _delayTime;
  [SerializeField]
  private float _particleBrustTime;
  public List<Enemy> _spawnEnemys = new List<Enemy>();
  private ParticleSystem _ps;
  private bool _start;
  public List<Enemy> _currentEnemy;

  private IEnumerator SpawnEnemy()
  {
    SpawnPortal spawnPortal = this;
    yield return (object) null;
    for (int i = 0; (double) i < (double) spawnPortal._spawnCount; ++i)
    {
      int index = Random.Range(0, spawnPortal._spawnEnemys.Count);
      Enemy enemy = Object.Instantiate<Enemy>(spawnPortal._spawnEnemys[index], spawnPortal.transform);
      enemy.transform.SetParent(spawnPortal.transform);
      enemy.transform.localPosition = Vector3.zero;
      spawnPortal._currentEnemy.Add(enemy);
      if ((double) (i + 1) == (double) spawnPortal._spawnCount)
        yield return (object) new WaitForSeconds(spawnPortal._delayTime);
    }
    spawnPortal._start = true;
    yield return (object) new WaitForSeconds(spawnPortal._particleBrustTime);
    spawnPortal._ps.Stop();
  }

  private void Update()
  {
    if (this._currentEnemy.Count != 0 || !this._start)
      return;
    this._start = false;
    this.transform.parent.GetComponent<global::SpawnEnemy>()._spawnPortalList.Remove(this);
  }

  public override void ResetItem()
  {
    this.StartCoroutine(this.SpawnEnemy());
    this._ps = this.transform.GetChild(0).GetComponent<ParticleSystem>();
  }
}
