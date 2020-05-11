using Locallies.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Allows Player to choose a language
 * Put this script into a Game Object to use it
*/

namespace Locallies.Tools {
    public class LanguageManager : MonoBehaviour {
        //language options available
        [Header("Languages")]
        [SerializeField] List<LanguageItem> languages;
        int languageIndex;

        //language flag display
        [Header("UI Elements")]
        [SerializeField] Image languageFlagUI;

        //initial setup
        private void Awake() {
            ChangeLanguage(0);
        }

        //changes current language
        private void ChangeLanguage(int value) {
            //updates language index
            languageIndex += value;

            if (languageIndex > languages.Count - 1) {
                languageIndex = 0;
            }
            else if (languageIndex < 0) {
                languageIndex = languages.Count - 1;
            }

            //selects new language based on index and loads
            LanguageItem languageItem = languages[languageIndex];
            languageFlagUI.sprite = languageItem.languageFlagSprite;
            LocalizationManager.LoadLocalizationFile(languageItem.fileName);
        }

        //navigation methods
        public void PreviousLanguage() {
            ChangeLanguage(-1);
        }
        public void NextLanguage() {
            ChangeLanguage(1);
        }
    }
}