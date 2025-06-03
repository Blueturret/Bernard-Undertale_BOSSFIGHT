using UnityEngine;

public class FightMenuBehavior : MonoBehaviour
// CE SCRIPT NE FAIT RIEN (pour le moment)
{
    MenuNavigation playerMenu;

    private void Awake()
    {
        playerMenu = GameObject.Find("Player").GetComponent<MenuNavigation>();
    }
}
