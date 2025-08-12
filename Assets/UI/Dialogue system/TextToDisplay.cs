using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class TextToDisplay : MonoBehaviour
{
    MenuNavigation playerMenu;
    TextMeshProUGUI UIText;

    TypeWriterEffect typeWriter;

    bool hasCooldown;

    private void Awake()
    {
        playerMenu = GameObject.Find("Player").GetComponent<MenuNavigation>();
        UIText = this.gameObject.GetComponent<TextMeshProUGUI>();

        typeWriter = GetComponent<TypeWriterEffect>();
    }

    [System.Serializable]
    public class TextBlock
    {
        public string name;
        public bool isRandom;
        public List<string> texts = new List<string>();

        [HideInInspector] public int index = -1; // L'index du texte a afficher dans la liste
    }

    [SerializeField] List<TextBlock> textBlocksList = new List<TextBlock>();

    // J'utilise un dictionnaire ET une liste parce qu'on peut pas afficher de dictionnaire dans l'inspecteur
    Dictionary<string, TextBlock> textBlocksDict = new Dictionary<string, TextBlock>();

    private void Start()
    {
        // Ajoute chaque entree de 'textBlocksList' au dictionnaire, avec comme cle le nom du bloc
        foreach (TextBlock textBlock in textBlocksList)
        {
            textBlocksDict.Add(textBlock.name, textBlock);
        }
    }

    public void DisplayText(string blockName, bool _hasCooldown=true)
    // Methode pour afficher le prochain texte de la boite de texte qui a comme nom 'blockName'
    {
        if (!textBlocksDict.ContainsKey(blockName))
        {
            Debug.LogWarning("Textblock with name '" + blockName + "' not found!");
            return;
        }

        // Active le TMProUGUI
        Enable();

        // Stock la boite de texte
        TextBlock current = textBlocksDict[blockName];

        hasCooldown = _hasCooldown;

        if (current.index < current.texts.Count - 1)
        {
            current.index += 1;
        }

        if (current.isRandom == true)
        {
            current.index = Random.Range(0, current.texts.Count);
        }

        typeWriter.SetText(current.texts[current.index]);
    }

    public void Enable()
    // Pour ne pas devoir faire une reference a ce script et au TMProUGUI juste pour l'activer/le desactiver
    {
        UIText.enabled = true;
    }

    public void Disable()
    // Idem
    {
        typeWriter.Skip();
        UIText.enabled = false;
    }

    public bool isEnabled()
    {
        return UIText.enabled;
    }

    public void DisableTextAfterCooldown()
    // Fonction dans l'Event 'OnCompleteTextRevealed' de TypeWriterEffect.cs
    // On peut pas ajouter de Coroutines donc j'ai fait une fonction qui la lance
    {
        if (!hasCooldown)
        {
            return;
        }

        StartCoroutine(DisableTextAfterCooldown(2f));
    }

    IEnumerator DisableTextAfterCooldown(float cooldown)
    {   
        yield return new WaitForSeconds(cooldown);

        UIText.enabled = false;
        playerMenu.ChangeToGame();
    }
}
