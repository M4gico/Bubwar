using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class InteractiveUpgrade : MonoBehaviour
{
    private GameObject bubbleInteractive;
    private Canvas armuerieCanva;
    private InputAction EKeyAction;
    private bool isInTrigger;
    private float EKeyValue;
    private bool armuerieState = true;
    private bool delayToActiveState;

    private void Awake()
    {
        bubbleInteractive = GameObject.FindGameObjectWithTag("Interactive");
        armuerieCanva = GameObject.FindWithTag("Armuerie").GetComponent<Canvas>();
    }

    private void Start()
    {
        armuerieCanva.enabled = false;
        EKeyAction = InputSystem.actions.FindAction("Interact");
    }

    private void Update()
    {
        if (isInTrigger && !delayToActiveState)
        {
            EKeyValue = EKeyAction.ReadValue<float>();
            if(EKeyValue == 1f)
            {
                StartCoroutine(ChangeStateArmuerie());
            }

        }

    }

    private IEnumerator ChangeStateArmuerie()
    {
        delayToActiveState = true;
        armuerieCanva.enabled = armuerieState;
        yield return new WaitForSeconds(0.5f);
        armuerieState = !armuerieState;
        delayToActiveState = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bubbleInteractive.SetActive(true);
            isInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bubbleInteractive.SetActive(false);
            isInTrigger = false;
            armuerieCanva.enabled = false;
        }
    }


}
