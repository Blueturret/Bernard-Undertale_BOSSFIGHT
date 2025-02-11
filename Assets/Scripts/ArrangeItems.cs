using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ArrangeItems : MonoBehaviour
// Arrange les items dans l'inventaire pendant le runtime, pour eviter d'avoir des trous quand tu te soignes
{
    MenuNavigation playerMenu;
    
    List<GameObject> itemList = new List<GameObject>();
    List<Transform> slotsList = new List<Transform>();

    private void Awake()
    {
        playerMenu = GameObject.Find("Player").GetComponent<MenuNavigation>();
    }

    void Start()
    {
        for(int i=0; i < transform.childCount; i++)
        {
            itemList.Add(transform.GetChild(i).gameObject);
            slotsList.Add(transform.GetChild(i));
        }
    }

    public void ConsumeItem()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;

        button.SetActive(false);
        itemList.Remove(button);

        playerMenu.ChangeToMenu();
        ArrangeInventory();

    }

    void ArrangeInventory()
    {
        for(int index=0; index < itemList.Count; index++)
        {
            itemList[index].transform.position = slotsList[index].position;
            Debug.Log(itemList[index].name + " at position : " + slotsList[index].position);
        }
    }
}
