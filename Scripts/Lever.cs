using UnityEngine;
using UnityEngine.InputSystem;

#nullable disable
public class Lever : MonoBehaviour
{
  [SerializeField]
  private PuzzleCombatRoom _room;
  private bool _inPlayer;

  private void Update()
  {
    if (!this._inPlayer || !MonoSingleton<CombatManager>.Instance.CombatEnd || !Keyboard.current.tKey.wasPressedThisFrame)
      return;
    Debug.Log((object) "입력");
    this._room.EnterRoom(false, true);
  }

  private void OnTriggerEnter(Collider other) => this._inPlayer = true;

  private void OnTriggerExit(Collider other) => this._inPlayer = false;
}