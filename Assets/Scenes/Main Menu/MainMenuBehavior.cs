using UnityEngine;

public class MainMenuBehavior : MonoBehaviour
{
    private void Start()
    {
        // Audio
        AudioManager.instance.PlaySound("Menu");
        AudioManager.instance.StopSound("Theme");
    }

    public void StartGame()
    // Lance le combat contre Bernard
    {
        // Audio
        AudioManager.instance.PlaySound("Theme");
        AudioManager.instance.StopSound("Menu");

        StartCoroutine(GameManager.instance.LoadScene(1));
    }

    public void Quit()
    // Quitte l'application
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
