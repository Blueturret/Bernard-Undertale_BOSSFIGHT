using System.Collections;
using UnityEngine;
public class FloorIsLavaObstacle : Obstacle
{
    PlayerHealth playerHealth;

    bool canDamage = true;
    Vector2 startPos = new Vector2(0, -2.50f);
    
    protected override void Awake()
    {
        base.Awake();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    public override void OnObjectSpawned()
    {
        canDamage = true;
        rb.linearVelocity = -Vector2.right * speed;

        GetComponent<Animation>().Play();
    }

    private void Start()
    {
        canDamage = true;
        rb.linearVelocity = -Vector2.right * speed;
    }

    private void Update()
    {
        if (transform.position.x <= -11.5f)
        {
            transform.position = startPos;
        }
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
}
