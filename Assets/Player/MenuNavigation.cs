using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    InputActionAsset playerInput;
    HUDNavigation hud;
    
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

        hud = GameObject.Find("HUD").GetComponent<HUDNavigation>();

        playerSprite = this.transform.GetChild(0).gameObject;
        playerCollision = this.GetComponent<BoxCollider2D>();
        defaultPlayerPosition = GameObject.Find("Player Default Position").transform;

        attackManager = GameObject.Find("BOSS").GetComponent<AttackManager>();
    }

    private void Start()
    {
        ChangeToMenu();

        OnChangeToGame += attackManager.LaunchNextAttack; // Ajoute la fonction pour lancer la prochaine attaque dans l'event OnChangeToGame
    }

    public void ChangeToMenu()
    // Fonction pour passer du gameplay au menu
    {
        //if (isInGame)
        //{
            playerInput.FindActionMap("UI").Enable();

            playerSprite.SetActive(false);
            playerCollision.enabled = false;

            fightButton.Select(); // Selectionne le bouton FIGHT par defaut

            isInGame = false;
        //}
    }

    public void ChangeToGame()
    // Fonction pour passer du menu au gameplay (faut lire les gars...)
    {   
        if (!isInGame)
        {
            hud.Backwards();
            
            playerInput.FindActionMap("UI").Disable();

            playerCollision.enabled = true;
            playerSprite.SetActive(true);
            transform.position = defaultPlayerPosition.position;

            EventSystem.current.SetSelectedGameObject(null); // Deselectionne tous les boutons

            isInGame = true;

            OnChangeToGame.Invoke(); // Lance l'attaque suivante quand on finit notre tour
        }
    }
}
