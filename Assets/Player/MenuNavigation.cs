using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    InputActionAsset playerInput;
    HUD hud;
    
    GameObject playerSprite;
    Transform defaultPlayerPosition;
    bool isInGame = true;

        [Header("Menu Navigation")]
    [SerializeField] Button fightButton;

    private void Awake()
    {
        playerInput = EventSystem.current.GetComponent<InputSystemUIInputModule>().actionsAsset;

        hud = GameObject.Find("HUD").GetComponent<HUD>();

        playerSprite = this.transform.GetChild(0).gameObject;
        defaultPlayerPosition = GameObject.Find("Player Default Position").transform;
    }

    private void Start()
    {
        ChangeToMenu();
    }

    public void ChangeToMenu()
    {
        if (isInGame)
        {
            playerInput.FindActionMap("UI").Enable();

            playerSprite.SetActive(false);

            fightButton.Select();

            isInGame = false;
        }
    }

    public void ChangeToGame()
    {   
        if (!isInGame)
        {
            hud.Backwards();
            
            playerInput.FindActionMap("UI").Disable();

            playerSprite.SetActive(true);
            transform.position = defaultPlayerPosition.position;

            EventSystem.current.SetSelectedGameObject(null);

            isInGame = true;
        }
    }
}
