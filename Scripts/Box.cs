using UnityEngine;

#nullable disable
public class Box : MonoBehaviour
{
  [SerializeField]
  private GameObject _spawnObj;
  public bool canSpawn;

  public void BreakBox()
  {
    if (this.canSpawn)
      Object.Instantiate<GameObject>(this._spawnObj, this.transform.position, Quaternion.identity);
    Object.Destroy((Object) this.gameObject);
  }
}