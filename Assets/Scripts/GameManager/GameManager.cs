using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool isGameOver;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        isGameOver = false;
    }

    void Update()
    {
        if (isGameOver)
        {
            // Handle game over state
        }
    }

    public void StartGame()
    {
        isGameOver = false;
        RoomManager.instance.StartGame();
    }

    public void EndGame()
    {
        isGameOver = true;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
