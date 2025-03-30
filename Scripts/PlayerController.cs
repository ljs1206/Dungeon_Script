using UnityEngine;

#nullable disable
public class PlayerController : MonoBehaviour
{
  [SerializeField]
  private float _moveSpeed;
  private Rigidbody rg;

  private void Awake() => this.rg = this.GetComponent<Rigidbody>();

  private void Update() => this.InputMove();

  private void InputMove()
  {
    this.rg.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")) * this._moveSpeed;
  }
}
