using UnityEngine;

public class MainMenuBehavior : MonoBehaviour
{
    public void StartGame()
    // Lance le combat contre Bernard
    {
        StartCoroutine(GameManager.instance.LoadScene(1));
    }

    public void Quit()
    // Quite l'application
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
