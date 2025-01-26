using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager instance;

    [SerializeField]
    private int[] powerUps = new int[] { 0, 0, 0, 0 };

    private void Awake()
    {
        Debug.Log("RoomManager Awake");
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

    public void SetPowerUp(int powerUp, int quantity)
    {
        powerUps[powerUp] = quantity;
    }

    public void ResetPowerUp()
    {
        powerUps = new int[] { 0, 0, 0, 0 };
    }

    public int[] GetPowerUps()
    {
        return powerUps;
    }
}
