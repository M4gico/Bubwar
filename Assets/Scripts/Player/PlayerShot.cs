using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShot : MonoBehaviour
{
    [SerializeField] private GameObject bubbleBullet;
    [SerializeField] private Transform forwardWeapon;


    private InputAction rightClickMouseAction;
    private float rightClickMouseState;
    private Vector2 mousePos;

    private Transform playerTransform;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        //Get the playermovement script of the parent
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerTransform = GetComponentInParent<Transform>();
    }

    private void Start()
    {
        rightClickMouseAction = InputSystem.actions.FindAction("Attack");
    }

    private void FixedUpdate()
    {
        rightClickMouseState = rightClickMouseAction.ReadValue<float>();
        mousePos = playerMovement.mousPos;

        if(rightClickMouseState == 1f)
        {
            Instantiate(bubbleBullet, playerTransform);
        }

    }

}
