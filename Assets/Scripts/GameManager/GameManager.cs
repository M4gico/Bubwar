using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool isGameOver;

    [SerializeField]
    private GameOverManager gameOverManager;

    [SerializeField]
    private GameObject[] playerUI;
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
        foreach (GameObject pUI in playerUI)
        {
            pUI.SetActive(false);
        }
    }


    public void StartGame()
    {
        isGameOver = false;
        RoomManager.instance.StartGame();
        SetupPlayer();
    }

    public void EndGame()
    {
        isGameOver = true;
        gameOverManager.GameOver();

    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void SetupPlayer()
    {
        player.SetActive(true);
        GameObject.Find("Player").GetComponent<PlayerHealth>().ResetPlayerHealth();
        playerShot.SetCanShot(true);
        foreach (GameObject pUI in playerUI)
        {
            pUI.SetActive(true);
        }
    }

    public void UnSetupPlayer()
    {
        playerShot.ResetPlayerShot(); // Reset player shot bubules price
        playerShot.SetCanShot(false);
        PlayerMovement.instance.ResetMoveSpeed(); // Reset player speed
        PlayerVisionController.instance.ResetVision(); // Reset player vision

        player.SetActive(false);
        foreach (GameObject pUI in playerUI)
        {
            pUI.SetActive(false);
        }

        PowerUpManager.instance.ResetPowerUp(); // Reset power up levels
        RoomManager.instance.ResetRoomManager(); // Reset room manager stats

    }

    public void SwitchProps()
    {
        foreach (GameObject pUI in playerUI)
        {
            if (pUI.name == "PlayerConeVision")
            {
                pUI.SetActive(!pUI.activeSelf);
                return;
            }
        }
    }
}
