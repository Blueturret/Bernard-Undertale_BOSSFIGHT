using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TextToDisplay : MonoBehaviour
{
    TextMeshProUGUI text;
    
    public bool isDisplayRandom;
    public List<string> textsToDisplay = new List<string>();
    int textIndex;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        textIndex = -1;
    }

    public void ChangeText()
    {
        if (textIndex < textsToDisplay.Count - 1)
        {
            text.text = "* " + textsToDisplay[textIndex + 1];
            textIndex++;
        }
    }
}
