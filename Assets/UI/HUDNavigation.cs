using UnityEngine;
using UnityEngine.UI;

public class HUDNavigation : MonoBehaviour
    // Toutes les fonctions pour les boutons FIGHT, ACT, ITEM & MERCY
{
    [SerializeField] TextToDisplay UIText;

    Button currentButton; // Variable qui contient le bouton qui correspond au menu dans lequel on est
    CanvasGroup HUDGroup; // Groupe contenant les 4 boutons en bas de l'ecran

    [Header("Menus")]
    [SerializeField] GameObject fightMenu;
    Button fightButton;
    bool isInFight;
    [SerializeField] GameObject actMenu;
    Button actButton;
    bool isInAct;
    [SerializeField] GameObject itemMenu;
    Button itemButton;
    bool isInItems;
    [SerializeField] GameObject mercyMenu;
    Button mercyButton;
    bool isInMercy;

    public void Awake()
    {
        // Stockage des boutons
        fightButton = transform.GetChild(0).GetComponent<Button>();
        actButton = transform.GetChild(1).GetComponent<Button>();
        itemButton = transform.GetChild(2).GetComponent<Button>();
        mercyButton = transform.GetChild(3).GetComponent<Button>();

        currentButton = fightButton; // Bouton d'attaque selectionne par defaut
        HUDGroup = GetComponent<CanvasGroup>();
    }

    public void Backwards()
    // Quitte le menu dans lequel on se trouve et retourne sur le 'menu principal' avec les boutons FIGHT, ACT,...
    {
        // Desactive tous les booleens (il y a pas une meilleure maniere de faire ca ?)
        if(isInFight) fightMenu.SetActive(false);
        if(isInAct) actMenu.SetActive(false);
        if(isInItems) itemMenu.SetActive(false);
        if(isInMercy) mercyMenu.SetActive(false);

        HUDGroup.interactable = true;
        currentButton.Select(); // Retourne sur le dernier bouton qu'on a selectionne
    }

    public void FIGHT()
    {
        // Rentre dans le menu et desactive les 4 boutons
        fightMenu.SetActive(true);
        HUDGroup.interactable = false;
        UIText.Disable();

        // Je sais que j'utilise GetComponent de facon recurrente, mais dans ce cas-ci je pense pas que ca cree des soucis de performance
        Button firstButton = fightMenu.transform.GetChild(0).GetComponent<Button>();
        firstButton.Select();

        // Stocke le dernier bouton selectionne pour pas retourner systematiquement sur FIGHT quand tu quittes le menu
        currentButton = fightButton;

        isInFight = true;
    }
    public void ACT()
    {
        // Rentre dans le menu et desactive les 4 boutons
        actMenu.SetActive(true);
        HUDGroup.interactable = false;
        UIText.Disable();

        // Selectionne le premier bouton
        Button firstButton = actMenu.transform.GetChild(0).GetComponent<Button>();
        firstButton.Select();

        currentButton = actButton;

        isInAct = true;
    }
    public void ITEMS()
    {
        // Rentre dans le menu et desactive les 4 boutons
        itemMenu.SetActive(true);
        HUDGroup.interactable = false;
        UIText.Disable();

        // Selectionne le premier bouton.
        // Vu que les items vont etre desactives une fois consumes, il faut trouver le nouveau premier item a selectionner
        Button firstButton = null; 
        for(int i = 0; i < itemMenu.transform.childCount; i++)
        {
            if (itemMenu.transform.GetChild(i).gameObject.activeInHierarchy == true)
            {
                firstButton = itemMenu.transform.GetChild(i).GetComponent<Button>();
                break;
            }
        }

        if(firstButton != null) firstButton.Select();

        currentButton = itemButton;

        isInItems = true;
    }
    public void MERCY()
    {
        // Rentre dans le menu et desactive les 4 boutons
        mercyMenu.SetActive(true);
        HUDGroup.interactable = false;
        UIText.Disable();

        // Selectionne le premier bouton
        Button firstButton = mercyMenu.transform.GetChild(0).GetComponent<Button>();
        firstButton.Select();

        currentButton = mercyButton;

        isInMercy = true;
    }
}