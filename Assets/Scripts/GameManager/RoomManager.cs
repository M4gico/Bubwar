using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    [SerializeField]
    private int currentRoomIndex;

    [SerializeField]
    private Transform playerTransform;

    [Header("Rooms choose")]
    [SerializeField]
    private string finalRoomName;
    [SerializeField]
    private int nbRooms;

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
        currentRoomIndex = 0;
    }

    public void StartGame()
    {
        currentRoomIndex++;
        // SceneManager.LoadScene(GetRandomChildrenRoom());
        SceneManager.LoadScene("TutorialRoom");
        playerTransform.position = new Vector3(0, -4, 0);
    }

    void Update()
    {

    }

    public void LoadNextRoom()
    {
        if (currentRoomIndex + 1 >= nbRooms)
        {
            playerTransform.position = new Vector3(0, -4, 0);
            SceneManager.LoadScene(finalRoomName);
            return;
        }
        LoadRandomRoom();
    }

    private void LoadRandomRoom()
    {
        int randRoomValue = UnityEngine.Random.Range(0, 100);
        int roomProbabilityValue = 0;
        string nextSceneName = string.Empty;
        for (int i = 0; i < roomsProbabilities.Length; i++)
        {
            roomProbabilityValue += roomsProbabilities[i];
            if (randRoomValue <= roomProbabilityValue)
            {
                nextSceneName = rooms[i];
                break;
            }
        }
        if (nextSceneName == string.Empty)
            nextSceneName = rooms[0]; // Default to the first room (children room)

        if (nextSceneName == rooms[0]) // Children room
            nextSceneName = GetRandomChildrenRoom();
        currentRoomIndex++;
        SceneManager.LoadScene(nextSceneName);
        playerTransform.position = new Vector3(0, -4, 0);
    }

    private void IncressDifficulty()
    {
        spawnTimeChildren *= 1 - ((100 / (10 + currentRoomIndex)) / 100);
        spawnNbChildren += UnityEngine.Random.Range(1, 3);
        spawnQuantityChildren += 1;

        nbChildren = spawnQuantityChildren;
    }

    private string GetRandomChildrenRoom()
    {
        int rand = UnityEngine.Random.Range(1, nbChildrenRooms + 1);
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
        if (nbChildren <= 0)
            GameObject.Find("FinalDoor").GetComponent<DoorManager>().OpenDoor();
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

    public void ResetRoomManager()
    {
        currentRoomIndex = 0;

        nbChildren = 1;
        spawnTimeChildren = 2f;
        spawnNbChildren = 1;
        spawnQuantityChildren = 1;
    }
}