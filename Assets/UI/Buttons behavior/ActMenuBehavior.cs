using UnityEngine;

public class ActMenuBehavior : MonoBehaviour
// Logique du menu ACT
{
    [SerializeField] TextToDisplay UIText;

    GameObject flirtButton;

    private void Start()
    {
        flirtButton = transform.GetChild(2).gameObject;

        flirtButton.SetActive(false); // Desactiver le bouton par defaut
    }

    public void Check()
    // Affiche les stats de Bernard et une phrase personnalisee
    {
        UIText.DisplayText("Check");
    }

    public void Talk()
    // Parler une fois a Bernard active le bouton pour flirt
    {
        UIText.DisplayText("Talk");

        flirtButton.SetActive(true);
    }
    public void Flirt()
    // Flirt avec Bernard, ce qui lance la fin pacifiste
    {
        GameObject.Find("Body").GetComponent<Animator>().SetTrigger("mustKawaii"); // Lance l'animation de Bernard qui rougit

        UIText.DisplayText("Flirt", false);

        // Retourne dans le menu
        GameObject.Find("HUD").GetComponent<HUDNavigation>().Backwards();
    }
}
