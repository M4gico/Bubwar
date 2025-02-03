using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManagement : MonoBehaviour
{
  [Header("Menu")]
  [SerializeField]
  private GameObject mainMenu;
  [SerializeField]
  private GameObject optionsMenu;
  [SerializeField]
  private GameObject creditsMenu;

  [Header("Intro")]
  [SerializeField]
  private Button introButton;
  [SerializeField]
  private GameObject intro;
  [SerializeField]
  private Image introBackground;
  [SerializeField]
  private Image introText;
  [SerializeField]
  private TextMeshProUGUI introTextBis;




  public void Start()
  {
    mainMenu.SetActive(true);
    optionsMenu.SetActive(false);
    creditsMenu.SetActive(false);
  }

  public void OpenOptionsMenu()
  {
    mainMenu.SetActive(false);
    optionsMenu.SetActive(true);
  }

  public void CloseOptionsMenu()
  {
    mainMenu.SetActive(true);
    optionsMenu.SetActive(false);
  }

  public void OpenCreditsMenu()
  {
    mainMenu.SetActive(false);
    creditsMenu.SetActive(true);
  }

  public void CloseCreditsMenu()
  {
    mainMenu.SetActive(true);
    creditsMenu.SetActive(false);
  }

  private void StartGame()
  {
    GameManager.instance.StartGame();
  }

  public void QuitGame()
  {
    Application.Quit();
  }

  public void IntroStart()
  {
    intro.SetActive(true);
    mainMenu.SetActive(false);

    StartCoroutine(FadeImage(false));
  }

  public void IntroEnd()
  {
    introButton.interactable = false;
    StartCoroutine(FadeImage(true));
    StartCoroutine(Wait(1.1f));
  }

  private IEnumerator Wait(float seconds)
  {
    yield return new WaitForSeconds(seconds);
    StartGame();
    GameManager.instance.SetupPlayer();
  }

  private IEnumerator FadeImage(bool fadeAway)
  {
    if (fadeAway)
    {
      for (float i = 1; i >= 0; i -= Time.deltaTime)
      {
        // introBackground.color = new Color(1, 1, 1, i);
        introText.color = new Color(1, 1, 1, i);
        introTextBis.color = new Color(0.8156862745f, 0.7058823529f, 0.5803921569f, i);
        yield return null;
      }
    }
    else
    {
      for (float i = 0; i <= 1; i += Time.deltaTime)
      {
        introBackground.color = new Color(1, 1, 1, i);
        introText.color = new Color(1, 1, 1, i);
        introTextBis.color = new Color(0.8156862745f, 0.7058823529f, 0.5803921569f, i);
        yield return null;
      }
    }
  }
}
