using UnityEngine;

public class BubbleBulletController : MonoBehaviour
{
    [SerializeField] private float timeBeforeDisappear;

    //Animations part
    private Animator animatorBubble;
    [SerializeField] private AnimatorOverrideController overrideController;

    private void Awake()
    {
        animatorBubble = GetComponent<Animator>();
    }

    private void Start()
    {
        //Destroy the gameobject after timeBeforeDisappear in seconds
        Destroy(gameObject, timeBeforeDisappear);
    }
}
