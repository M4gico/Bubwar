using UnityEngine;

public class PersonalLayoutManager : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField]
    private Transform pointTransform;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject behindPhysicCollider;
    [SerializeField]
    private GameObject frontPhysicCollider;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerTransform.position.y > pointTransform.position.y)
        {
            behindPhysicCollider.SetActive(false);
            frontPhysicCollider.SetActive(true);
            spriteRenderer.sortingOrder = 6;
        }
        else
        {
            behindPhysicCollider.SetActive(true);
            frontPhysicCollider.SetActive(false);
            spriteRenderer.sortingOrder = 2;
        }
    }
}
