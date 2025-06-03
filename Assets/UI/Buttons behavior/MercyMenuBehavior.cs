using UnityEngine;

public class MercyMenuBehavior : MonoBehaviour
// Logique du menu MERCY
{
    MenuNavigation playerMenu;

    bool canSpare = false; // Cette variable passe à 'True' quand on flirt avec embrasse Bernard apres lui avoir parle

    private void Awake()
    {
        playerMenu = GameObject.Find("Player").GetComponent<MenuNavigation>();
    }

    public void MERCY()
    // Fonction pour epargner, ou non, Bernard
    {
        if (canSpare)
        {
            print("Bernard est amoureux hihi");
        }
        else
        {
            // Lance l'attaque suivante
            playerMenu.ChangeToGame();
        }
    }
}
