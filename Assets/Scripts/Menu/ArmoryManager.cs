using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ArmoryManager : MonoBehaviour
{
  [Header("power ups")]
  [SerializeField] private float fovPowerUp;
  [SerializeField] private float speedPowerUp;
  [SerializeField] private float weaponPowerUp;

  [SerializeField]
  private Button[] upgradesButtons;

  [SerializeField]
  private int[] upgradesMaxLevel;
  [SerializeField]
  private int[] upgradesLevel;

  [SerializeField]
  private GameObject[] levelIconsContainer;
  [SerializeField]
  private List<Image>[] levelIcons;

  [SerializeField]
  private GameObject emptySpritePrefab;
  [SerializeField]
  private GameObject fullSpritePrefab;

  [SerializeField]
  private Sprite fullSprite;

  private GameObject player;

  private void Awake()
  {
    player = GameObject.FindWithTag("Player");
  }
  private void Start()
  {
    levelIcons = new List<Image>[upgradesButtons.Length];
    for (int i = 0; i < upgradesButtons.Length; i++)
    {
      levelIcons[i] = new List<Image>();
    }
    upgradesLevel = PowerUpManager.instance.GetPowerUps();
    GenerateLevelIcons();
    SetAllButtons();
  }

  private void GenerateLevelIcons()
  {
    for (int i = 0; i < upgradesButtons.Length; i++)
    {
      for (int j = 0; j < upgradesMaxLevel[i]; j++)
      {
        GameObject objectIcon;

        if (j < upgradesLevel[i])
          objectIcon = Instantiate(fullSpritePrefab, levelIconsContainer[i].transform);
        else
          objectIcon = Instantiate(emptySpritePrefab, levelIconsContainer[i].transform);
        Image image = objectIcon.GetComponent<Image>();
        levelIcons[i].Add(image);
      }
    }
  }

  public void UpgradeRadius()
  {
    upgradesLevel[0]++;
    if (upgradesLevel[0] <= upgradesMaxLevel[0])
    {
      GetComponent<ArmorySound>().PlayVisionUp(upgradesLevel[0]);
      player.GetComponent<PlayerVisionController>().fovAngle += fovPowerUp;
      Debug.Log("upgrade level : " + (upgradesLevel[0] - 1));
      levelIcons[0][upgradesLevel[0] - 1].sprite = fullSprite;
      PowerUpManager.instance.SetPowerUp(0, upgradesLevel[0]);
    }
    DisableAllButtons();
  }

  public void UpgradeSpeed()
  {
    upgradesLevel[1]++;
    if (upgradesLevel[1] <= upgradesMaxLevel[1])
    {
      GetComponent<ArmorySound>().PlaySpeedUp(upgradesLevel[1]);
      player.GetComponent<PlayerMovement>().moveSpeed += speedPowerUp;
      Debug.Log("upgrade level : " + (upgradesLevel[1] - 1));
      levelIcons[1][upgradesLevel[1] - 1].sprite = fullSprite;
      PowerUpManager.instance.SetPowerUp(1, upgradesLevel[1]);
    }
    DisableAllButtons();
  }

  public void UpgradeWeapon()
  {
    upgradesLevel[2]++;
    if (upgradesLevel[2] <= upgradesMaxLevel[2])
    {
      GetComponent<ArmorySound>().PlayGunUp(upgradesLevel[2]);
      player.GetComponentInChildren<PlayerShot>().decrementGauchePerShot /= weaponPowerUp;
      Debug.Log("upgrade level : " + (upgradesLevel[2] - 1));
      levelIcons[2][upgradesLevel[2] - 1].sprite = fullSprite;
      PowerUpManager.instance.SetPowerUp(2, upgradesLevel[2]);
    }
    DisableAllButtons();
  }

  public void UpgradeHp()
  {

    upgradesLevel[3]++;
    if (upgradesLevel[3] <= upgradesMaxLevel[3])
    {
      GetComponent<ArmorySound>().PlayHealthUp(upgradesLevel[3]);
      player.GetComponent<PlayerHealth>().GiveHP();
      Debug.Log("upgrade level : " + (upgradesLevel[3] - 1));
      levelIcons[3][upgradesLevel[3] - 1].sprite = fullSprite;
      PowerUpManager.instance.SetPowerUp(3, upgradesLevel[3]);
    }
    DisableAllButtons();
  }

  private void DisableAllButtons()
  {
    foreach (Button button in upgradesButtons)
      button.interactable = false;

    OpenDoor();
  }

  public void SetAllButtons()
  {
    for (int i = 0; i < upgradesButtons.Length; i++)
    {
      if (upgradesLevel[i] < upgradesMaxLevel[i])
        upgradesButtons[i].interactable = true;
      else
        upgradesButtons[i].interactable = false;
    }
  }

  private void OpenDoor()
  {
    GameObject.Find("FinalDoor").GetComponent<DoorManager>().OpenDoor();
  }

  public void CloseArmory()
  {
    gameObject.GetComponent<Canvas>().enabled = false;
    InteractiveUpgrade interactiveUpgrade = GameObject.Find("tableauUpgrade").GetComponent<InteractiveUpgrade>();

    interactiveUpgrade.PlayArmoryOpenClosedSound();
    interactiveUpgrade.SetArmoryState(true);
  }
}
