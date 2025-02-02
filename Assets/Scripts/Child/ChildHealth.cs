using UnityEngine;

public class ChildHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private GameObject itemRecharge;
    [SerializeField] private Sprite deadChildSprite;

    private ChildFollow childFollow;
    private Animator animator;
    private SpriteRenderer graphics;
    private Collider2D coll;
    private Rigidbody2D rb;
    private ChildWeapon childWeapon;

    private void Awake()
    {
        graphics = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        childFollow = GetComponent<ChildFollow>();
        childWeapon = GetComponentInChildren<ChildWeapon>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (0.5f > Random.Range(0f, 1f))
            {
                Instantiate(itemRecharge, transform.position, transform.rotation);
            }
            childWeapon.enabled = false;
            childFollow.enabled = false;
            coll.enabled = false;
            graphics.sortingOrder = 1;

            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector3.zero;

            RoomManager.instance.DecressNbChildren();

            animator.SetBool("isDead", true);
            GetComponent<ChildSound>().PlayDeathSound();
        }
        else
        {
            GetComponent<ChildSound>().PlayTakeDamageSound();
        }
    }
}

