using UnityEngine;

#nullable disable
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
  private static T _instance;
  private static bool isDestroyed;

  public static T Instance
  {
    get
    {
      if (MonoSingleton<T>.isDestroyed)
        MonoSingleton<T>._instance = default (T);
      if ((Object) MonoSingleton<T>._instance == (Object) null)
      {
        MonoSingleton<T>._instance = Object.FindObjectOfType<T>();
        if ((Object) MonoSingleton<T>._instance == (Object) null)
          Debug.LogError((object) (typeof (T).Name + " singleton is not exist"));
        else
          MonoSingleton<T>.isDestroyed = false;
      }
      return MonoSingleton<T>._instance;
    }
  }

  public virtual void Awake()
  {
    if ((Object) MonoSingleton<T>._instance == (Object) null)
    {
      MonoSingleton<T>._instance = this as T;
      Object.DontDestroyOnLoad((Object) this.gameObject);
    }
    else
      Object.Destroy((Object) this.gameObject);
  }

  private void OnDestroy() => MonoSingleton<T>.isDestroyed = true;
}