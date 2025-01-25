using UnityEngine;

public class BubbleBulletController : MonoBehaviour
{
    [SerializeField] private float timeBeforeDisappear;

    private void Start()
    {
        //Destroy the gameobject after timeBeforeDisappear in seconds
        Destroy(gameObject, timeBeforeDisappear);
    }
}
