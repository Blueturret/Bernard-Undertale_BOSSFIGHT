using System.Collections;
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
    Animator damageBarAnim;
    [SerializeField] GameObject damageMarker;
    DamageMarker markerScript;

    
    float bossHealth; // C'est un float pour que la division par maxBossHealth fonctionne

    private void Awake()
    {
        playerMenu = GetComponent<MenuNavigation>();

        markerScript = damageMarker.GetComponent<DamageMarker>();

        attackManager = GameObject.Find("BOSS").GetComponent<AttackManager>();
        healthbarDisplay = GameObject.Find("BOSS Infos").GetComponent<HandleHealthbarDisplay>();
        damageBarAnim = damageBar.GetComponent<Animator>();
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
    }

    public void Hit(InputAction.CallbackContext context)
    {
        if(context.performed && !playerMenu.isInGame)
        {
            int dmg = markerScript.CalculateDamage();
            Attack(dmg);
        }
    }

    void Attack(int dmg)
    {
        // Gestion des degats
        bossHealth -= dmg;
        if (bossHealth <= 0) Debug.Log("Da boss is ded!!");

        // Gestion de l'interface
        float new_amount = bossHealth / boss.maxHealth;

        healthbarDisplay.DisplayHealthbar(new_amount, dmg);
        StartCoroutine(AnimateDamageBar());
        damageMarker.SetActive(false);

        // Lance la prochaine attaque
        playerMenu.ChangeToGame();
        attackManager.LaunchNextAttack();
    }

    public void CancelAttack()
    {
        Attack(0);
    }

    IEnumerator AnimateDamageBar()
    {
        damageBarAnim.SetTrigger("Fade");

        yield return null;

        AnimatorClipInfo current = damageBarAnim.GetCurrentAnimatorClipInfo(0)[0];

        yield return new WaitForSeconds(current.clip.length - Time.deltaTime);

        damageBar.SetActive(false);
    }
}
