using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/* 
 * This must be used only for displaying text and other stuff on screen
 * It shouldn't have logic about what to display or for how long
*/

public class UI : MonoBehaviour {
    public static UI instance;

    [SerializeField] TextMeshProUGUI dialogueSentenceText; //element used for displaying dialogue sentences
    [SerializeField] Image dialogueSentenceBox; //element used as box for dialogue
    [SerializeField] float dialogueTypeSpeed = 0.02f; //speed which dialogue is typed on screen
    [SerializeField] float dialogueFadeAmount = 0.1f; //speed which dialogue disappears from screen

    //setting singleton instance
    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    //update dialogue sentence that is being show on screen
    public void UpdateDialogueSentenceText(string sentence) {
        StopCoroutine("TypeText");
        StartCoroutine(TypeText(dialogueSentenceText, sentence, dialogueTypeSpeed));
    }

    public void HideDialogueSentence() {

    }

    //types each letter of text at a certain amount of time and displays on specific UI element
    private IEnumerator TypeText(TextMeshProUGUI textUI, string text, float typeSpeed) {
        textUI.text = "";

        foreach (char letter in text.ToCharArray()) {
            textUI.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }

        StartCoroutine(FadeText(dialogueSentenceText, dialogueSentenceBox, dialogueFadeAmount));
    }

    //fades UI elements based on specific fade amount
    private IEnumerator FadeText(TextMeshProUGUI textUI, Image textBoxUI, float fadeAmount) {
        float alpha = 1f;

        while (textUI.color.a > 0) {
            textBoxUI.color = new Color(textBoxUI.color.r, textBoxUI.color.g, textBoxUI.color.b, alpha);
            textUI.color = new Color(textUI.color.r, textUI.color.g, textUI.color.b, alpha);
            alpha -= fadeAmount;
            yield return null;
        }
    }
}