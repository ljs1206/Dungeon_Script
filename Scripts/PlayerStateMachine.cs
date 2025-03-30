using System.Collections.Generic;

#nullable disable
public class PlayerStateMachine
{
  public Dictionary<PlayerStateEnum, PlayerState> stateDictionary;
  private Player _player;

  public PlayerState CurrentState { get; private set; }

  public PlayerStateMachine()
  {
    this.stateDictionary = new Dictionary<PlayerStateEnum, PlayerState>();
  }

  public void Initialize(PlayerStateEnum startState, Player player)
  {
    this._player = player;
    this.CurrentState = this.stateDictionary[startState];
    this.CurrentState.Enter();
  }

  public void ChangeState(PlayerStateEnum newState)
  {
    if (!this._player.CanStateChangeable)
      return;
    this.CurrentState.Exit();
    this.CurrentState = this.stateDictionary[newState];
    this.CurrentState.Enter();
  }

  public void AddState(PlayerStateEnum stateEnum, PlayerState state)
  {
    this.stateDictionary.Add(stateEnum, state);
  }
}
