using System;
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
    Collider2D playerCollision;
    Transform defaultPlayerPosition;
    bool isInGame = true;

        [Header("Menu Navigation")]
    [SerializeField] Button fightButton;

    // Attack Manager
    AttackManager attackManager;
    public event Action OnChangeToGame;

    private void Awake()
    {
        playerInput = EventSystem.current.GetComponent<InputSystemUIInputModule>().actionsAsset;

        hud = GameObject.Find("HUD").GetComponent<HUD>();

        playerSprite = this.transform.GetChild(0).gameObject;
        playerCollision = this.GetComponent<BoxCollider2D>();
        defaultPlayerPosition = GameObject.Find("Player Default Position").transform;

        attackManager = GameObject.Find("BOSS").GetComponent<AttackManager>();
    }

    private void Start()
    {
        ChangeToMenu();

        OnChangeToGame += attackManager.LaunchNextAttack;
    }

    public void ChangeToMenu()
    {
        if (isInGame)
        {
            playerInput.FindActionMap("UI").Enable();

            playerSprite.SetActive(false);
            playerCollision.enabled = false;

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
            playerCollision.enabled = true;

            playerSprite.SetActive(true);
            transform.position = defaultPlayerPosition.position;

            EventSystem.current.SetSelectedGameObject(null);

            isInGame = true;

            OnChangeToGame.Invoke();
        }
    }
}
