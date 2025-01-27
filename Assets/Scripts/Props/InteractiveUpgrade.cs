using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class InteractiveUpgrade : MonoBehaviour
{
    private enum ActionToPlay { Heal, End, Armurerie };
    [SerializeField] private float AddHealing;

    [SerializeField] private ActionToPlay actionChoose;
    private SpriteRenderer bubbleInteractive;
    private Canvas armuerieCanva;
    private InputAction EKeyAction;
    [SerializeField]
    private bool isInTrigger;
    private float EKeyValue;


    [SerializeField]
    private bool armuerieState = true;
    [SerializeField]
    private bool delayToActiveState;
    private PlayerHealth playerHealth;

    private Animator endAnimator;


    private void Awake()
    {
        bubbleInteractive = GameObject.FindGameObjectWithTag("Interactive").GetComponent<SpriteRenderer>();
        if (actionChoose == ActionToPlay.Armurerie)
        {
            armuerieCanva = GameObject.FindWithTag("Armuerie").GetComponent<Canvas>();
        }
        else if (actionChoose == ActionToPlay.Heal)
        {
            playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        }
        else
        {
            endAnimator = GameObject.FindWithTag("EndUI").GetComponent<Animator>();
        }

    }

    private void Start()
    {
        if (actionChoose == ActionToPlay.Armurerie)
        {
            armuerieCanva.enabled = false;
        }
        EKeyAction = InputSystem.actions.FindAction("Interact");
    }

    private void Update()
    {
        if (isInTrigger && !delayToActiveState)
        {
            EKeyValue = EKeyAction.ReadValue<float>();
            Debug.Log("[InteractiveUpgrade] EKeyValue: " + EKeyValue);
            if (EKeyValue == 1f)
            {
                if (actionChoose == ActionToPlay.Armurerie)
                {
                    Debug.Log("[InteractiveUpgrade] Armurerie");
                    StartCoroutine(ChangeStateArmuerie());
                }
                else if (actionChoose == ActionToPlay.Heal)
                {
                    playerHealth.AddLife(AddHealing);
                    GameObject.Find("FinalDoor").GetComponent<DoorManager>().OpenDoor();
                    Destroy(gameObject);
                }
                else if (actionChoose == ActionToPlay.End)
                {
                    endAnimator.SetTrigger("FadeIn");
                }

            }
        }
    }

    private IEnumerator ChangeStateArmuerie()
    {
        if (armuerieState)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Armory/OpenArmory");
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Armory/CloseArmory");
        }
        delayToActiveState = true;
        armuerieCanva.enabled = armuerieState;
        GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerShot>().SetCanShot(!armuerieState);
        yield return new WaitForSeconds(0.5f);
        armuerieState = !armuerieState;
        delayToActiveState = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bubbleInteractive.enabled = true;
            isInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bubbleInteractive.enabled = false;
            isInTrigger = false;
            if (actionChoose == ActionToPlay.Armurerie)
            {
                armuerieCanva.enabled = false;
                armuerieState = true;
                GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerShot>().SetCanShot(true);
            }

        }
    }
}
