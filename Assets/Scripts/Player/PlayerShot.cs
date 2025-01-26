using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShot : MonoBehaviour
{
    [SerializeField] private GameObject bubbleBullet;
    [SerializeField] private float cooldownToShot;
    [SerializeField] private float forceToBubble;
    [SerializeField] private BoxCollider2D colliderSafeArea;

    [SerializeField] private Transform rightForwardWeapon;
    [SerializeField] private Transform leftForwardWeapon;
    [SerializeField] private SoapGaugeManager soapGaugeManager;

    [Header("Gauge")]
    [SerializeField] private float decrementGauchePerShot;

    private Transform weaponTransform;
    private Vector2 weaponTransformVector;
    private Vector2 differenceVector;

    private float gaugeShot;

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
        gaugeShot = 1f;
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
        if (!colliderSafeArea.bounds.Contains(mousePos))
        {
            if (rightClickMouseState == 1f)
            {
                if (gaugeShot > 0f)
                {
                    gunHeat += Time.deltaTime;
                    if (gunHeat > cooldownToShot)
                    {
                        //Know if the player is see at right or left
                        if (playerMovement.isFacingRight)
                        {
                            weaponTransform = rightForwardWeapon;
                        }
                        else
                        {
                            weaponTransform = leftForwardWeapon;
                        }
                        GameObject bubbleBulletGO = Instantiate(bubbleBullet, weaponTransform.position, playerTransform.rotation);
                        gunHeat = 0;
                        Rigidbody2D bubbleBulletrb = bubbleBulletGO.GetComponent<Rigidbody2D>();

                        weaponTransformVector = new Vector2(weaponTransform.position.x, weaponTransform.position.y);
                        differenceVector = mousePos - weaponTransformVector;

                        //Get the orientation of the weapon compared to the mouse with transform.right
                        bubbleBulletrb.AddForce(differenceVector * forceToBubble);
                        gaugeShot -= decrementGauchePerShot;
                        soapGaugeManager.SetCrop(gaugeShot);
                        Debug.Log("Gauge value" + gaugeShot);
                    }
                }
                else
                {
                    Debug.Log("Plus de jauge");
                }
            }
        }
    }

    public void AddGauge(float gaugeValue)
    {
        gaugeShot += gaugeValue;
        if(gaugeShot > 1f)
        {
            gaugeValue = 1f;
        }
        soapGaugeManager.SetCrop(gaugeShot);

    }
}