using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

#nullable disable
public class SceneManagent : MonoSingleton<SceneManagent>
{
  private void Start()
  {
    SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.LoadAction);
  }

  private void LoadAction(Scene arg0, LoadSceneMode arg1)
  {
    MonoSingleton<GameManager>.Instance.PlayerTrm = GameObject.FindWithTag("Player").transform;
  }
}
