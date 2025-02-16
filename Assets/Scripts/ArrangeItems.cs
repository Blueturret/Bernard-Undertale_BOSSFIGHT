using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ArrangeItems : MonoBehaviour
// Arrange les items dans l'inventaire pendant le runtime, pour eviter d'avoir des trous quand tu te soignes
{
    MenuNavigation playerMenu;
    PlayerHealth playerHealth;
    GameObject noFoodText;
    HUD hud;

    List<GameObject> itemList = new List<GameObject>();
    List<Vector2> slotsList = new List<Vector2>();

    private void Awake()
    {
        playerMenu = GameObject.Find("Player").GetComponent<MenuNavigation>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();

        hud = GameObject.Find("HUD").GetComponent<HUD>();

        noFoodText = transform.GetChild(transform.childCount - 1).gameObject;
    }

    void Start()
    {
        noFoodText.SetActive(false);
        
        for(int i=0; i < transform.childCount - 1; i++)
        {
            itemList.Add(transform.GetChild(i).gameObject);
            slotsList.Add(transform.GetChild(i).position);
        }
    }

    public void ConsumeItem(int healAmount)
    {
        playerHealth.Heal(healAmount);
        
        GameObject button = EventSystem.current.currentSelectedGameObject;

        button.SetActive(false);
        itemList.Remove(button);

        ArrangeInventory();
        if (itemList.Count <= 0) noFoodText.SetActive(true);

        hud.Backwards();
    }

    void ArrangeInventory()
    {
        for(int index=0; index < itemList.Count; index++)
        {
            itemList[index].transform.position = slotsList[index];
        }
    }
}
