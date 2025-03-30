using UnityEngine;
using UnityEngine.InputSystem;

#nullable disable
public class ChangeLayer : MonoBehaviour
{
  [SerializeField]
  private LayerMask _changeLayer;
  [SerializeField]
  private LayerMask _originLayer;

  private void Update()
  {
    if (!Keyboard.current.eKey.wasPressedThisFrame)
      return;
    if (this.gameObject.layer == (int) this._changeLayer)
      this.gameObject.layer = 7;
    else
      this.gameObject.layer = 8;
  }
}