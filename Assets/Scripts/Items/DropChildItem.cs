using UnityEngine;

public class DropChildItem : MonoBehaviour
{
    [SerializeField] private float AddGaugeValue;
    private PlayerShot playerShot;

    private void Awake()
    {
        playerShot = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerShot>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerShot.AddGauge(AddGaugeValue);
            Destroy(gameObject);
        }
    }
}