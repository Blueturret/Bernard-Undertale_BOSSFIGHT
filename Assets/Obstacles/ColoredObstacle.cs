using UnityEngine;

public class ColoredObstacle : MonoBehaviour, IPooledObject
{
    Rigidbody2D rb;

    [Header("Properties")]
    int isOrange;
    [SerializeField] float speed;
    [SerializeField] float damage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void IPooledObject.OnObjectSpawned()
    {
        isOrange = Random.Range(0, 2);
        UpdateColor();
        
        rb.linearVelocity = new Vector2(-1, 0) * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 playerVelocity = collision.GetComponent<Rigidbody2D>().linearVelocity;

            if (isOrange == 1 && playerVelocity.magnitude != 0)
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                gameObject.SetActive(false);
            }
            
            if (isOrange == 0 && playerVelocity.magnitude == 0)
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                gameObject.SetActive(false);
            }
        }
    }

    void UpdateColor()
    {
        switch(isOrange)
        {
            case 0:
                GetComponent<SpriteRenderer>().color = new Color32(252, 166, 0, 255);
                break;
            case 1:
                GetComponent<SpriteRenderer>().color = new Color32(66, 252, 255, 255);
                break;
        }
    }
}
