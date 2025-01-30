using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float damage;

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        
        rb.linearVelocity = new Vector2 (-3, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
