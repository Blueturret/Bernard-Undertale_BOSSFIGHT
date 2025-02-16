using Unity.VisualScripting;
using UnityEngine;

public class Boomerang : Obstacle
{
    Transform playerPosition;

    [SerializeField] float repelForce;

    protected override void Awake()
    {
        base.Awake();
        playerPosition = GameObject.Find("Player").transform;
    }

    void FixedUpdate()
    {
        float velocity = speed * Mathf.Pow(Vector2.Distance(playerPosition.position, transform.position), 2);
        rb.AddForce((playerPosition.position - transform.position) * speed * Time.deltaTime);
        rb.AddTorque(10f);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    // Infliger des dégâts, ou repousser les boomerangs quand ils se touchent
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Boomerang"))
        {
            rb.AddForce((transform.position - collision.transform.position) * repelForce);
        }
    }
}
