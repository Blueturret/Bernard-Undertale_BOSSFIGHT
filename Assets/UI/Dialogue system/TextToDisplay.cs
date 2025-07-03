using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class TextToDisplay : MonoBehaviour
{
    [System.Serializable]
    public class TextBlock
    {
        public string name;
        public bool isRandom;
        public List<string> texts = new List<string>();

        [HideInInspector] public int index = -1; // L'index du texte a afficher dans la liste
    }

    #region UIText
    TextMeshProUGUI UIText;
    private void Awake()
    {
        UIText = this.gameObject.GetComponent<TextMeshProUGUI>();
    }
    #endregion

    [SerializeField] List<TextBlock> textBlocksList = new List<TextBlock>();
    Dictionary<string, TextBlock> textBlocksDict = new Dictionary<string, TextBlock>();

    private void Start()
    {
        // Ajoute chaque entree de la liste de bloc de texte au dictionnaire, avec comme cle le nom
        // parce qu'on peut pas afficher de dictionnaire dans l'inspecteur
        foreach (TextBlock textBlock in textBlocksList)
        {
            textBlocksDict.Add(textBlock.name, textBlock);
        }
    }

    public void DisplayText(string blockName)
    // Methode pour afficher le prochain texte de la boite de texte qui a comme nom 'blockName'
    {
        if (!textBlocksDict.ContainsKey(blockName))
        {
            Debug.LogWarning("Textblock with name '" + blockName + "' not found!");
        }

        TextBlock current = textBlocksDict[blockName];

        UIText.enabled = true;

        if (current.index < current.texts.Count - 1)
        {
            current.index += 1;
        }

        if (current.isRandom == true)
        {
            current.index = Random.Range(0, current.texts.Count);
        }

        UIText.text = current.texts[current.index];
    }

    public void Disable()
    // Pour ne pas devoir faire une reference a ce script et au TMProUGUI juste pour le desactiver
    {
        UIText.enabled = false;
    }
}
