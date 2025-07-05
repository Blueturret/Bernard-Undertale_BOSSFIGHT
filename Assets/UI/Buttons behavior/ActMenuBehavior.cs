using TMPro;
using UnityEngine;

public class ActMenuBehavior : MonoBehaviour
// Logique du menu ACT
{
    [SerializeField] TextToDisplay UIText;

    [Header("Pacifist route")]
    [SerializeField] MercyMenuBehavior mercyBehavior;
    [SerializeField] TextMeshProUGUI spareText;

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
    // Flirt avec Bernard, ce qui permet de l'epargner pour la fin pacifiste
    {
        // Change la couleur de '* Spare' en jaune
        spareText.color = new Color(255, 255, 0);
        mercyBehavior.canSpare = true;

        // Retourne dans le menu
        GameObject.Find("HUD").GetComponent<HUDNavigation>().Backwards();

        UIText.DisplayText("Flirt", false);        
    }
}
