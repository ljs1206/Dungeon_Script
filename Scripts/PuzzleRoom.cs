using UnityEngine;

#nullable disable
public class PuzzleRoom : Room
{
  [SerializeField]
  private PuzzleType _puzzleType;
  public bool _puzzleEnd;
}
