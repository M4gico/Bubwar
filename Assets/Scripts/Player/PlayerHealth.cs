using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] [Range(0.05f, 1f)] private float invincibilityFlashDelay;
    [SerializeField] [Range(0.1f, 3f)] private float invincibilityTimeAfterHit;

    [Header("UI part")]
    [SerializeField] private List<Image> hearthImage;
    [SerializeField] private Sprite heartGood;
    [SerializeField] private Sprite heartBroken;

    private SpriteRenderer graphics;

    private bool isInvincible;

    private void Awake()
    {
        graphics = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            health -= damage;
            if (health < 0)
            {
                health = 0;
            }
            isInvincible = true;
            Debug.Log("Player health " + health);
            ActualiseHearth();

            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public void AddLife(float life)
    {
        health += life;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        ActualiseHearth();
    }

    private void ActualiseHearth()
    {
        for(int i = 0; i < hearthImage.Count; i++)
        {
            if(i < health)
            {
                hearthImage[i].sprite = heartGood;
            }
            else
            {
                hearthImage[i].sprite = heartBroken;
            }
        }
    }

    private IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }

    private IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityTimeAfterHit);
        isInvincible = false;
    }
}
