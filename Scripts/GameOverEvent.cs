using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#nullable disable
public class GameOverEvent : MonoBehaviour
{
  private Transform _canvasTrm;
  [Header("Compo")]
  [SerializeField]
  private Transform _gameOverMassage;
  [SerializeField]
  private Image _blackPanel;
  [Header("GameOverSetting")]
  [SerializeField]
  private float _blackFadeTime;
  private bool _endGameOverEvent;

  private void Awake()
  {
    this._canvasTrm = this.GetComponent<Transform>();
    this._blackPanel.color = (Color) Vector4.zero;
    this._endGameOverEvent = false;
  }

  private void Update()
  {
    if (!this._endGameOverEvent || !Keyboard.current.anyKey.wasPressedThisFrame)
      return;
    this._endGameOverEvent = false;
    SceneManager.LoadScene("map");
  }

  public void GameOver()
  {
    MonoSingleton<CameraManager>.Instance.ZoomInOut(5.25f, 2f, 0.1f, ZoomType.ZoomIn, new Action(this.ShowGameOverScene));
  }

  public void ShowGameOverScene()
  {
    this._gameOverMassage.gameObject.SetActive(true);
    this.StartCoroutine(this.DelayCoro(3f));
  }

  public IEnumerator DelayCoro(float delayTime)
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    GameOverEvent gameOverEvent = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated method
      DOTweenModuleUI.DOFade(gameOverEvent._blackPanel, 1f, gameOverEvent._blackFadeTime).OnComplete<TweenerCore<Color, Color, ColorOptions>>(new TweenCallback(gameOverEvent.\u003CDelayCoro\u003Eb__9_0));
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) new WaitForSeconds(delayTime);
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;
  }
}
