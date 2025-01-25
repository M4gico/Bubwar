using UnityEngine;

public class BubbleChildController : MonoBehaviour
{
    [SerializeField] private float damage;

    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>() != null)
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        rb.linearVelocity = new Vector2(0, 0);
        animator.SetBool("isDestroy", true);
    }
    private void DestroyGO()
    {
        Destroy(gameObject);
    }
}