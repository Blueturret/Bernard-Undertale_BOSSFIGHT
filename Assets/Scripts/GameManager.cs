using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
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
    AudioManager audioManager;

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
        audioManager = AudioManager.instance;
    }

    void Start()
    {
        // Enleve le curseur
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PlayUISound(string name)
    // Je peux pas utiliser l'AudioManager directement dans l'inspecteur pour assigner les sons
    // vu qu'il est conserve entre les scenes, donc les boutons le trouvent pas.
    // Il faut forcement le referencer via script
    {
        AudioManager.instance.PlaySound(name);
    }

    public IEnumerator LoadScene(int sceneIndex)
    {
        transitionAnimator.SetTrigger("Transition");
        audioManager.PlaySound("Transition", true);

        yield return new WaitForSeconds(0.65f);

        SceneManager.LoadScene(sceneIndex);
    }
}
