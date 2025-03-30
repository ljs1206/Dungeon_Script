using UnityEngine;

#nullable disable
public class ObjectMove : MonoBehaviour
{
  public float time;
  private float m_time;
  private float m_time2;
  public float MoveSpeed = 10f;
  public bool AbleHit;
  public float HitDelay;
  public GameObject m_hitObject;
  private GameObject m_makedObject;
  public float MaxLength;
  public float DestroyTime2;
  private float m_scalefactor;

  private void Start()
  {
    this.m_scalefactor = VariousEffectsScene.m_gaph_scenesizefactor;
    this.m_time = Time.time;
    this.m_time2 = Time.time;
  }

  private void LateUpdate()
  {
    if ((double) Time.time > (double) this.m_time + (double) this.time)
      Object.Destroy((Object) this.gameObject);
    this.transform.Translate(Vector3.forward * Time.deltaTime * this.MoveSpeed * this.m_scalefactor);
    RaycastHit hitInfo;
    if (!this.AbleHit || !Physics.Raycast(this.transform.position, this.transform.forward, out hitInfo, this.MaxLength) || (double) Time.time <= (double) this.m_time2 + (double) this.HitDelay)
      return;
    this.m_time2 = Time.time;
    this.HitObj(hitInfo);
  }

  private void HitObj(RaycastHit hit)
  {
    this.m_makedObject = Object.Instantiate<GameObject>(this.m_hitObject, hit.point, Quaternion.LookRotation(hit.normal)).gameObject;
    Object.Destroy((Object) this.m_makedObject, this.DestroyTime2);
  }
}
