using UnityEngine;

public class DamageMarker : MonoBehaviour
{
    Rigidbody2D markerRb;
    Vector2 markerDefaultPos = new Vector2(-5.7f, -1.608f);

    AttackSystem attackSystem;

    [Header("Properties")]
    [SerializeField] float speed;
    [SerializeField] int minDamage;
    [SerializeField] int medDamage;
    [SerializeField] int maxDamage;

    void Awake()
    {
        markerRb = GetComponent<Rigidbody2D>();

        attackSystem = GameObject.Find("Player").GetComponent<AttackSystem>();
    }

    void OnEnable()
    {
        transform.position = markerDefaultPos;
        markerRb.linearVelocityX = speed;
    }

    public int CalculateDamage()
    {
        float dist = Vector2.Distance(Vector2.zero, new Vector2(transform.position.x, 0));

        if (dist <= 0.5f)
        {
            int dmg = Random.Range(maxDamage - 10, maxDamage + 11);
            return dmg;
        }
        else if (0.5f < dist && dist <= 2.8f)
        {
            int dmg = Random.Range(medDamage - 10, medDamage + 11);
            return dmg;
        }
        else
        {
            int dmg = Random.Range(minDamage - 10, minDamage + 11);
            return dmg;
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.x >= 5.8f)
        {
            attackSystem.CancelAttack();
        }
    }
}
