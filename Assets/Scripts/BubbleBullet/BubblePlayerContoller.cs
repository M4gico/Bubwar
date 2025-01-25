using UnityEngine;

public class BubblePlayerContoller : MonoBehaviour
{
    [SerializeField] private float timeBeforeDisappear;
    [SerializeField] private float damage;

    private CircleCollider2D colliderObject;
    private Collider2D[] childCollidersToDamage;

    private Vector2 centerCollider;
    private float radiusCollider = 0.5f;

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
        //Destroy(gameObject, timeBeforeDisappear);
    }

    private void Update()
    {
        //childColliders = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(physicWeaponEquip.reachX, physicWeaponEquip.reachY), 0, LayerMask.GetMask("Enemy"));
        childCollidersToDamage = Physics2D.OverlapCircleAll(new Vector2(0,0), radiusCollider, LayerMask.GetMask("Child"));
        if(childCollidersToDamage == null || childCollidersToDamage.Length == 0)
        {
            Debug.Log("colliders null");
        }
        for (int i = 0; i < childCollidersToDamage.Length; i++)
        {
            //Take the EnemyHealth script of the enemy and take the function TakeDamage
            childCollidersToDamage[i].GetComponent<ChildHealth>().TakeDamage(damage);
            Debug.Log("Attack child");
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(centerCollider, 1);
    }
}
