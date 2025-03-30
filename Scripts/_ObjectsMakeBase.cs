using UnityEngine;

#nullable disable
public class _ObjectsMakeBase : MonoBehaviour
{
  public GameObject[] m_makeObjs;

  public float GetRandomValue(float value) => Random.Range(-value, value);

  public float GetRandomValue2(float value) => Random.Range(0.0f, value);

  public Vector3 GetRandomVector(Vector3 value)
  {
    Vector3 randomVector;
    randomVector.x = this.GetRandomValue(value.x);
    randomVector.y = this.GetRandomValue(value.y);
    randomVector.z = this.GetRandomValue(value.z);
    return randomVector;
  }

  public Vector3 GetRandomVector2(Vector3 value)
  {
    Vector3 randomVector2;
    randomVector2.x = this.GetRandomValue2(value.x);
    randomVector2.y = this.GetRandomValue2(value.y);
    randomVector2.z = this.GetRandomValue2(value.z);
    return randomVector2;
  }
}