using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class SpecialGround : MonoBehaviour
{
  private List<GameObject> _tiles = new List<GameObject>();
  private List<RedTile> _randTileSet = new List<RedTile>();
  [SerializeField]
  private PuzzleRoom _puzzleRoom;

  public RedTile CurrentTile { get; private set; }

  public int Count { get; private set; }

  public void Shuffle(IList<RedTile> list)
  {
    System.Random random = new System.Random();
    int count = list.Count;
    while (count > 1)
    {
      --count;
      int index = random.Next(count + 1);
      RedTile redTile = list[index];
      list[index] = list[count];
      list[count] = redTile;
    }
  }

  private void Awake()
  {
    List<GameObject> list = ((IEnumerable<RedTile>) this.GetComponentsInChildren<RedTile>()).Where<RedTile>((Func<RedTile, bool>) (t => (UnityEngine.Object) t != (UnityEngine.Object) this.transform)).Select<RedTile, GameObject>((Func<RedTile, GameObject>) (t => t.gameObject)).ToList<GameObject>();
    for (int index = 0; index < list.Count; ++index)
      this._randTileSet.Add(list[index].GetComponent<RedTile>());
    this.Shuffle((IList<RedTile>) this._randTileSet);
    this.CurrentTile = this._randTileSet[0];
  }

  public void CheckTile(RedTile tile)
  {
    if ((UnityEngine.Object) this.CurrentTile == (UnityEngine.Object) tile)
    {
      if (this.Count >= this._randTileSet.Count - 1)
      {
        tile.ChangeMat();
        this.PuzzleComplete();
      }
      else
      {
        ++this.Count;
        this.CurrentTile = this._randTileSet[this.Count];
        tile.ChangeMat();
      }
    }
    else
    {
      this.Count = 0;
      this.CurrentTile = this._randTileSet[0];
      this.ResetTile();
    }
  }

  private void PuzzleComplete()
  {
    Debug.Log((object) "완성!");
    this._puzzleRoom._puzzleEnd = true;
    foreach (Component randTile in this._randTileSet)
      randTile.GetComponent<BoxCollider>().enabled = false;
  }

  private void ResetTile()
  {
    foreach (RedTile randTile in this._randTileSet)
      randTile.ChangeOriginMat();
  }
}
