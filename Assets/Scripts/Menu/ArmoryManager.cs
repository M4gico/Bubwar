using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ArmoryManager : MonoBehaviour
{
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

  private void Start()
  {
    levelIcons = new List<Image>[upgradesButtons.Length];
    for (int i = 0; i < upgradesButtons.Length; i++)
    {
      levelIcons[i] = new List<Image>();
    }
    GenerateLevelIcons();
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
      Debug.Log("upgrade level : " + (upgradesLevel[0] - 1));
      levelIcons[0][upgradesLevel[0] - 1].sprite = fullSprite;
    }
    DisableAllButtons();
  }

  public void UpgradeSpeed()
  {
    upgradesLevel[1]++;
    if (upgradesLevel[1] <= upgradesMaxLevel[1])
    {
      Debug.Log("upgrade level : " + (upgradesLevel[1] - 1));
      levelIcons[1][upgradesLevel[1] - 1].sprite = fullSprite;
    }
    DisableAllButtons();
  }

  public void UpgradeWeapon()
  {
    upgradesLevel[2]++;
    if (upgradesLevel[2] <= upgradesMaxLevel[2])
    {
      Debug.Log("upgrade level : " + (upgradesLevel[2] - 1));
      levelIcons[2][upgradesLevel[2] - 1].sprite = fullSprite;
    }
    DisableAllButtons();
  }

  public void UpgradeHp()
  {
    upgradesLevel[3]++;
    if (upgradesLevel[3] <= upgradesMaxLevel[3])
    {
      Debug.Log("upgrade level : " + (upgradesLevel[3] - 1));
      levelIcons[3][upgradesLevel[3] - 1].sprite = fullSprite;
    }
    DisableAllButtons();
  }

  private void DisableAllButtons()
  {
    foreach (Button button in upgradesButtons)
      button.interactable = false;
  }

  public void EnableAllButtons()
  {
    for (int i = 0; i < upgradesButtons.Length; i++)
    {
      if (upgradesLevel[i] < upgradesMaxLevel[i])
        upgradesButtons[i].interactable = true;
    }
  }
}
