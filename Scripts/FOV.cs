using System;
using UnityEngine;

#nullable disable
public class FOV : MonoBehaviour
{
  [SerializeField]
  public LayerMask _whatIsplayer;
  [SerializeField]
  public float _viewAngle;
  [SerializeField]
  public float _viewRadius;
  [SerializeField]
  public bool _showGizoms;
  [SerializeField]
  public LayerMask _whatIsObstacle;
  [HideInInspector]
  public Collider _visibleTarget;
  [HideInInspector]
  public Collider _obstacleTarget;
  [HideInInspector]
  public Collider[] _visibleTargets;
  [HideInInspector]
  public bool _isFindTarget;

  public Collider CheckFleidView()
  {
    this._visibleTargets = Physics.OverlapSphere(this.transform.position, this._viewRadius, (int) this._whatIsplayer);
    if (this._visibleTargets.Length < 1)
      return (Collider) null;
    Transform transform = this._visibleTargets[0].transform;
    Vector3 normalized = (transform.position - this.transform.position).normalized with
    {
      y = 0.0f
    };
    if ((double) this.LJSAngle(this.transform.forward, normalized) >= (double) this._viewAngle / 2.0)
      return (Collider) null;
    float maxDistance = Vector3.Distance(this.transform.position, transform.position);
    RaycastHit hitInfo;
    if (Physics.Raycast(this.transform.position, normalized, out hitInfo, maxDistance, (int) this._whatIsObstacle))
    {
      this._isFindTarget = false;
      this._obstacleTarget = hitInfo.collider;
      this._visibleTarget = (Collider) null;
      return hitInfo.collider;
    }
    this._isFindTarget = true;
    this._visibleTarget = this._visibleTargets[0];
    FadeEffect component;
    if (this._visibleTarget.TryGetComponent<FadeEffect>(out component) && !component._coroStart)
      component.StartFadeCoro();
    return this._visibleTarget;
  }

  public float LJSAngle(Vector3 from, Vector3 to)
  {
    return Mathf.Acos((float) ((double) from.x * (double) to.x + (double) from.y + (double) to.y + (double) from.z * (double) to.z)) * 57.29578f;
  }

  public Vector3 DirFromAngle(float angleDeg, bool global)
  {
    if (!global)
      angleDeg += this.transform.eulerAngles.y;
    return new Vector3(Mathf.Sin(angleDeg * ((float) Math.PI / 180f)), 0.0f, Mathf.Cos(angleDeg * ((float) Math.PI / 180f)));
  }

  public bool ShowGizoms() => this._showGizoms;
}
