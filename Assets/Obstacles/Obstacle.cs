using UnityEngine;

public class Obstacle : MonoBehaviour, IPooledObject
{
    GameObject player;
    Rigidbody2D rb;

    [Header("Properties")]
    [SerializeField] float damage;
    [SerializeField] float speed;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        
        rb = GetComponent<Rigidbody2D>();   
    }

    // Mouvement de base de cet obstacle
    public void OnObjectSpawned()
    {
        Vector2 dir = player.transform.position - transform.position;
        
        rb.linearVelocity = dir.normalized * speed;
    }

    // Gestion des degats
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
