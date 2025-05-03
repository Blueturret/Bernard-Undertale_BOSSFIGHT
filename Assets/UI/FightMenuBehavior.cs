using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;

public class FightMenuBehavior : MonoBehaviour
{
    InputActionAsset playerInput;
    
    [SerializeField] GameObject damageBar;
    [SerializeField] GameObject damageMarker;

    private void Awake()
    {
        playerInput = EventSystem.current.GetComponent<InputSystemUIInputModule>().actionsAsset;
    }

    public void LaunchAttack()
    {
        playerInput.FindActionMap("UI").Disable();

        damageBar.SetActive(true);
        damageMarker.SetActive(true);
    }
}
