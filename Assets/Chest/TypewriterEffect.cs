using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [Header("Settings")]
    public float typingSpeed = 0.05f;

    private TextMeshProUGUI textComponent;
    private string originalText;

    void Awake()
    {
        // 1. Grab the text component 
        textComponent = GetComponent<TextMeshProUGUI>();
        if (textComponent != null)
        {
            originalText = textComponent.text;
        }
    }

    void OnEnable()
    {
        // 2. When the UI turns on, clear the text instantly and start typing
        if (textComponent != null && !string.IsNullOrEmpty(originalText))
        {
            textComponent.text = "";
            StartCoroutine(TypeText());
        }
    }

    void OnDisable()
    {
        // 3. When the UI turns off, put the full text back as a failsafe
        if (textComponent != null)
        {
            textComponent.text = originalText;
            StopAllCoroutines();
        }
    }

    IEnumerator TypeText()
    {
        // 4. Type it out letter by letter using Substring
        for (int i = 0; i <= originalText.Length; i++)
        {
            textComponent.text = originalText.Substring(0, i);

            // Ignore the paused game time and use real-world clock time!
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }
}