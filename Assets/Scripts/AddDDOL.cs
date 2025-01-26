using UnityEngine;
using UnityEngine.SceneManagement;

public class AddDDOL : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjectsToDDOL;

    private void Awake()
    {
        for(int i = 0; i < gameObjectsToDDOL.Length; i++)
        {
            DontDestroyOnLoad(gameObjectsToDDOL[i]);
        }
        SceneManager.LoadScene("MainMenuScene");
        Debug.Log("DDOL activated");
    }
}
