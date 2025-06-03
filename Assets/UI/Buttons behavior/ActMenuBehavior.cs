using TMPro;
using UnityEngine;

public class ActMenuBehavior : MonoBehaviour
// Logique du menu ACT
{
    MenuNavigation playerMenu;
    HUDNavigation hud;

    BossObject bossInstance = new BossObject();

    GameObject flirtButton;

    private void Awake()
    {
        playerMenu = GameObject.Find("Player").GetComponent<MenuNavigation>();
        hud = GameObject.Find("HUD").GetComponent<HUDNavigation>();
    }

    private void Start()
    {
        flirtButton = transform.GetChild(2).gameObject;

        flirtButton.SetActive(false); // Desactiver le bouton par defaut
    }

    public void Check()
    // Affiche les stats de Bernard et une phrase personnalisee
    {
        print(bossInstance.description); // Temporaire en attendant le systeme de dialogues
        // Lance l'attaque suivante
        playerMenu.ChangeToGame();
    }

    public void Talk()
    // Parler une fois a Bernard active le bouton pour flirt
    {
        flirtButton.SetActive(true);

        // Lance l'attaque suivante
        playerMenu.ChangeToGame();
    }
    public void Flirt()
    // Flirt avec Bernard, ce qui lance la fin pacifiste
    {
        GameObject.Find("Body").GetComponent<Animator>().SetTrigger("mustKawaii"); // Lance l'animation de Bernard qui rougit

        // Retourne dans le menu
        hud.Backwards();
    }
}
