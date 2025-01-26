using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void GoMainMenu()
    {
        RoomManager.instance.ResetRoomManager();
        SceneManager.LoadScene("MainMenuScene");

        GameObject.Find("Player").GetComponent<PlayerHealth>().ResetPlayerHealth();
        GameObject.Find("GameManager").GetComponent<GameManager>().UnSetupPlayer();
    }

    public void SetGoMainMenu()
    {
        animator.SetTrigger("FadeOut");
    }
}
