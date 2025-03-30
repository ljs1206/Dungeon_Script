using UnityEngine;

#nullable disable
public interface IMovement
{
  Vector3 Velocity { get; }

  bool IsGround { get; }

  void Initialize(Agent agent);

  void SetMovement(Vector3 movement, bool isRotation = true);

  void StopImmediately();

  void SetDestination(Vector3 destination);

  void GetKnockback(Vector3 force);
}
