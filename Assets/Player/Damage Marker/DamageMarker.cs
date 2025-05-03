using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class DamageMarker : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 defaultPos = new Vector2(-5.7f, -1.608f);

    BossObject boss = new BossObject();
    AttackManager attackManager;
    HandleHealthbarDisplay healthbarDisplay;
    MenuNavigation playerMenu;

    [Header("UI Elements")]
    [SerializeField] GameObject damageBar;

    [Header("Properties")]
    [SerializeField] float speed;
    [SerializeField] int minDamage;
    [SerializeField] int maxDamage;
    float bossHealth; // C'est un float pour que la division par maxBossHealth fonctionne

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        attackManager = GameObject.Find("BOSS").GetComponent<AttackManager>();
        playerMenu = GameObject.Find("Player").GetComponent<MenuNavigation>();
        healthbarDisplay = GameObject.Find("BOSS Infos").GetComponent<HandleHealthbarDisplay>();
    }

    private void Start()
    {
        bossHealth = boss.maxHealth;
    }

    private void OnEnable()
    {
        transform.position = defaultPos;
        //rb.linearVelocityX = speed;
    }

    public void Hit(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Attack();

            // Lance la prochaine attaque
            attackManager.LaunchNextAttack();
            playerMenu.ChangeToGame();
        }
    }

    public void Attack()
    {
        int dmg = Random.Range(minDamage, maxDamage + 1);

        // Gestion des degats
        bossHealth -= dmg;
        if (bossHealth <= 0) Debug.Log("Da boss is ded!!");

        // Gestion de l'interface
        float new_amount = bossHealth / boss.maxHealth;

        healthbarDisplay.DisplayHealthbar(new_amount, dmg);
        damageBar.SetActive(false);
        gameObject.SetActive(false);
    }
}
