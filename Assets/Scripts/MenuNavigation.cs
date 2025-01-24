using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    GameObject playerSprite;
    Transform defaultPlayerPosition;
    bool isInGame = true;

        [Header("Menu Navigation")]
    [SerializeField] Button fightButton;

    private void Start()
    {
        playerSprite = this.transform.GetChild(0).gameObject;

        defaultPlayerPosition = GameObject.Find("Player Default Position").transform;
    }

    public void ChangeToMenu()
    {
        if (isInGame)
        {
            playerSprite.SetActive(false);

            fightButton.Select();

            isInGame = false;
        }
    }

    public void ChangeToGame()
    {
        if (!isInGame)
        {
            playerSprite.SetActive(true);
            transform.position = defaultPlayerPosition.position;

            EventSystem.current.SetSelectedGameObject(null);

            isInGame = true;
        }
    }
}
