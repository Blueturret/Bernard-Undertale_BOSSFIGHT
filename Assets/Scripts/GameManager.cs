using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    // Stocke les inputs du joueur globalement pour pouvoir desactiver/activer des Action Map plus facilement
    public static InputActionAsset playerInput { get; private set; }

    Animator transitionAnimator;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        transitionAnimator = GameObject.Find("Transition Canvas").GetComponent<Animator>();
        playerInput = EventSystem.current.GetComponent<InputSystemUIInputModule>().actionsAsset;
    }

    void Start()
    {
        // Enleve le curseur
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public IEnumerator LoadScene(int sceneIndex)
    {
        transitionAnimator.SetTrigger("Transition");

        yield return new WaitForSeconds(0.65f);

        SceneManager.LoadScene(sceneIndex);
    }
}
