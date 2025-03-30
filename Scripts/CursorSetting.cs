using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class CursorSetting : MonoSingleton<CursorSetting>
{
  [Header("CursorImage")]
  [SerializeField]
  private List<Sprite> SetImages = new List<Sprite>();
  [Header("Cursor Physic Setting")]
  [SerializeField]
  private LayerMask _whatIsGround;
  private Dictionary<CursorTypeEnum, Sprite> _cursorImages = new Dictionary<CursorTypeEnum, Sprite>();
  private Transform _cursorTrm;
  private SpriteRenderer _cursorSp;
  private Camera _mainCam;
  private RaycastHit[] _mousePosHits;
  private Ray TestRay;

  public CursorTypeEnum CurrentCursorType { get; private set; } = CursorTypeEnum.Sword;

  public override void Awake()
  {
    base.Awake();
    this._cursorTrm = this.transform.Find("CursorImage");
    this._cursorSp = this._cursorTrm.GetComponent<SpriteRenderer>();
    this._mainCam = Camera.main;
  }

  private void Update()
  {
    if (!(bool) (UnityEngine.Object) this.GetWorldPositionUseRay().collider)
      return;
    Vector3 point = this.GetWorldPositionUseRay().point;
    point.y += 0.3f;
    this._cursorTrm.position = point;
    Vector3 vector3 = MonoSingleton<GameManager>.Instance.PlayerTrm.position - this._cursorTrm.position;
    this._cursorTrm.rotation = Quaternion.Euler(90f, 0.0f, Mathf.Atan2(vector3.y, vector3.x) * 57.29578f);
  }

  private void Start()
  {
    this.CurrentCursorType = CursorTypeEnum.None;
    int index = 0;
    foreach (CursorTypeEnum key in Enum.GetValues(typeof (CursorTypeEnum)))
    {
      this._cursorImages.Add(key, this.SetImages[index]);
      ++index;
    }
  }

  public RaycastHit GetWorldPositionUseRay()
  {
    RaycastHit hitInfo;
    return Physics.Raycast(this._mainCam.ScreenPointToRay(Input.mousePosition), out hitInfo, (float) int.MaxValue, (int) this._whatIsGround) ? hitInfo : new RaycastHit();
  }

  public void ChangeCursor(CursorTypeEnum typeEnum)
  {
    this.CurrentCursorType = typeEnum;
    this._cursorSp.sprite = this._cursorImages[typeEnum];
  }
}
