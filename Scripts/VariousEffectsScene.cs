using UnityEngine;
using UnityEngine.UI;

#nullable disable
public class VariousEffectsScene : MonoBehaviour
{
  public Transform[] m_effects;
  public GameObject scaleform;
  public GameObject[] m_destroyObjects = new GameObject[30];
  public GameObject FriendlyEnemyObject;
  private GameObject gm;
  public int inputLocation;
  public Text m_scalefactor;
  public static float m_gaph_scenesizefactor = 1f;
  public Text m_effectName;
  private int index;

  private void Awake()
  {
    this.inputLocation = 0;
    this.m_effectName.text = this.m_effects[this.index].name.ToString();
    this.MakeObject();
  }

  private void Update()
  {
    this.InputKey();
    if (this.index < 70)
      this.FriendlyEnemyObject.SetActive(false);
    else
      this.FriendlyEnemyObject.SetActive(true);
  }

  private void InputKey()
  {
    if (Input.GetKeyDown(KeyCode.Z))
    {
      if (this.index <= 0)
        this.index = this.m_effects.Length - 1;
      else
        --this.index;
      this.MakeObject();
    }
    if (Input.GetKeyDown(KeyCode.X))
    {
      if (this.index >= this.m_effects.Length - 1)
        this.index = 0;
      else
        ++this.index;
      this.MakeObject();
    }
    if (!Input.GetKeyDown(KeyCode.C))
      return;
    this.MakeObject();
  }

  private void MakeObject()
  {
    this.DestroyGameObject();
    this.gm = Object.Instantiate<Transform>(this.m_effects[this.index], this.m_effects[this.index].transform.position, this.m_effects[this.index].transform.rotation).gameObject;
    this.m_effectName.text = (this.index + 1).ToString() + " : " + this.m_effects[this.index].name.ToString();
    this.scaleform.transform.position = this.gm.transform.position;
    this.gm.transform.parent = this.scaleform.transform;
    this.gm.transform.localScale = new Vector3(1f, 1f, 1f);
    float gaphScenesizefactor = VariousEffectsScene.m_gaph_scenesizefactor;
    if (this.index < 70)
      gaphScenesizefactor *= 0.5f;
    this.gm.transform.localScale = new Vector3(gaphScenesizefactor, gaphScenesizefactor, gaphScenesizefactor);
    this.m_destroyObjects[this.inputLocation] = this.gm;
    ++this.inputLocation;
  }

  private void DestroyGameObject()
  {
    for (int index = 0; index < this.inputLocation; ++index)
      Object.Destroy((Object) this.m_destroyObjects[index]);
    this.inputLocation = 0;
  }

  public void GetSizeFactor()
  {
    VariousEffectsScene.m_gaph_scenesizefactor = float.Parse(this.m_scalefactor.text.ToString());
    float gaphScenesizefactor = VariousEffectsScene.m_gaph_scenesizefactor;
    if (this.index < 70)
      gaphScenesizefactor *= 0.5f;
    this.gm.transform.localScale = new Vector3(gaphScenesizefactor, gaphScenesizefactor, gaphScenesizefactor);
  }
}
