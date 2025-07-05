using UnityEngine;

public class Platform : MonoBehaviour
// Gestion des soft platforms
{
    Rigidbody2D rb;
    Vector2 initialPos;

    [SerializeField] float speed = 1.2f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        rb.linearVelocity = -Vector2.right * speed;
        initialPos = transform.position;
    }

    private void OnDisable()
    {
        transform.position = initialPos;
    }
}
