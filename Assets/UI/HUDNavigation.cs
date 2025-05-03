using UnityEngine;
using UnityEngine.UI;

public class HUDNavigation : MonoBehaviour
    // Toutes les fonctions pour les boutons FIGHT, ACT, ITEM & MERCY
{
    Button currentButton;
    CanvasGroup HUDGroup;
    
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
        currentButton = transform.GetChild(0).GetComponent<Button>();
        HUDGroup = GetComponent<CanvasGroup>();

        fightButton = transform.GetChild(0).GetComponent<Button>();
        actButton = transform.GetChild(1).GetComponent<Button>();
        itemButton = transform.GetChild(2).GetComponent<Button>();
        mercyButton = transform.GetChild(3).GetComponent<Button>();
    }

    public void Backwards()
    {
        // Desactive tous les booleens (il y a pas une meilleure maniere de faire ca ?)
        if(isInFight) fightMenu.SetActive(false);
        if(isInAct) actMenu.SetActive(false);
        if(isInItems) itemMenu.SetActive(false);
        if(isInMercy) mercyMenu.SetActive(false);

        HUDGroup.interactable = true;
        currentButton.Select();
    }

    public void FIGHT()
    {
        fightMenu.SetActive(true);
        HUDGroup.interactable = false;

        // Je sais que j'utilise GetComponent de facon recurrente, mais dans ce cas-ci je pense pas que ca cree des soucis de performance
        Button firstButton = fightMenu.transform.GetChild(0).GetComponent<Button>();
        firstButton.Select();

        // Stocke le dernier bouton selectionne pour pas retourner systematiquement sur FIGHT quand tu quittes le menu
        currentButton = fightButton;

        isInFight = true;
    }
    public void ACT()
    {
        actMenu.SetActive(true);
        HUDGroup.interactable = false;

        Button firstButton = actMenu.transform.GetChild(0).GetComponent<Button>();
        firstButton.Select();

        currentButton = actButton;

        isInAct = true;
    }
    public void ITEMS()
    {
        itemMenu.SetActive(true);
        HUDGroup.interactable = false;

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
        mercyMenu.SetActive(true);
        HUDGroup.interactable = false;

        Button firstButton = mercyMenu.transform.GetChild(0).GetComponent<Button>();
        firstButton.Select();

        currentButton = mercyButton;

        isInMercy = true;
    }
}