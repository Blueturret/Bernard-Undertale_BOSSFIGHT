using UnityEngine;

public class Swing : MonoBehaviour
// Je repete en grande partie le script de ColoredObstacle mais j'ai pas besoin
// d'heriter de la class mere Obstacle, ca causerait trop de problemes,
// je me demande s'il y a pas une meilleure maniere de le faire
{
    public int isOrange; // Cette variable sera assignee des l'apparition de l'objet
    float damage = 45;

    private void Start()
    {
        // Donne la bonne couleur au swing
        switch (isOrange)
        {
            case 0:
                GetComponent<SpriteRenderer>().color = new Color32(252, 166, 0, 180);
                break;
            case 1:
                GetComponent<SpriteRenderer>().color = new Color32(66, 252, 255, 180);
                break;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    // Gestion des degats
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Detection de la velocite du joueur
            Vector2 playerVelocity = collision.GetComponent<Rigidbody2D>().linearVelocity;

            if (isOrange == 1 && playerVelocity.magnitude != 0)
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            }

            if (isOrange == 0 && playerVelocity.magnitude == 0)
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }
    }

    public void Destroy()
    // Methode appelee par un animation event
    {
        Destroy(this.gameObject);
    }
}
