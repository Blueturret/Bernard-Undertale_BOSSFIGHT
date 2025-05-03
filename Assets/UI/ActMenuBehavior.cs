using UnityEngine;

public class ActMenuBehavior : MonoBehaviour
{
    MenuNavigation playerMenu;
    BossObject boss = new BossObject();

    GameObject flirtButton;

    private void Awake()
    {
        playerMenu = GameObject.Find("Player").GetComponent<MenuNavigation>();
    }

    private void Start()
    {
        flirtButton = transform.GetChild(2).gameObject;

        flirtButton.SetActive(false); // Desactiver par defaut
    }

    public void Check()
    {
        string text = boss.name + " - ATK " + boss.attackStat + " DEF " + boss.defenseStat; 
        print(text);
        // Lance l'attaque suivante
        playerMenu.ChangeToGame();
    }

    public void Talk()
    {
        flirtButton.SetActive(true);

        // Lance l'attaque suivante
        playerMenu.ChangeToGame();
    }
    public void Flirt()
    {
        print(boss.description);
        // Lance l'attaque suivante
        playerMenu.ChangeToGame();
    }
}
