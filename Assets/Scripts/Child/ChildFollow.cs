using UnityEngine;

public class ChildFollow : MonoBehaviour
{
    [SerializeField] private float moveSpeedChild;
    [SerializeField] private float smoothFactor;
    [SerializeField] private float lengthLimitToTarget;

    private Transform playerTransform;
    private float angleGOtoTarget;
    private Vector3 vectorZero = Vector3.zero;
    private Vector2 differenceToTarget;
    private Rigidbody2D rb;

    private Vector3 vel;

    private void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Rotate the object to the player transform (target)
        angleGOtoTarget = Mathf.Atan2(playerTransform.position.y - transform.position.y, playerTransform.position.x - transform.position.x);
        angleGOtoTarget = (180 / Mathf.PI) * angleGOtoTarget - 90;

        transform.rotation = Quaternion.Euler(0f, 0f, angleGOtoTarget);
        Debug.DrawLine(transform.position, playerTransform.position, Color.red, Time.deltaTime);

        differenceToTarget = new Vector2(playerTransform.position.x - transform.position.x, playerTransform.position.y - transform.position.y);
        
        //move to the player but not too close 
        if (differenceToTarget.magnitude > lengthLimitToTarget)
        {
             vel = new Vector2(differenceToTarget.x * moveSpeedChild, differenceToTarget.y * moveSpeedChild);
        }
        else
        {
             vel = new Vector2(0,0);
        }
        //ref vectorZero write the actual velocity to continue to change it after
        rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, vel, ref vectorZero, smoothFactor);
    }
}
