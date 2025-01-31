using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
    // Toutes les fonctions pour les boutons FIGHT, ACT, ITEM & MERCY
{
    Button currentButton;
    
    [SerializeField] GameObject fightMenu;
    bool isInFight;
    [SerializeField] GameObject actMenu;
    bool isInAct;
    [SerializeField] GameObject itemMenu;
    bool isInItems;
    [SerializeField] GameObject mercyMenu;
    bool isInMercy;

    public void Start()
    {
        // Stocke le dernier bouton selectionne pour pas retourner systematiquement sur FIGHT quand tu quittes le menu
        currentButton = transform.GetChild(0).GetComponent<Button>();
    }

    public void Backwards()
    {
        // Desactive tous les booleens (il y a pas une meilleure maniere de faire ca ?)
        if(isInFight) fightMenu.SetActive(false);
        if(isInAct) actMenu.SetActive(false);
        if(isInItems) itemMenu.SetActive(false);
        if(isInMercy) mercyMenu.SetActive(false);

        currentButton.Select();
    }

    public void FIGHT()
    {
        fightMenu.SetActive(true);

        // Je sais que j'utilise GetComponent de facon recurrente, mais dans ce cas-ci je pense pas que ca cree des soucis de performance
        Button firstButton = fightMenu.transform.GetChild(0).GetComponent<Button>();
        firstButton.Select();

        currentButton = transform.GetChild(0).GetComponent<Button>();

        isInFight = true;
    }
    public void ACT()
    {
        actMenu.SetActive(true);

        Button firstButton = actMenu.transform.GetChild(0).GetComponent<Button>();
        firstButton.Select();

        currentButton = transform.GetChild(1).GetComponent<Button>();

        isInAct = true;
    }
    public void ITEMS()
    {
        itemMenu.SetActive(true);

        Button firstButton = itemMenu.transform.GetChild(0).GetComponent<Button>();
        firstButton.Select();

        currentButton = transform.GetChild(2).GetComponent<Button>();

        isInItems = true;
    }
    public void MERCY()
    {
        mercyMenu.SetActive(true);

        Button firstButton = mercyMenu.transform.GetChild(0).GetComponent<Button>();
        firstButton.Select();

        currentButton = transform.GetChild(3).GetComponent<Button>();

        isInMercy = true;
    }
}