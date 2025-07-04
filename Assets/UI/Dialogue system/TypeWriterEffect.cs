using UnityEngine;
using TMPro;
using System.Collections;
using System;

[RequireComponent(typeof(TMP_Text))]
public class TypeWriterEffect : MonoBehaviour
{
    // Le tuto pour faire ce script :
    // https://youtu.be/UR_Rh0c4gbY?si=wd-EWb3wisHyOP14

    // Fonction de base
    TMP_Text _textBox;
    int _currentVisibleCharacterIndex;

    Coroutine _typeWriterCoroutine;
    WaitForSeconds _simpleDelay;
    WaitForSeconds _interpunctuationDelay;

    // Options
    [Header("Typewriter Settings")]
    [SerializeField] float characterPerSecond = 20;
    [SerializeField] float interpunctuationDelay = 0.5f;

    // Events
    WaitForSeconds _textBoxFullEventDelay;
    [SerializeField][Range(0.1f, 0.5f)] float sendDoneDelay = 0.25f;

    public static event Action OnCompleteTextRevealed;
    public static event Action OnCharacterRevealed;

    // UIText
    TextToDisplay UIText;

    private void Awake()
    {
        _textBox = GetComponent<TMP_Text>();

        _simpleDelay = new WaitForSeconds(1 / characterPerSecond);
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);

        _textBoxFullEventDelay = new WaitForSeconds(sendDoneDelay);

        UIText = GetComponent<TextToDisplay>();
    }

    private void Start()
    {
        OnCompleteTextRevealed += UIText.DisableTextAfterCooldown;
    }

    public void SetText(string text)
    {
        if (_typeWriterCoroutine != null)
        {
            StopCoroutine(_typeWriterCoroutine);
        }
        
        _textBox.text = text;
        _textBox.maxVisibleCharacters = 0;
        _currentVisibleCharacterIndex = 0;

        _typeWriterCoroutine = StartCoroutine(TypeWriter());
    }

    void OnCancel()
    // Fonction pour skip le texte
    {
        if (_textBox.maxVisibleCharacters != _textBox.textInfo.characterCount - 1)
        {
            Skip();
        }
    }

    void Skip()
    {
        StopCoroutine(_typeWriterCoroutine);
        _textBox.maxVisibleCharacters = _textBox.textInfo.characterCount;
        OnCompleteTextRevealed?.Invoke();
    }

    IEnumerator TypeWriter()
    {
        TMP_TextInfo textInfo = _textBox.textInfo;

        while (_currentVisibleCharacterIndex < textInfo.characterCount + 1)
        {
            int lastCharacterIndex = textInfo.characterCount - 1;
            
            if (_currentVisibleCharacterIndex == lastCharacterIndex)
            {
                _textBox.maxVisibleCharacters++;

                yield return _textBoxFullEventDelay;

                OnCompleteTextRevealed?.Invoke();
                yield break;
            }
            
            char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;

            _textBox.maxVisibleCharacters++;

            if (character == '.')
            {
                yield return _interpunctuationDelay;
            }
            else
            {
                yield return _simpleDelay;
            }

            OnCharacterRevealed?.Invoke();
            _currentVisibleCharacterIndex++;
        }
    }
}
