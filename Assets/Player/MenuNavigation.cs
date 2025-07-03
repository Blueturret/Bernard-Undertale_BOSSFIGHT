using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
// Gestion de la navigation entre jeu et menus entre chaque attaque
{
    HUDNavigation hud;

    GameObject playerSprite;
    Collider2D playerCollision;
    Transform defaultPlayerPosition;
    public bool isInGame {get; private set;}

    [Header("Menu Navigation")]
    [SerializeField] Button fightButton;
    [SerializeField] TextToDisplay UIText;

    public event Action OnChangeToGame;

    private void Awake()
    {
        hud = GameObject.Find("HUD").GetComponent<HUDNavigation>();

        playerSprite = transform.GetChild(0).gameObject;
        playerCollision = GetComponent<BoxCollider2D>();
        defaultPlayerPosition = GameObject.Find("Player Default Position").transform;
    }

    private void Start()
    {
        isInGame = true;
        StartCoroutine(LateStart());

        // Ajoute la fonction pour lancer la prochaine attaque dans l'event OnChangeToGame
        OnChangeToGame += GameObject.Find("BOSS").GetComponent<AttackManager>().LaunchNextAttack;
    }

    public void ChangeToMenu()
    // Fonction pour passer du gameplay au menu
    {
        if(isInGame)
        {
            // Gestion des action maps
            GameManager.playerInput.FindActionMap("UI").Enable();
            GameManager.playerInput.FindActionMap("Player").Disable();

            // Cache le joueur et retire ses collisions
            playerSprite.SetActive(false);
            playerCollision.enabled = false;

            // Affiche un texte global
            UIText.DisplayText("Global");

            fightButton.Select(); // Selectionne le bouton FIGHT par defaut

            isInGame = false;
        }
    }

    public void ChangeToGame()
    // Fonction pour passer du menu au gameplay (faut lire les gars...)
    {   
        if (!isInGame)
        {
            hud.Backwards(); // Retourne sur le menu principal pour eviter des bugs d'affichage

            // Gestion des action maps
            GameManager.playerInput.FindActionMap("UI").Disable();
            GameManager.playerInput.FindActionMap("Player").Enable();

            // 'Reactive' le joueur
            playerCollision.enabled = true;
            playerSprite.SetActive(true);
            transform.position = defaultPlayerPosition.position;

            // Desactive le texte global
            UIText.Disable();

            EventSystem.current.SetSelectedGameObject(null); // Deselectionne tous les boutons

            isInGame = true;

            OnChangeToGame.Invoke(); // Lance l'attaque suivante quand on finit notre tour
        }
    }

    IEnumerator LateStart()
    // Si on desactive des ActionMap dans Start ou Awake, ca fonctionne pas, il faut un delai (merci le brozers  Unity)
    {
        yield return new WaitForSeconds(0.01f);

        ChangeToMenu();
    }
}
