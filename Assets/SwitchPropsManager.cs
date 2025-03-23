using UnityEngine;

public class SwitchPropsManager : MonoBehaviour
{
    [SerializeField]
    private bool isTutorial = false;


    public void UseSwicht()
    {
        if (isTutorial)
        {
            GameManager.instance.SwitchProps();
            GameObject.Find("FinalDoor").GetComponent<DoorManager>().OpenDoor();
        }
        else
        {
            GameManager.instance.SwitchProps();
        }
    }
}
