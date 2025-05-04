using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class GameManager : MonoBehaviour
{
    public static InputActionAsset playerInput { get; private set; }

    private void Awake()
    {
        playerInput = EventSystem.current.GetComponent<InputSystemUIInputModule>().actionsAsset;
    }

    void Start()
    {
        // Enleve le curseur
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
}
