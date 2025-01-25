using UnityEngine;

public class ChildHealth : MonoBehaviour
{
    [SerializeField] private float health;

    public void TakeDamage(float damage)
    {
        Debug.Log(health);
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
