using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private Image gameOverBackground;
    [SerializeField]
    private Image gameOverLogo;


    [SerializeField]
    private TextMeshProUGUI restartTextButton;
    [SerializeField]
    private TextMeshProUGUI mainMenuTextButton;

    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button mainMenuButton;

    // [SerializeField]
    // private bool isGameOver;

    public void GameOver()
    {
        // isGameOver = true;
        gameOverUI.SetActive(true);
        StartCoroutine(FadeImage(false));

        ChildrenManager childrenManager = GameObject.Find("ChildrenManager").GetComponent<ChildrenManager>();
        childrenManager.StopSpawnChild();

        System.Array.ForEach(GameObject.FindGameObjectsWithTag("Child"), Child => Destroy(Child));
        System.Array.ForEach(GameObject.FindGameObjectsWithTag("Bubble"), bubble => bubble.SetActive(false));

        GameManager.instance.UnSetupPlayer();
    }


    public void RestartGame()
    {
        StartCoroutine(GameOverExitRestart());
    }

    public void MainMenu()
    {
        StartCoroutine(GameOverExitMainMenu());
    }

    private IEnumerator GameOverExitRestart()
    {
        // isGameOver = false;
        restartButton.interactable = false;
        mainMenuButton.interactable = false;
        yield return StartCoroutine(FadeImage(true));
        yield return new WaitForSeconds(0.9f);
        GameManager.instance.UnSetupPlayer();

        RoomManager.instance.ResetRoomManager();
        GameManager.instance.SetupPlayer();
        RoomManager.instance.StartGame();
        restartButton.interactable = true;
        mainMenuButton.interactable = true;
        gameOverUI.SetActive(false);
    }
    private IEnumerator GameOverExitMainMenu()
    {
        // isGameOver = false;
        restartButton.interactable = false;
        mainMenuButton.interactable = false;
        yield return StartCoroutine(FadeImage(true));
        yield return new WaitForSeconds(0.9f);
        GameManager.instance.UnSetupPlayer();

        SceneManager.LoadScene("MainMenuScene");
        restartButton.interactable = true;
        mainMenuButton.interactable = true;
        gameOverUI.SetActive(false);
    }

    private IEnumerator FadeImage(bool fadeAway)
    {
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // introBackground.color = new Color(1, 1, 1, i);
                gameOverLogo.color = new Color(1, 1, 1, i);
                restartTextButton.color = new Color(0.8156862745f, 0.7058823529f, 0.5803921569f, i);
                mainMenuTextButton.color = new Color(0.8156862745f, 0.7058823529f, 0.5803921569f, i);
                yield return null;
            }
        }
        else
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                gameOverBackground.color = new Color(1, 1, 1, i);
                gameOverLogo.color = new Color(1, 1, 1, i);
                restartTextButton.color = new Color(0.8156862745f, 0.7058823529f, 0.5803921569f, i);
                mainMenuTextButton.color = new Color(0.8156862745f, 0.7058823529f, 0.5803921569f, i);
                yield return null;
            }
        }
    }
}
