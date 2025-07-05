using System.Collections;
using UnityEngine;

public class MercyMenuBehavior : MonoBehaviour
// Logique du menu MERCY
{
    MenuNavigation playerMenu;

    [HideInInspector] public bool canSpare = false;// Passe à 'True' quand on embrasse Bernard apres lui avoir parle

    private void Awake()
    {
        playerMenu = GameObject.Find("Player").GetComponent<MenuNavigation>();
    }

    public void MERCY()
    // Fonction pour epargner, ou non, Bernard
    {
        if (canSpare)
        {
            StartCoroutine(Spare());    
        }
        else
        {
            // Lance l'attaque suivante
            playerMenu.ChangeToGame();
        }
    }

    IEnumerator Spare()
    {
        // Lance l'animation de Bernard qui rougit
        GameObject.Find("Body").GetComponent<Animator>().SetTrigger("mustKawaii");

        yield return new WaitForSeconds(2);

        StartCoroutine(GameManager.instance.LoadScene(0));
    }
}
