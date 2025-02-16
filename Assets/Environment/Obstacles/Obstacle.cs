using System.Collections.Generic;
using UnityEngine;
public class Obstacle : MonoBehaviour, IPooledObject
    // Classe mere pour tous les obstacles
{
    List<GameObject> attackObjects;
    protected Rigidbody2D rb;

    [Header("Properties")]
    public int damage;
    public float speed;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        attackObjects = AttackManager.attackObjects;
    }

    // Methode appelee quand l'objet sort de sa pool
    public virtual void OnObjectSpawned() { }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    // Gestion des degats
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            gameObject.SetActive(false);

            attackObjects.Remove(gameObject);
        }
    }
}
