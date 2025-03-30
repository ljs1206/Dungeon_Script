using UnityEngine;

#nullable disable
public class RedTile : MonoBehaviour
{
  private SpecialGround _specialGround;
  private MeshRenderer _meshRenderer;
  private Material _originMat;
  [SerializeField]
  private Material _greenMat;

  private void Awake()
  {
    this._specialGround = this.transform.parent.GetComponent<SpecialGround>();
    this._meshRenderer = this.GetComponent<MeshRenderer>();
    this._originMat = this._meshRenderer.material;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (!other.gameObject.CompareTag("Player"))
      return;
    this._specialGround.CheckTile(this);
  }

  public void ChangeMat() => this._meshRenderer.material = this._greenMat;

  public void ChangeOriginMat() => this._meshRenderer.material = this._originMat;
}