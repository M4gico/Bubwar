using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    private Animator animator;

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
        SceneManager.LoadScene("MainMenuScene");
    }

    public void SetGoMainMenu()
    {
        animator.SetTrigger("FadeOut");
    }
}
