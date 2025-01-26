using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    private int currentRoomIndex;

    [SerializeField]
    private string[] rooms;
    [SerializeField]
    private int[] roomsProbabilities;

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

    void Start()
    {
        Debug.Log("RoomManager Start");
        currentRoomIndex = 0;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(rooms[0]); // Load the first room
        currentRoomIndex++;
    }

    void Update()
    {

    }

    public void LoadNextRoom()
    {
        currentRoomIndex++;
        LoadRandomRoom();
    }

    private void LoadRandomRoom()
    {
        int randRoomValue = UnityEngine.Random.Range(0, 100);
        Debug.Log("Random scene value: " + randRoomValue);
        int roomProbabilityValue = 0;
        string nextSceneName = string.Empty;
        for (int i = 0; i < roomsProbabilities.Length; i++)
        {
            roomProbabilityValue += roomsProbabilities[i];
            if (randRoomValue <= roomProbabilityValue)
            {
                currentRoomIndex = i;
                nextSceneName = rooms[i];
                break;
            }
        }
        if (nextSceneName == string.Empty)
            nextSceneName = rooms[0]; // Default to the first room (children room)

        currentRoomIndex++;
        SceneManager.LoadScene(nextSceneName);
    }

    public int GetCurrentRoomIndex()
    {
        return currentRoomIndex;
    }
}