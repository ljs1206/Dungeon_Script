using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

#nullable disable
public class Gate : MonoBehaviour
{
  [SerializeField]
  private Transform _moveStartPos;
  [SerializeField]
  private Transform _moveEndPos;
  [SerializeField]
  private Material _changeMat;
  [SerializeField]
  private Material _originMat;
  [HideInInspector]
  public Room _enterRoom;
  public GateType _gateType;
  public bool _activeGate;
  private GameObject _blockingWall;

  private void Start()
  {
    this._blockingWall = this.transform.Find("BlockWall")?.gameObject;
    this._blockingWall.transform.position = this._moveStartPos.position;
    this._blockingWall.SetActive(false);
  }

  public void CloseGate()
  {
    this._blockingWall = this.transform.Find("BlockWall")?.gameObject;
    this._blockingWall.SetActive(true);
    this._blockingWall.transform.DOLocalMoveY(this._moveEndPos.position.y, 0.3f).SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(Ease.OutExpo);
  }

  public void OpenGate()
  {
    this._blockingWall.transform.DOLocalMoveY(this._moveStartPos.position.y, 1f).SetEase<TweenerCore<Vector3, Vector3, VectorOptions>>(Ease.OutExpo).OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>((TweenCallback) (() => this._blockingWall.SetActive(false)));
  }

  private void OnTriggerEnter(Collider other)
  {
    if (!other.gameObject.CompareTag("Player"))
      return;
    this._enterRoom.EnterRoom(this._activeGate);
  }
}
