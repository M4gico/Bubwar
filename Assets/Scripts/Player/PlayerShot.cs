using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShot : MonoBehaviour
{
    [SerializeField] private GameObject bubbleBullet;
    [SerializeField] private float forceToBubble;
    [SerializeField] private BoxCollider2D colliderSafeArea;

    [SerializeField] private Transform rightForwardWeapon;
    [SerializeField] private Transform leftForwardWeapon;

    [Header("Cooldown")]
    [SerializeField] private GameObject cooldownSliderObject;
    private Slider cooldownSlider;
    private float timeToCooldown;
    [SerializeField] private float cooldownToShot;

    [Header("Gauge")]
    [SerializeField] private SoapGaugeManager soapGaugeManager;
    public float decrementGauchePerShot;
    [SerializeField]
    private float initialDecrementGauchePerShot = 0.05f;

    private Transform weaponTransform;
    private Vector2 weaponTransformVector;
    private Vector2 differenceVector;

    private float initialGaugeShot;
    private float gaugeShot;

    private InputAction rightClickMouseAction;
    private float rightClickMouseState;
    private Vector2 mousePos;
    private float gunHeat;

    private Transform playerTransform;
    private PlayerMovement playerMovement;

    private bool canShot = true;

    public static PlayerShot instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerShot dans la sc�ne");
            return;
        }
        instance = this;
        //Get the playermovement script of the parent
        playerMovement = GetComponentInParent<PlayerMovement>();
        playerTransform = GetComponentInParent<Transform>();
        cooldownSlider = cooldownSliderObject.GetComponent<Slider>();
        cooldownSlider.maxValue = cooldownToShot;
        initialGaugeShot = 1f;
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
                gunHeat += Time.deltaTime;
                if (gaugeShot > 0f && canShot)
                {
                    if (gunHeat > cooldownToShot)
                    {
                        StartCoroutine(ChangeCooldownSlider());
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
                    }
                }
                else
                {
                    GetComponentInParent<PlayerSound>().PlayNoAmmoSound();
                }
            }
        }
    }

    private IEnumerator ChangeCooldownSlider()
    {
        timeToCooldown = 0;
        cooldownSliderObject.SetActive(true);
        while (timeToCooldown < cooldownToShot)
        {
            cooldownSlider.value = Mathf.Lerp(0, cooldownToShot, timeToCooldown / cooldownToShot);
            timeToCooldown += Time.deltaTime;
            yield return null;
        }
        cooldownSliderObject.SetActive(false);
    }

    public void AddGauge(float gaugeValue)
    {
        gaugeShot += gaugeValue;
        if (gaugeShot > 1f)
        {
            gaugeShot = 1f;
        }
        soapGaugeManager.SetCrop(gaugeShot);
        GetComponentInParent<PlayerSound>().PlayRegenAmmoSound();
    }

    public void resetPlayerShot()
    {
        gaugeShot = initialGaugeShot;
        soapGaugeManager.SetCrop(gaugeShot);

        decrementGauchePerShot = initialDecrementGauchePerShot;
    }

    public void SetCanShot(bool canShot)
    {
        this.canShot = canShot;
    }
}