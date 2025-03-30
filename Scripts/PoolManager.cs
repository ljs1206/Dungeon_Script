using ObjectPooling;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class PoolManager : MonoSingleton<PoolManager>
{
  private Dictionary<PoolingType, Pool<PoolableMono>> _pools = new Dictionary<PoolingType, Pool<PoolableMono>>();
  public PoolingTableSO listSO;

  public override void Awake()
  {
    base.Awake();
    foreach (PoolingItemSO data in this.listSO.datas)
      this.CreatePool(data);
  }

  private void CreatePool(PoolingItemSO item)
  {
    Pool<PoolableMono> pool = new Pool<PoolableMono>(item.prefab, item.prefab.type, this.transform, item.poolCount);
    this._pools.Add(item.prefab.type, pool);
  }

  public PoolableMono Pop(PoolingType type)
  {
    if (!this._pools.ContainsKey(type))
    {
      Debug.LogError((object) ("Prefab does not exist on pool : " + type.ToString()));
      return (PoolableMono) null;
    }
    PoolableMono poolableMono = this._pools[type].Pop();
    poolableMono.ResetItem();
    return poolableMono;
  }

  public void Push(PoolableMono obj, bool resetParent = false)
  {
    if (resetParent)
      obj.transform.SetParent(this.transform);
    this._pools[obj.type].Push(obj);
  }
}
