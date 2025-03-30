using System;
using UnityEngine;
using UnityEngine.Events;

#nullable disable
public class Player : Agent
{
  [Header("Setting Values")]
  public float moveSpeed = 8f;
  public float dashSpeed = 20f;
  public RollingDirection rollingDirection;
  [Header("Attack Settings")]
  public float attackSpeed = 1f;
  public int currentComboCounter;
  public float[] attackMovement;
  public AnimationEvent _animationEvent;
  [SerializeField]
  private PlayerInputCompo _playerInput;
  public UnityEvent OnGameOverEvent;
  [Header("CameraEffect")]
  [SerializeField]
  [Range(0.0f, 50f)]
  private float _amplitudeGain;
  [SerializeField]
  [Range(0.0f, 50f)]
  private float _frequencyGain;
  [SerializeField]
  [Range(0.0f, 50f)]
  private float _time;
  [HideInInspector]
  public bool _isFullCharing;
  [HideInInspector]
  public AttackType _attackType;

  public PlayerStateMachine StateMachine { get; protected set; }

  public PlayerInputCompo PlayerInput => this._playerInput;

  public PlayerVFX PlayerVFXCompo => this.VFXCompo as PlayerVFX;

  public bool CanHit { get; set; } = true;

  protected override void Awake()
  {
    base.Awake();
    this.StateMachine = new PlayerStateMachine();
    foreach (PlayerStateEnum stateEnum in Enum.GetValues(typeof (PlayerStateEnum)))
    {
      string str = stateEnum.ToString();
      try
      {
        PlayerState instance = Activator.CreateInstance(System.Type.GetType(nameof (Player) + str + "State"), (object) this, (object) this.StateMachine, (object) str) as PlayerState;
        this.StateMachine.AddState(stateEnum, instance);
      }
      catch (Exception ex)
      {
        Debug.LogError((object) (str + " is loading error! check Message"));
        Debug.LogError((object) ex.Message);
      }
    }
  }

  protected void Start() => this.StateMachine.Initialize(PlayerStateEnum.Idle, this);

  protected void Update() => this.StateMachine.CurrentState.UpdateState();

  public override void Attack()
  {
    if (!this.DamageCasterCompo.CastDamage())
      return;
    CameraManager instance = MonoSingleton<CameraManager>.Instance;
    double amplitudeGain = (double) this._amplitudeGain;
    float frequencyGain1 = this._frequencyGain;
    double time = (double) this._time;
    double frequencyGain2 = (double) frequencyGain1;
    instance.CameraShake((float) amplitudeGain, (float) time, (float) frequencyGain2);
  }

  public void SwordMoveOrigin() => this._animationEvent.AttackEndEvent();

  public void PlayBladeVFX() => this.PlayerVFXCompo.PlayBladeVFX(this.currentComboCounter);

  public void SetHit() => this.StateMachine.ChangeState(PlayerStateEnum.Hit);

  public void CanHitChange()
  {
    this.StartCoroutine(this.DelayCoroutine(0.4f, (Action) (() => this.CanHit = true)));
  }

  public override void SetDead()
  {
    this.isDead = true;
    this.StateMachine.ChangeState(PlayerStateEnum.Dead);
    this.CanStateChangeable = false;
  }
}
