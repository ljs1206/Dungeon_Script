using UnityEngine;

#nullable disable
public class ObjectMoveDestroy : MonoBehaviour
{
  public GameObject m_gameObjectMain;
  public GameObject m_gameObjectTail;
  private GameObject m_makedObject;
  public Transform m_hitObject;
  public float maxLength;
  public bool isDestroy;
  public float ObjectDestroyTime;
  public float TailDestroyTime;
  public float HitObjectDestroyTime;
  public float maxTime = 1f;
  public float MoveSpeed = 10f;
  public bool isCheckHitTag;
  public string mtag;
  public bool isShieldActive;
  public bool isHitMake = true;
  private float time;
  private bool ishit;
  private float m_scalefactor;

  private void Start()
  {
    this.m_scalefactor = VariousEffectsScene.m_gaph_scenesizefactor;
    this.time = Time.time;
  }

  private void LateUpdate()
  {
    this.transform.Translate(Vector3.forward * Time.deltaTime * this.MoveSpeed * this.m_scalefactor);
    RaycastHit hitInfo;
    if (!this.ishit && Physics.Raycast(this.transform.position, this.transform.forward, out hitInfo, this.maxLength))
      this.HitObj(hitInfo);
    if (!this.isDestroy || (double) Time.time <= (double) this.time + (double) this.ObjectDestroyTime)
      return;
    this.MakeHitObject(this.transform);
    Object.Destroy((Object) this.gameObject);
  }

  private void MakeHitObject(RaycastHit hit)
  {
    if (!this.isHitMake)
      return;
    this.m_makedObject = Object.Instantiate<Transform>(this.m_hitObject, hit.point, Quaternion.LookRotation(hit.normal)).gameObject;
    this.m_makedObject.transform.parent = this.transform.parent;
    this.m_makedObject.transform.localScale = new Vector3(1f, 1f, 1f);
  }

  private void MakeHitObject(Transform point)
  {
    if (!this.isHitMake)
      return;
    this.m_makedObject = Object.Instantiate<Transform>(this.m_hitObject, point.transform.position, point.rotation).gameObject;
    this.m_makedObject.transform.parent = this.transform.parent;
    this.m_makedObject.transform.localScale = new Vector3(1f, 1f, 1f);
  }

  private void HitObj(RaycastHit hit)
  {
    if (this.isCheckHitTag && hit.transform.tag != this.mtag)
      return;
    this.ishit = true;
    if ((bool) (Object) this.m_gameObjectTail)
      this.m_gameObjectTail.transform.parent = (Transform) null;
    this.MakeHitObject(hit);
    if (this.isShieldActive)
    {
      ShieldActivate component = hit.transform.GetComponent<ShieldActivate>();
      if ((bool) (Object) component)
        component.AddHitObject(hit.point);
    }
    Object.Destroy((Object) this.gameObject);
    Object.Destroy((Object) this.m_gameObjectTail, this.TailDestroyTime);
    Object.Destroy((Object) this.m_makedObject, this.HitObjectDestroyTime);
  }
}
