using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
// Gestion de la vie du joueur
{
    AudioManager audioManager;

    // UI
    [SerializeField] Image healthBar;
    [SerializeField] TextMeshProUGUI healthText;

    float health; // C'est un float pour que la division par maxHealth fonctionne
    int maxHealth = 92;

    private void Start()
    {
        audioManager = AudioManager.instance;
        
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    // Fonction pour faire prendre des degats au joueur
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health <= 0)
        {
            Die();
        }

        // Audio
        audioManager.PlaySound("PlayerDamage");

        // Change UI
        healthBar.fillAmount = health / maxHealth;
        healthText.text = health.ToString() + " / " + maxHealth.ToString();
    }

    public void Heal(float amount)
    // Fonction pour soigner le joueur
    {
        health += amount;
        health = Mathf.Clamp(health, 1, maxHealth);

        // Audio
        audioManager.PlaySound("PlayerHeal");

        // Change UI
        healthBar.fillAmount = health / maxHealth;
        healthText.text = health.ToString() + " / " + maxHealth.ToString();
    }

    void Die()
    // Gestion de la mort du joueur
    {
        StartCoroutine(GameManager.instance.LoadScene(0));
    }
}
