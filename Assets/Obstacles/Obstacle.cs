using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour, IPooledObject
{
    [SerializeField] float damage;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    public void OnObjectSpawned()
    {
        rb.linearVelocity = new Vector2 (-3, Random.Range(-1, 1));
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
