using UnityEngine;

#nullable disable
public class HideObject : MonoBehaviour
{
  [SerializeField]
  private LayerMask _layer;
  private MeshRenderer[] meshs = new MeshRenderer[0];

  private void LateUpdate()
  {
    Vector3 vector3 = MonoSingleton<GameManager>.Instance.PlayerTrm.position - Camera.main.transform.position;
    RaycastHit hitInfo;
    if (Physics.Raycast(Camera.main.transform.position, vector3.normalized, out hitInfo, vector3.magnitude, (int) this._layer))
    {
      foreach (Renderer componentsInChild in hitInfo.collider.GetComponentsInChildren<MeshRenderer>())
        componentsInChild.material.SetFloat("_Alpha", 0.05f);
    }
    else
    {
      if (this.meshs.Length == 0)
        return;
      this.meshs = hitInfo.collider.GetComponentsInChildren<MeshRenderer>();
      for (int index = 0; index < this.meshs.Length; ++index)
        this.meshs[index].material.SetFloat("_Alpha", 0.0f);
    }
  }
}