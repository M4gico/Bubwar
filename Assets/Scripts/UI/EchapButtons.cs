using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EchapButtons : MonoBehaviour
{
    private InputAction escapeAction;
    private float escapeValue;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject UIMenu;
    private bool isOptionsMenuOpen = false;
    private bool delayToActiveState = false;

    private void Start()
    {
        escapeAction = InputSystem.actions.FindAction("Echap");
    }

    private void Update()
    {
        escapeValue = escapeAction.ReadValue<float>();
        if (escapeValue == 1f && !delayToActiveState)
        {
            Debug.Log("EchapButtons pressed");
            if (isOptionsMenuOpen)
            {
                Time.timeScale = 1;
                optionsMenu.SetActive(false);
                isOptionsMenuOpen = false;
                PlayerVisionController.instance.SetVisionActive(true);
            }
            else
            {
                Time.timeScale = 0;
                optionsMenu.SetActive(true);
                isOptionsMenuOpen = true;
                PlayerVisionController.instance.SetVisionActive(false);
            }
            StartCoroutine(DelayToActiveState());
        }     
    }

    private IEnumerator DelayToActiveState()
    {
        delayToActiveState = true;
        yield return new WaitForSecondsRealtime(0.2f);
        delayToActiveState = false;
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        Application.Quit();
    }

    public void OpenOptionsMenu()
    {
        Debug.Log("OpenOptionsMenu");

        isOptionsMenuOpen = false;
        PlayerHealth.instance.ResetPlayerHealth();
        PlayerShot.instance.ResetPlayerShot();
        PlayerVisionController.instance.SetVisionActive(true);
        UIMenu.SetActive(false);
        optionsMenu.SetActive(false);
        Time.timeScale = 1;

        RoomManager.instance.ResetRoomManager();
        SceneManager.LoadScene("MainMenuScene");
    }

    public void RestartGame()
    {
        Debug.Log("RestartGame");

        isOptionsMenuOpen = false;
        PlayerHealth.instance.ResetPlayerHealth();
        PlayerShot.instance.ResetPlayerShot();
        PlayerVisionController.instance.SetVisionActive(true);
        optionsMenu.SetActive(false);
        Time.timeScale = 1;

        RoomManager.instance.ResetRoomManager();
        RoomManager.instance.StartGame();
    }
}
