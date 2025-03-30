using UnityEngine;

#nullable disable
public class AnimationEvent : MonoBehaviour
{
  [SerializeField]
  private GameObject weapon;
  public AnimationSetting playerHandTo;
  public AnimationSetting playerSpineTo;
  private Animator _animator;

  private void Awake() => this._animator = this.GetComponent<Animator>();

  public void AttackStartEvent()
  {
    this.weapon.transform.SetParent(this.playerHandTo.trm);
    this.weapon.transform.localPosition = this.playerHandTo.pos;
    this.weapon.transform.localRotation = Quaternion.Euler(this.playerHandTo.rot.x, this.playerHandTo.rot.y, this.playerHandTo.rot.z);
  }

  public void AttackEndEvent()
  {
    this._animator.SetBool("Attack", false);
    this.weapon.transform.SetParent(this.playerSpineTo.trm);
    this.weapon.transform.localPosition = this.playerSpineTo.pos;
    this.weapon.transform.localRotation = Quaternion.Euler(this.playerSpineTo.rot.x, this.playerSpineTo.rot.y, this.playerSpineTo.rot.z);
  }
}