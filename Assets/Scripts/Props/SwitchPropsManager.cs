using System;
using System.Collections;
using UnityEngine;

public class SwitchPropsManager : MonoBehaviour
{
    [SerializeField]
    private bool isTutorial = false;

    [Header("Switch settings")]
    [SerializeField]
    private bool canBeUsed = true;
    [SerializeField]
    private float switchCooldown = 0.2f;

    [SerializeField]
    private float lightOffProbability = 0.4f;
    public void Start()
    {
        if (!isTutorial)
        {
            float rand = UnityEngine.Random.Range(0f, 1f);
            Debug.Log("SwitchPropsManager Start : " + rand);
            bool lightOff = rand < lightOffProbability;
            if (lightOff)
            {
                GameManager.instance.SwitchSetup(lightOff);
            }
            else gameObject.SetActive(false);
        }
    }

    public void UseSwicht()
    {
        if (canBeUsed)
        {
            StartCoroutine(SwitchCooldown());
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

    IEnumerator SwitchCooldown()
    {
        canBeUsed = false;
        yield return new WaitForSeconds(switchCooldown);
        canBeUsed = true;
    }
}
