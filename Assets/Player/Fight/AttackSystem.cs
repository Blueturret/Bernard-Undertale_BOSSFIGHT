using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackSystem : MonoBehaviour
// Gestion de l'attaque du boss
{
    MenuNavigation playerMenu;

    BossObject boss = new BossObject();
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

        healthbarDisplay = GameObject.Find("BOSS Infos").GetComponent<HandleHealthbarDisplay>();
        damageBarAnim = damageBar.GetComponent<Animator>();
    }

    private void Start()
    {
        bossHealth = boss.maxHealth;
    }

    public void StartAttack()
    // Demarre une attaque
    {
        // Gestion des action maps
        GameManager.playerInput.FindActionMap("UI").Disable();
        GameManager.playerInput.FindActionMap("Player").Enable();

        // Activer la barre de charge et le marker
        damageBar.SetActive(true);
        damageMarker.SetActive(true);
    }

    public void Hit(InputAction.CallbackContext context)
    // Attaque le boss
    {
        if(context.performed && !playerMenu.isInGame)
        {
            int dmg = markerScript.CalculateDamage();
            Attack(dmg);
        }
    }

    public void Attack(int dmg)
    // Gestion des degats infliges au boss
    {
        // Gestion des degats
        bossHealth -= dmg;

        // Gestion de l'interface
        float new_amount = bossHealth / boss.maxHealth;

        healthbarDisplay.DisplayHealthbar(new_amount, dmg);
        StartCoroutine(AnimateDamageBar());
        damageMarker.SetActive(false);

        // Lance la prochaine attaque
        if (bossHealth <= 0)
        {
            StartCoroutine(KillBoss());
        }
        else
        {
            playerMenu.ChangeToGame();
        }
    }

    IEnumerator KillBoss()
    {
        yield return new WaitForSeconds(3);

        StartCoroutine(GameManager.instance.LoadScene(0));
    }

    IEnumerator AnimateDamageBar()
    // Coroutine pour gerer les animations de la barre de charge
    {
        damageBarAnim.SetTrigger("Fade");

        yield return null;

        AnimatorClipInfo current = damageBarAnim.GetCurrentAnimatorClipInfo(0)[0]; // L'animation qui est en train de se jouer

        yield return new WaitForSeconds(current.clip.length - Time.deltaTime);

        damageBar.SetActive(false); // Desactive la barre une fois que l'animation est finie
    }
}
