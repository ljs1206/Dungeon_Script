using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class MathTestScript : MonoBehaviour
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
  public float meshResolution;
  public int edgeResolveIterations;
  public float edgeDstThreshold;
  public MeshFilter viewMeshFilter;
  private Mesh viewMesh;
  public float _delayTime = 0.2f;

  private void Start()
  {
    this.viewMesh = new Mesh();
    this.viewMesh.name = "View Mesh";
    this.viewMeshFilter.mesh = this.viewMesh;
    this.StartCoroutine(this.FindDelayCoro());
  }

  private IEnumerator FindDelayCoro()
  {
    while (true)
    {
      Debug.Log((object) this._isFindTarget);
      yield return (object) new WaitForSeconds(this._delayTime);
      this.CheckFleidView();
    }
  }

  private void LateUpdate() => this.DrawMesh();

  private Collider CheckFleidView()
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

  private void DrawMesh()
  {
    int num1 = Mathf.RoundToInt(this._viewAngle * this.meshResolution);
    float num2 = this._viewAngle / (float) num1;
    List<Vector3> vector3List = new List<Vector3>();
    MathTestScript.ViewCastInfo minViewCast = new MathTestScript.ViewCastInfo();
    for (int index = 0; index <= num1; ++index)
    {
      MathTestScript.ViewCastInfo maxViewCast = this.ViewCast((float) ((double) this.transform.eulerAngles.y - (double) this._viewAngle / 2.0 + (double) num2 * (double) index));
      if (index > 0)
      {
        bool flag = (double) Mathf.Abs(minViewCast.dst - maxViewCast.dst) > (double) this.edgeDstThreshold;
        if (minViewCast.hit != maxViewCast.hit || ((!minViewCast.hit ? 0 : (maxViewCast.hit ? 1 : 0)) & (flag ? 1 : 0)) != 0)
        {
          MathTestScript.EdgeInfo edge = this.FindEdge(minViewCast, maxViewCast);
          if (edge.pointA != Vector3.zero)
            vector3List.Add(edge.pointA);
          if (edge.pointB != Vector3.zero)
            vector3List.Add(edge.pointB);
        }
      }
      vector3List.Add(maxViewCast.point);
      minViewCast = maxViewCast;
    }
    int length = vector3List.Count + 1;
    Vector3[] vector3Array = new Vector3[length];
    int[] numArray = new int[(length - 2) * 3];
    vector3Array[0] = Vector3.zero;
    for (int index = 0; index < length - 1; ++index)
    {
      vector3Array[index + 1] = this.transform.InverseTransformPoint(vector3List[index]);
      if (index < length - 2)
      {
        numArray[index * 3] = 0;
        numArray[index * 3 + 1] = index + 1;
        numArray[index * 3 + 2] = index + 2;
      }
    }
    this.viewMesh.Clear();
    this.viewMesh.vertices = vector3Array;
    this.viewMesh.triangles = numArray;
    this.viewMesh.RecalculateNormals();
  }

  private MathTestScript.EdgeInfo FindEdge(
    MathTestScript.ViewCastInfo minViewCast,
    MathTestScript.ViewCastInfo maxViewCast)
  {
    float num1 = minViewCast.angle;
    float num2 = maxViewCast.angle;
    Vector3 _pointA = Vector3.zero;
    Vector3 _pointB = Vector3.zero;
    for (int index = 0; index < this.edgeResolveIterations; ++index)
    {
      float globalAngle = (float) (((double) num1 + (double) num2) / 2.0);
      MathTestScript.ViewCastInfo viewCastInfo = this.ViewCast(globalAngle);
      bool flag = (double) Mathf.Abs(minViewCast.dst - viewCastInfo.dst) > (double) this.edgeDstThreshold;
      if (viewCastInfo.hit == minViewCast.hit && !flag)
      {
        num1 = globalAngle;
        _pointA = viewCastInfo.point;
      }
      else
      {
        num2 = globalAngle;
        _pointB = viewCastInfo.point;
      }
    }
    return new MathTestScript.EdgeInfo(_pointA, _pointB);
  }

  private MathTestScript.ViewCastInfo ViewCast(float globalAngle)
  {
    Vector3 direction = this.DirFromAngle(globalAngle, true);
    RaycastHit hitInfo;
    return Physics.Raycast(this.transform.position, direction, out hitInfo, this._viewRadius, (int) this._whatIsObstacle) ? new MathTestScript.ViewCastInfo(true, hitInfo.point, hitInfo.distance, globalAngle) : new MathTestScript.ViewCastInfo(false, this.transform.position + direction * this._viewRadius, (float) (int) this._whatIsObstacle, globalAngle);
  }

  public struct ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
  {
    public bool hit = _hit;
    public Vector3 point = _point;
    public float dst = _dst;
    public float angle = _angle;
  }

  public struct EdgeInfo(Vector3 _pointA, Vector3 _pointB)
  {
    public Vector3 pointA = _pointA;
    public Vector3 pointB = _pointB;
  }
}
