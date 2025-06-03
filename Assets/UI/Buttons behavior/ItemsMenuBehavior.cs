using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ItemsMenuBehavior : MonoBehaviour
// Logique de l'inventaire
{
    MenuNavigation playerMenu;
    PlayerHealth playerHealth;
    GameObject noFoodText;

    List<GameObject> itemList = new List<GameObject>();
    List<Vector2> slotsList = new List<Vector2>();

    private void Awake()
    {
        playerMenu = GameObject.Find("Player").GetComponent<MenuNavigation>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();

        noFoodText = transform.GetChild(transform.childCount - 1).gameObject;
    }

    void Start()
    {
        noFoodText.SetActive(false);
        
        // Associe a chaque item une position pour la fonction ArrangeInventory()
        for(int i=0; i < transform.childCount - 1; i++)
        {
            itemList.Add(transform.GetChild(i).gameObject);
            slotsList.Add(transform.GetChild(i).position);
        }
    }

    public void ConsumeItem(int healAmount)
    // Fonction pour consommer un item et soigner le joueur
    {
        // Affiche un message si on a plus d'items
        if (itemList.Count <= 0)
        {
            noFoodText.SetActive(true);
            return;
        }

        playerHealth.Heal(healAmount);
        
        // Desactive l'item qu'on vient de selectionner
        GameObject button = EventSystem.current.currentSelectedGameObject;
        button.SetActive(false);

        itemList.Remove(button);

        ArrangeInventory();

        // Lance l'attaque suivante
        playerMenu.ChangeToGame();
    }

    void ArrangeInventory()
    // Arrange les items dans l'inventaire pendant le runtime, pour eviter d'avoir des trous
    // quand le joueur se soigne
    {
        // Rearrange les items restants
        for (int index=0; index < itemList.Count; index++)
        {
            itemList[index].transform.position = slotsList[index];
        }
    }
}
