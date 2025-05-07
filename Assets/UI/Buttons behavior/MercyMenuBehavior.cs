using UnityEngine;

public class MercyMenuBehavior : MonoBehaviour
{
    MenuNavigation playerMenu;

    private void Awake()
    {
        playerMenu = GameObject.Find("Player").GetComponent<MenuNavigation>();
    }

    public void MERCY()
    {
        // Lance l'attaque suivante
        playerMenu.ChangeToGame();
    }
}
