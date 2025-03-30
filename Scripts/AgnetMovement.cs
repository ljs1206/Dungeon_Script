using UnityEngine;

#nullable disable
public class AgnetMovement : MonoBehaviour, IMovement
{
  [SerializeField]
  private float _rotateSpeed = 8f;
  private CharacterController _characterController;
  private Agent _agent;
  [SerializeField]
  private float _gravity = -9.8f;
  private Quaternion _targetRotation;
  private Vector3 _velocity;
  private float _verticalVelocity;

  public Vector3 Velocity => this._velocity;

  public bool IsGround => this._characterController.isGrounded;

  public void Initialize(Agent agent)
  {
    this._characterController = this.GetComponent<CharacterController>();
    this._agent = agent;
  }

  private void FixedUpdate()
  {
    this.ApplyRotation();
    this.ApplyGravity();
    this.Move();
  }

  private void ApplyRotation()
  {
    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this._targetRotation, Time.fixedDeltaTime * 8f);
  }

  private void ApplyGravity()
  {
    if (this.IsGround && (double) this._verticalVelocity < 0.0)
      this._verticalVelocity = -0.03f;
    else
      this._verticalVelocity += this._gravity * Time.fixedDeltaTime;
    this._velocity.y = this._verticalVelocity;
  }

  private void Move()
  {
    int num = (int) this._characterController.Move(this._velocity);
  }

  public void SetMovement(Vector3 moveMent, bool isRotate)
  {
    this._velocity = moveMent * Time.fixedDeltaTime;
    if (!(this._velocity != Vector3.zero) || !isRotate)
      return;
    this._targetRotation = Quaternion.LookRotation(this._velocity);
  }

  public void StopImmediately() => this._velocity = Vector3.zero;

  public void SetDestination(Vector3 destination)
  {
  }

  public void GetKnockback(Vector3 force)
  {
  }
}
