using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShot : MonoBehaviour
{
    [SerializeField] private GameObject bubbleBullet;
    [SerializeField] private Transform forwardWeapon;
    [SerializeField] private float cooldownToShot;
    [SerializeField] private float forceToBubble;
    [SerializeField] private BoxCollider2D colliderSafeArea;

    private Vector2 transformBullet;

    private InputAction rightClickMouseAction;
    private float rightClickMouseState;
    private Vector2 mousePos;
    private float gunHeat;

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

    private void Update()
    {
        rightClickMouseState = rightClickMouseAction.ReadValue<float>();
        mousePos = playerMovement.mousPos;

        //Verify if the cursor is not in the player
        if(!colliderSafeArea.bounds.Contains(mousePos))
        {
            if (rightClickMouseState == 1f)
            {
                gunHeat += Time.deltaTime;
                if (gunHeat > cooldownToShot)
                {
                    GameObject bubbleBulletGO = Instantiate(bubbleBullet, forwardWeapon.position, playerTransform.rotation);
                    gunHeat = 0;
                    Rigidbody2D bubbleBulletrb = bubbleBulletGO.GetComponent<Rigidbody2D>();
                    transformBullet = new Vector2(transform.position.x, transform.position.y);
                    //Get the orientation of the weapon compared to the mouse
                    transform.right = mousePos - transformBullet;
                    bubbleBulletrb.AddForce(transform.right * forceToBubble);
                }
            }
        }

        

    }
}
