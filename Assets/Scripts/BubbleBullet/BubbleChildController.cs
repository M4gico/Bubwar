using UnityEngine;

public class BubbleChildController : MonoBehaviour
{
    [SerializeField] private float timeBeforeDisappear;
    [SerializeField] private float damage;

    private CircleCollider2D colliderObject;
    private Collider2D[] playerCollidersToDamage;

    private Vector2 centerCollider;
    private float radiusCollider = 0.8347953f;

    private void Awake()
    {
        colliderObject = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        // Get the center and radius of the circle collider
        centerCollider = colliderObject.bounds.center; // Center of the collider
        //radiusCollider = colliderObject.radius; // Radius of the collider
        //Destroy the gameobject after timeBeforeDisappear in seconds
        Destroy(gameObject, timeBeforeDisappear);
    }

    private void Update()
    {

        //childColliders = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(physicWeaponEquip.reachX, physicWeaponEquip.reachY), 0, LayerMask.GetMask("Enemy"));
        playerCollidersToDamage = Physics2D.OverlapCircleAll(new Vector2(0,0), radiusCollider, LayerMask.GetMask("Player"));
        for (int i = 0; i < playerCollidersToDamage.Length; i++)
        {
            //Take the EnemyHealth script of the enemy and take the function TakeDamage
            playerCollidersToDamage[i].GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
