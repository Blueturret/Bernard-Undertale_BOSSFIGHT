using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] TextMeshProUGUI healthText;
    
    [SerializeField] float health;
    float maxHealth = 92;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            health -= 5;
            if (health <= 0)
            {
                Debug.Log("You ded");
                health = maxHealth;
            }

            // Change UI
            healthBar.fillAmount = health / maxHealth;
            healthText.text = health.ToString() + " / " + maxHealth.ToString();
        }
    }

    public void Heal(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            health += 5;
            health = Mathf.Clamp(health, 1, maxHealth);

            // Change UI
            healthBar.fillAmount = health / maxHealth;
            healthText.text = health.ToString() + " / " + maxHealth.ToString();
        }
    }
}
