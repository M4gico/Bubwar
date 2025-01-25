using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Debug.Log("Joueur mort");
        }
    }
}
