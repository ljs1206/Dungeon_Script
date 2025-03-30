using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class CameraManager : MonoSingleton<CameraManager>
{
  [SerializeField]
  private LayerMask _whatIsGround;
  [SerializeField]
  private CinemachineVirtualCamera _followCam;
  private CinemachineBasicMultiChannelPerlin perlin;
  private CinemachineFramingTransposer _ft;
  private bool isActionEnd;

  public CinemachineVirtualCamera FollowCam { get; private set; }

  public bool IsActionEnd
  {
    get => this.isActionEnd;
    set => this.isActionEnd = value;
  }

  public override void Awake()
  {
    base.Awake();
    this.FollowCam = this._followCam;
    this.perlin = this._followCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    this._ft = this._followCam.GetCinemachineComponent<CinemachineFramingTransposer>();
  }

  public bool ScreenToWorld(Vector3 screenPos, out Vector3 worldPos)
  {
    Camera main = Camera.main;
    RaycastHit hitInfo;
    bool world = Physics.Raycast(main.ScreenPointToRay(screenPos), out hitInfo, main.farClipPlane, (int) this._whatIsGround);
    worldPos = world ? hitInfo.point : Vector3.zero;
    return world;
  }

  public Vector3 GetTowardMouseDirection(Transform trm, Vector3 mouseScreenPos)
  {
    Vector3 worldPos;
    if (!this.ScreenToWorld(mouseScreenPos, out worldPos))
      return trm.forward;
    return ((worldPos - trm.position) with { y = 0.0f }).normalized;
  }

  public void CameraShake(float amplitude, float time, float frequencyGain)
  {
    this.StartCoroutine(this.CameraShakeCoro(amplitude, time, frequencyGain));
  }

  public void ZoomInOut(
    float Value,
    float mulValue,
    float delayTime,
    ZoomType zoomType,
    Action endFnc)
  {
    this.StartCoroutine(this.ZoomInOutCoro(Value, mulValue, delayTime, zoomType, endFnc));
  }

  public IEnumerator CameraShakeCoro(float amplitude, float time, float frequencyGain)
  {
    float currentTime = 0.0f;
    this.perlin.m_FrequencyGain = frequencyGain;
    while ((double) currentTime < (double) time)
    {
      currentTime += Time.deltaTime;
      float num = (float) (1.0 - (double) currentTime / (double) time);
      this.perlin.m_AmplitudeGain = amplitude * num;
      yield return (object) null;
    }
    this.perlin.m_FrequencyGain = 0.0f;
    this.perlin.m_AmplitudeGain = 0.0f;
    this.IsActionEnd = true;
  }

  public IEnumerator ZoomInOutCoro(
    float Value,
    float mulValue,
    float delayTime,
    ZoomType zoomType,
    Action endFnc)
  {
    switch (zoomType)
    {
      case ZoomType.ZoomIn:
        while ((double) this._ft.m_CameraDistance > (double) Value)
        {
          this._ft.m_CameraDistance -= Time.deltaTime;
          yield return (object) new WaitForSeconds(delayTime);
        }
        break;
      case ZoomType.ZoomInOut:
        while ((double) this._ft.m_CameraDistance < (double) Value)
        {
          this._ft.m_CameraDistance += mulValue;
          yield return (object) new WaitForSeconds(delayTime);
        }
        break;
      case ZoomType.ZoomOut:
        while ((double) this._ft.m_CameraDistance > (double) Value)
        {
          this._ft.m_CameraDistance += mulValue;
          yield return (object) new WaitForSeconds(delayTime);
        }
        break;
    }
    Action action = endFnc;
    if (action != null)
      action();
    this.IsActionEnd = true;
  }
}
