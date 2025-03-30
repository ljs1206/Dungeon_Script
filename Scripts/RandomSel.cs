using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class RandomSel : MonoBehaviour
{
  public List<Box> boxList = new List<Box>();

  private void Awake()
  {
    int num = Random.Range(0, this.boxList.Count);
    Debug.Log((object) num);
    this.boxList[num].canSpawn = true;
  }
}
