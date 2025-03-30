using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class FeedbackPlayer : MonoBehaviour
{
  private List<Feedback> _feedbackToPlay;

  private void Awake()
  {
    this._feedbackToPlay = ((IEnumerable<Feedback>) this.GetComponents<Feedback>()).ToList<Feedback>();
  }

  public void PlayFeedback()
  {
    this.FinishFeedback();
    this._feedbackToPlay.ForEach((Action<Feedback>) (f => f.CreateFeedback()));
  }

  public void FinishFeedback()
  {
    this._feedbackToPlay.ForEach((Action<Feedback>) (f => f.FinishFeedback()));
  }
}
