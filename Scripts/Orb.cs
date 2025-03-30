using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class Orb : MonoBehaviour
{
  public List<Material> _activeMat = new List<Material>();
  private Dictionary<OrbType, GameObject> _orbs = new Dictionary<OrbType, GameObject>();

  private void Awake()
  {
    List<GameObject> list = ((IEnumerable<Transform>) this.GetComponentsInChildren<Transform>()).Where<Transform>((Func<Transform, bool>) (t => (UnityEngine.Object) t != (UnityEngine.Object) this.transform)).Select<Transform, GameObject>((Func<Transform, GameObject>) (t => t.gameObject)).ToList<GameObject>();
    this._orbs.Add(OrbType.Red, list[0]);
    this._orbs.Add(OrbType.Green, list[1]);
    this._orbs.Add(OrbType.Blue, list[2]);
  }

  public void ActiveOrb(OrbType orbType)
  {
    switch (orbType)
    {
      case OrbType.Red:
        this._orbs[orbType].GetComponent<MeshRenderer>().material = this._activeMat[0];
        break;
      case OrbType.Green:
        this._orbs[orbType].GetComponent<MeshRenderer>().material = this._activeMat[1];
        break;
      case OrbType.Blue:
        this._orbs[orbType].GetComponent<MeshRenderer>().material = this._activeMat[2];
        break;
    }
  }
}