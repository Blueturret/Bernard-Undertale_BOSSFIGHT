using UnityEngine;

public class FightMenuBehavior : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] int minDamage;
    [SerializeField] int maxDamage;
    float bossHealth; // C'est un float pour que la division par maxBossHealth fonctionne

    BossObject boss = new BossObject();
    HandleHealthbarDisplay healthbarDisplay;

    private void Start()
    {
        healthbarDisplay = GameObject.Find("BOSS Infos").GetComponent<HandleHealthbarDisplay>();
        
        bossHealth = boss.maxHealth;
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
    }
}
