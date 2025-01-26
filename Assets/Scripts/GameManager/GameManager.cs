using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool isGameOver;

    [SerializeField]
    private GameObject playerUI;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private PlayerShot playerShot;

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

    public void SetupPlayer()
    {
        player.SetActive(true);
        playerUI.SetActive(true);
    }
    public void UnSetupPlayer()
    {
        player.SetActive(false);
        playerUI.SetActive(false);
        playerShot.resetPlayerShot();

        RoomManager.instance.ResetRoomManager();
    }
}
