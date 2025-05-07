using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    Dictionary<string, TextToDisplay> dict = new Dictionary<string, TextToDisplay>();
    
    void Start()
    {
        // Trouver tous les objets qui ont des textes
        foreach(TextToDisplay text in FindObjectsByType<TextToDisplay>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            dict.Add(text.gameObject.name, text);
            text.ChangeText();
            text.ChangeText();
            text.ChangeText();
            text.ChangeText();
        }
    }
}
