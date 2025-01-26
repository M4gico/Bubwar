using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject hearthGO;
    [SerializeField] private float initialHealth = 3;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField][Range(0.05f, 1f)] private float invincibilityFlashDelay;
    [SerializeField][Range(0.1f, 3f)] private float invincibilityTimeAfterHit;

    [Header("UI part")]
    [SerializeField] private List<Animator> hearthAnimator;

    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private SpriteRenderer graphics;

    private bool isInvincible;

    private void Awake()
    {
        graphics = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            health -= damage;
            if (health <= 0)
            {
                StartCoroutine(PlayerDead());
                GetComponent<PlayerSound>().PlayDieSound();
            }
            else
            {
                GetComponent<PlayerSound>().PlayTakeDamageSound();
            }
            isInvincible = true;
            Debug.Log("Player health " + health);
            ActualiseHearth();

            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    private IEnumerator PlayerDead()
    {
        // rb.bodyType = RigidbodyType2D.Kinematic;
        // rb.linearVelocity = Vector3.zero;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenuScene");

        GameManager.instance.UnSetupPlayer();
        ResetPlayerHealth();
        ActualiseHearth();
        PowerUpManager.instance.ResetPowerUp();

    }

    public void GiveHP()
    {

    }

    public void AddLife(float life)
    {
        GetComponent<PlayerSound>().PlayRegenLifeSound();
        health += life;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        Debug.Log("Give life");
        ActualiseHearth();
    }

    private void ActualiseHearth()
    {
        for (int i = 0; i < hearthAnimator.Count; i++)
        {
            if (i < health)
            {
                hearthAnimator[i].SetBool("HeartDestroy", false);
            }
            else
            {
                hearthAnimator[i].SetBool("HeartDestroy", true);
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

    public void ResetPlayerHealth()
    {
        maxHealth = initialHealth;
        health = maxHealth;
        isInvincible = false;
        ActualiseHearth();
        graphics.color = new Color(1f, 1f, 1f, 1f);
    }
}
