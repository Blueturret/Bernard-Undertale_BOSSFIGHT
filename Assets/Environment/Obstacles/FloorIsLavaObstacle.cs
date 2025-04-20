using NUnit.Framework.Constraints;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
public class FloorIsLavaObstacle : Obstacle
{
    PlayerHealth playerHealth;

    bool canDamage = true;
    
    protected override void Awake()
    {
        base.Awake();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    public override void OnObjectSpawned()
    {
        transform.position = new Vector2(transform.position.x + 15, -3.35f);

        canDamage = true;
        rb.linearVelocity = -Vector2.right * speed;

        StartCoroutine(ResetPosition()); // !!! FONCTION TEMPORAIRE !!!
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        return; // Vider la fonction mere 'OnTriggerEnter2D' pour override la gestion des degats par defaut
    }

    void OnTriggerStay2D(Collider2D collision)
    // Gestion des degats
    {
        if (collision.gameObject.CompareTag("Player") && canDamage)
        {
            playerHealth.TakeDamage(damage);

            StartCoroutine(DamageCooldown(0.05f));
        }
    }

    IEnumerator DamageCooldown(float cooldown)
    {
        canDamage = false;

        yield return new WaitForSeconds(cooldown);

        canDamage = true;
    }

    IEnumerator ResetPosition()
    // !!! FONCTION TEMPORAIRE !!!
    {
        if (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(4);

            transform.position = new Vector2(transform.position.x + 15, -2.9f);

            StartCoroutine(ResetPosition());
        }
    }
}
