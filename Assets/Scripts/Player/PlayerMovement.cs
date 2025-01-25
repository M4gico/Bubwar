using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] private float moveSpeed;
  [SerializeField] private float smoothFactor;

  private Camera cam;

  private Rigidbody2D rb;

  private InputAction moveAction;
  private InputAction lookAction;

  //Movement and look of the player
  private Vector2 moveValue;
  private Vector2 lookValue;
  private Vector3 vectorZero = Vector3.zero;
  private Vector3 mousPos;
  private float angleRadMouse;
  private float angleDegMouse;

  private static PlayerMovement instance;

  private void Awake()
  {
    if (instance != null)
    {
      if (instance != null)
      {
        Debug.LogWarning("There is more than one instance of PlayerMovement");
        return;
      }

      instance = this;
    }

    cam = Camera.main;
    rb = GetComponent<Rigidbody2D>();
  }

  private void Start()
  {
    moveAction = InputSystem.actions.FindAction("Move");
    lookAction = InputSystem.actions.FindAction("CursorPos");
  }

  private void FixedUpdate()
  {
    //Get the value of inputs
    moveValue = moveAction.ReadValue<Vector2>();
    lookValue = lookAction.ReadValue<Vector2>();

    //Find the position between the cursor and the character
    mousPos = (Vector2)cam.ScreenToWorldPoint(lookValue);
    angleRadMouse = Mathf.Atan2(mousPos.y - transform.position.y, mousPos.x - transform.position.x);
    angleDegMouse = (180 / Mathf.PI) * angleRadMouse - 90;

    transform.rotation = Quaternion.Euler(0f, 0f, angleDegMouse);
    //Draw a line between the mouse and the player by a white line and actualize it every tick
    Debug.DrawLine(transform.position, mousPos, Color.white, Time.deltaTime);

    //Move the player
    Vector3 vel = new Vector2(moveValue.x * moveSpeed, moveValue.y * moveSpeed);
    rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, vel, ref vectorZero, smoothFactor);
  }

}
