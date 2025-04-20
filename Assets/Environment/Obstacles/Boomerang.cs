using UnityEngine;

public class Boomerang : Obstacle
{
    Transform playerPosition;

    [SerializeField] float repelForce;

    float maxVelocity;

    protected override void Awake()
    {
        base.Awake();
        playerPosition = GameObject.Find("Player").transform;
    }

    public override void OnObjectSpawned()
    {
        speed = 0;
        maxVelocity = 74.999f; // Mathf.Pow(Vector2.Distance(playerPosition.position, transform.position), 2);
    }

    void FixedUpdate()
    {
        speed = Mathf.Lerp(0, maxVelocity, Time.deltaTime * 75);
        print(speed);

        rb.AddForce((playerPosition.position - transform.position) * speed * Time.deltaTime);
        rb.AddTorque(15);
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
