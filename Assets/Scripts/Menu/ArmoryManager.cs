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
  private GameObject levelIconPrefab;

  [SerializeField]
  private Color basicColor = Color.white;
  [SerializeField]
  private Color upgradedColor = Color.green;

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
        GameObject newImage = Instantiate(levelIconPrefab, levelIconsContainer[i].transform);
        Image image = newImage.GetComponent<Image>();
        if (j < upgradesLevel[i])
        {
          image.color = upgradedColor;
        }
        else
        {
          image.color = basicColor;
        }
        levelIcons[i].Add(image);
      }
    }
  }

  public void UpgradeWeapon()
  {
    upgradesLevel[0]++;
    if (upgradesLevel[0] <= upgradesMaxLevel[0])
    {
      Debug.Log("upgrade level : " + (upgradesLevel[0] - 1));
      levelIcons[0][upgradesLevel[0] - 1].color = upgradedColor;
    }
    if (upgradesLevel[0] >= upgradesMaxLevel[0])
      upgradesButtons[0].interactable = false;
  }

  public void UpgradeHp()
  {
    upgradesLevel[1]++;
    if (upgradesLevel[1] <= upgradesMaxLevel[1])
    {
      Debug.Log("upgrade level : " + (upgradesLevel[1] - 1));
      levelIcons[1][upgradesLevel[1] - 1].color = upgradedColor;
    }
    if (upgradesLevel[1] >= upgradesMaxLevel[1])
      upgradesButtons[1].interactable = false;
  }

  public void UpgradeSpeed()
  {
    upgradesLevel[2]++;
    if (upgradesLevel[2] <= upgradesMaxLevel[2])
    {
      Debug.Log("upgrade level : " + (upgradesLevel[2] - 1));
      levelIcons[2][upgradesLevel[2] - 1].color = upgradedColor;
    }
    if (upgradesLevel[2] >= upgradesMaxLevel[2])
      upgradesButtons[2].interactable = false;
  }

  public void UpgradeRadius()
  {
    upgradesLevel[3]++;
    if (upgradesLevel[3] <= upgradesMaxLevel[3])
    {
      Debug.Log("upgrade level : " + (upgradesLevel[3] - 1));
      levelIcons[3][upgradesLevel[3] - 1].color = upgradedColor;
    }
    if (upgradesLevel[3] >= upgradesMaxLevel[3])
      upgradesButtons[3].interactable = false;
  }
}
