using UnityEngine;

public class ColoredObstacle : Obstacle
{
    int isOrange;

    protected override void Awake() { base.Awake(); } // Est-ce que cette ligne est vraiment necessaire pour appeler le Awake() par defaut ?
    public override void OnObjectSpawned()
    {
        isOrange = Random.Range(0, 2);
        UpdateColor();
        
        rb.linearVelocity = new Vector2(-1, 0) * speed;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Detection de la velocite du joueur
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
                GetComponent<SpriteRenderer>().color = new Color32(252, 166, 0, 150);
                break;
            case 1:
                GetComponent<SpriteRenderer>().color = new Color32(66, 252, 255, 150);
                break;
        }
    }
}
