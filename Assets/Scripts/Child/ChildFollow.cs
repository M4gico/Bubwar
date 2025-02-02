using UnityEngine;

public class ChildFollow : MonoBehaviour
{
    [SerializeField] private float moveSpeedChild;
    [SerializeField] private float smoothFactor;
    [SerializeField] private float lengthLimitToTarget;

    public Transform playerTransform { get; private set; }
    private float angleGOtoTarget;
    private Vector3 vectorZero = Vector3.zero;
    public Vector2 differenceToTarget { get; private set; }
    private Rigidbody2D rb;
    private Animator animator;

    private Vector3 vel;

    private void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        //Rotate the object to the player transform (target)
        angleGOtoTarget = Mathf.Atan2(playerTransform.position.y - transform.position.y, playerTransform.position.x - transform.position.x);

        if (angleGOtoTarget > -Mathf.PI / 4 && angleGOtoTarget < Mathf.PI / 4)
        {
            //Right
            animator.SetInteger("IdleSide", 0);
        }
        else if (angleGOtoTarget > Mathf.PI / 4 && angleGOtoTarget < 3 * Mathf.PI / 4)
        {
            //Front
            animator.SetInteger("IdleSide", 3);
        }
        else if ((angleGOtoTarget > 3 * Mathf.PI / 4 && angleGOtoTarget < Mathf.PI) || (angleGOtoTarget < -3 * Mathf.PI / 4 && angleGOtoTarget > -Mathf.PI))
        {
            //Left
            animator.SetInteger("IdleSide", 1);
        }
        else
        {
            //Back
            animator.SetInteger("IdleSide", 2);
        }

        /*
        angleGOtoTarget = (180 / Mathf.PI) * angleGOtoTarget - 90;

        transform.rotation = Quaternion.Euler(0f, 0f, angleGOtoTarget);
        */
        Debug.DrawLine(transform.position, playerTransform.position, Color.red, Time.deltaTime);

        differenceToTarget = new Vector2(playerTransform.position.x - transform.position.x, playerTransform.position.y - transform.position.y);
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        //move to the player but not too close 
        if (differenceToTarget.magnitude > lengthLimitToTarget)
        {
            vel = new Vector2(differenceToTarget.x * moveSpeedChild, differenceToTarget.y * moveSpeedChild);
        }
        else
        {
            vel = new Vector2(0, 0);
        }
        //ref vectorZero write the actual velocity to continue to change it after
        rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, vel, ref vectorZero, smoothFactor);
    }
}
