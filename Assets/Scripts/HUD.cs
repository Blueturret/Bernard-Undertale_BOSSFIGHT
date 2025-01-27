using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
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
        currentButton = transform.GetChild(0).GetComponent<Button>();
    }

    public void Backwards()
    {
        if(isInFight) fightMenu.SetActive(false);
        if(isInAct) actMenu.SetActive(false);
        if(isInItems) itemMenu.SetActive(false);
        if(isInMercy) mercyMenu.SetActive(false);

        currentButton.Select();
    }

    public void FIGHT()
    {
        fightMenu.SetActive(true);

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
