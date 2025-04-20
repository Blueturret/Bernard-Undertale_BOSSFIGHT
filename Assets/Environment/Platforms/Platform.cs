using UnityEngine;

public class Platform : MonoBehaviour, IPooledObject
{
    Rigidbody2D rb;

    float minSpeed = 1.2f;
    float maxSpeed = 1.5f;
    public bool isMovable = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnObjectSpawned()
    {
        if (!isMovable) return;

        float speed = Random.Range(minSpeed, maxSpeed + 1);

        rb.linearVelocity = -Vector2.right * speed;
    }
}
