using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    private int currentRoomIndex;

    [Header("Rooms choose")]
    [SerializeField]
    private int nbChildrenRooms;
    [SerializeField]
    private string[] rooms;
    [SerializeField]
    private int[] roomsProbabilities;


    [Header("Rooms difficulty")]
    [SerializeField]
    private int nbChildren = 1;

    [SerializeField]
    private float spawnTimeChildren = 2f;
    [SerializeField]
    private int spawnNbChildren = 1;
    [SerializeField]
    private int spawnQuantityChildren = 1;

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
        currentRoomIndex++;
        SceneManager.LoadScene(GetRandomChildrenRoom()); // Load the first room
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

        if (nextSceneName == rooms[0]) // Children room
            GetRandomChildrenRoom();
        currentRoomIndex++;
        SceneManager.LoadScene(nextSceneName);
    }

    private void IncressDifficulty()
    {
        spawnTimeChildren *= 1 - ((100 / (10 + currentRoomIndex)) / 100);
        spawnNbChildren += UnityEngine.Random.Range(1, 3);
        spawnQuantityChildren += 1;
    }

    private string GetRandomChildrenRoom()
    {
        int rand = UnityEngine.Random.Range(1, nbChildrenRooms);
        Debug.Log("Random children room value: " + rand);
        IncressDifficulty();
        return rooms[0] + rand;
    }

    public int GetCurrentRoomIndex()
    {
        return currentRoomIndex;
    }

    public void DecressNbChildren()
    {
        nbChildren--;
    }

    public int GetNbChildren()
    {
        return nbChildren;
    }

    public float GetSpawnTimeChildren()
    {
        return spawnTimeChildren;
    }

    public int GetSpawnNbChildren()
    {
        return spawnNbChildren;
    }

    public int GetSpawnQuantityChildren()
    {
        return spawnQuantityChildren;
    }
}