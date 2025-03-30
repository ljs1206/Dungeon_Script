using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class AnimationManager : MonoSingleton<AnimationManager>
{
  private bool isChange;

  public void ChangeAnimationBool(Animator animator, string paramaterName)
  {
    this.isChange = false;
    foreach (AnimatorControllerParameter parameter in animator.parameters)
    {
      if (paramaterName == "Ready")
      {
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (animatorStateInfo.IsTag("Attack"))
        {
          this.isChange = true;
          animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
          if ((double) animatorStateInfo.normalizedTime < 1.0)
            break;
        }
      }
      if (parameter.type == AnimatorControllerParameterType.Bool)
      {
        if (paramaterName == parameter.name)
        {
          animator.SetBool(parameter.name, true);
          this.isChange = true;
        }
        else
          animator.SetBool(parameter.name, false);
      }
    }
    if (this.isChange)
      return;
    Debug.Log((object) ("Animator parameter named " + paramaterName + " could not be found."));
  }

  public void ChangeAnimationTrigger(Animator animator, string paramaterName)
  {
    this.isChange = false;
    foreach (AnimatorControllerParameter parameter in animator.parameters)
    {
      if (paramaterName == "Ready" && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
      {
        if ((double) animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0)
          this.isChange = true;
        else
          break;
      }
      if (parameter.type == AnimatorControllerParameterType.Bool)
      {
        if (paramaterName == parameter.name)
        {
          animator.SetTrigger(parameter.name);
          this.isChange = true;
        }
      }
      else
        animator.SetBool(parameter.name, false);
    }
    if (this.isChange)
      return;
    Debug.Log((object) ("Animator parameter named " + paramaterName + " could not be found."));
  }

  public void StopAndWait(Animator animator, float waitTime, Action endAction = null, Action startAction = null)
  {
    this.StartCoroutine(this.StopAndWaitCoro(animator, waitTime, endAction, startAction));
  }

  private IEnumerator StopAndWaitCoro(
    Animator animator,
    float waitTime,
    Action endAction,
    Action startAction)
  {
    animator.speed = 0.0f;
    Action action1 = startAction;
    if (action1 != null)
      action1();
    yield return (object) new WaitForSeconds(waitTime);
    animator.speed = 1f;
    Action action2 = endAction;
    if (action2 != null)
      action2();
  }
}
