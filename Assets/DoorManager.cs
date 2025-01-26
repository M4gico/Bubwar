using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField]
    private Sprite doorOpen;
    [SerializeField]
    private Sprite doorClosed;

    [SerializeField]
    private bool isDoorOpen;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = doorClosed;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.75f, 1.85f);

        isDoorOpen = false;
    }


    public void OpenDoor()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0.9f);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.75f, 0.7f);

        isDoorOpen = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isDoorOpen)
        {
            RoomManager.instance.LoadNextRoom();
        }
    }
}
