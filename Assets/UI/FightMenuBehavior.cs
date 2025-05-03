using UnityEngine;

public class FightMenuBehavior : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] int maxBossHealth; // La gestion de la vie du boss se fait ici parce que le boss n'a pas de script a lui
    [SerializeField] int minDamage;
    [SerializeField] int maxDamage;
    float bossHealth; // C'est un float pour que la division par maxBossHealth fonctionne

    HandleHealthbarDisplay healthbarDisplay;

    private void Start()
    {
        healthbarDisplay = GameObject.Find("BOSS Infos").GetComponent<HandleHealthbarDisplay>();
        
        bossHealth = maxBossHealth;
    }

    public void Attack()
    {
        int dmg = Random.Range(minDamage, maxDamage + 1);

        // Gestion des degats
        bossHealth -= dmg;
        if (bossHealth <= 0) Debug.Log("Da boss is ded!!");

        // Gestion de l'interface
        float new_amount = bossHealth / maxBossHealth;

        healthbarDisplay.DisplayHealthbar(new_amount, dmg);
    }
}
