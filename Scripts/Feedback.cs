using UnityEngine;

#nullable disable
public abstract class Feedback : MonoBehaviour
{
  protected Agent _owner;

  public abstract void CreateFeedback();

  public abstract void FinishFeedback();

  private void Awake() => this._owner = this.transform.parent.GetComponent<Agent>();

  protected virtual void OnDisable() => this.FinishFeedback();
}
