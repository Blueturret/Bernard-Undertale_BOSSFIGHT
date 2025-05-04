using UnityEngine;
using UnityEngine.InputSystem;

public class AttackSystem : MonoBehaviour
{
    MenuNavigation playerMenu;

    BossObject boss = new BossObject();
    AttackManager attackManager;
    HandleHealthbarDisplay healthbarDisplay;

    [Header("UI Elements")]
    [SerializeField] GameObject damageBar;
    [SerializeField] GameObject damageMarker;

    Rigidbody2D markerRb;
    Vector2 markerDefaultPos = new Vector2(-5.7f, -1.608f);

    [Header("Properties")]
    [SerializeField] float speed;
    [SerializeField] int minDamage;
    [SerializeField] int maxDamage;
    float bossHealth; // C'est un float pour que la division par maxBossHealth fonctionne

    private void Awake()
    {
        markerRb = damageMarker.GetComponent<Rigidbody2D>();

        playerMenu = GetComponent<MenuNavigation>();

        attackManager = GameObject.Find("BOSS").GetComponent<AttackManager>();
        healthbarDisplay = GameObject.Find("BOSS Infos").GetComponent<HandleHealthbarDisplay>();
    }

    private void Start()
    {
        bossHealth = boss.maxHealth;
    }

    public void StartAttack()
    {
        // Gestion des action maps
        GameManager.playerInput.FindActionMap("UI").Disable();
        GameManager.playerInput.FindActionMap("Player").Enable();

        // Activer et animer la barre de charge et le marker
        damageBar.SetActive(true);
        damageMarker.SetActive(true);
        damageMarker.transform.position = markerDefaultPos;
        markerRb.linearVelocityX = speed;
    }

    public void Hit(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Attack();

            // Lance la prochaine attaque
            playerMenu.ChangeToGame();
            attackManager.LaunchNextAttack();
        }
    }

    void Attack()
    {
        int dmg = Random.Range(minDamage, maxDamage + 1);

        // Gestion des degats
        bossHealth -= dmg;
        if (bossHealth <= 0) Debug.Log("Da boss is ded!!");

        // Gestion de l'interface
        float new_amount = bossHealth / boss.maxHealth;

        healthbarDisplay.DisplayHealthbar(new_amount, dmg);
        damageBar.SetActive(false);
        damageMarker.SetActive(false);
    }
}
