using UnityEngine;

public class Platform : MonoBehaviour
// Gestion des soft platforms
{
    Rigidbody2D rb;

    [SerializeField] float speed = 1.2f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        rb.linearVelocity = -Vector2.right * speed;
    }
}
