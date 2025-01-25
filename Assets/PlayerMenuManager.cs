using UnityEngine;

public class PlayerMenuManager : MonoBehaviour
{
  [Header("Menu")]
  [SerializeField]
  private GameObject armoryMenu;

  [SerializeField]
  private GameObject optionsMenu;

  public void Start()
  {
    armoryMenu.SetActive(false);
    optionsMenu.SetActive(false);
  }

  public void OpenArmoryMenu()
  {
    armoryMenu.SetActive(true);
    optionsMenu.SetActive(false);
  }

  public void CloseMenu()
  {
    armoryMenu.SetActive(false);
    optionsMenu.SetActive(false);
  }

  public void OpenOptionsMenu()
  {
    armoryMenu.SetActive(false);
    optionsMenu.SetActive(true);
  }
}
