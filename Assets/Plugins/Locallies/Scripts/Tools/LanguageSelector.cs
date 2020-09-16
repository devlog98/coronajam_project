using FMODUnity;
using Locallies.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Allows Player to choose a language
 * Put this script into a Game Object to use it
*/

namespace Locallies.Tools {
    public class LanguageSelector : MonoBehaviour {
        public static LanguageSelector instance;

        //language options available
        [Header("Languages")]
        [SerializeField] List<Language> languages;
        int languageIndex;

        //language flag display
        [Header("UI Elements")]
        [SerializeField] Image languageFlagUI;

        [Header("UI Buttons")]
        [SerializeField] Image languageLoadUI;

        private bool isLoading;
        public bool IsLoading { get { return isLoading; } }

        //setting singleton instance
        private void Awake() {
            if (instance != null && instance != this) {
                Destroy(this.gameObject);
            }
            else {
                instance = this;
            }

            languageIndex = PlayerPrefs.GetInt("locallies_default_language", 0);
            ChangeLanguage(languages[languageIndex]);
        }

        private void ChangeLanguage(Language language) {
            LocalizationManager.LoadLanguageItem(language);
        }

        //changes current language
        private IEnumerator ChangeLanguageCoroutine(int value) {
            isLoading = true;

            //updates language index
            languageIndex += value;

            if (languageIndex > languages.Count - 1) {
                languageIndex = 0;
            }
            else if (languageIndex < 0) {
                languageIndex = languages.Count - 1;
            }

            PlayerPrefs.SetInt("locallies_default_language", languageIndex);

            // shows loading UI
            languageFlagUI.rectTransform.localScale = Vector3.zero;
            languageLoadUI.gameObject.SetActive(true);

            yield return new WaitForSeconds(1);

            //selects new language based on index and loads
            ChangeLanguage(languages[languageIndex]);

            // hides loading UI
            languageFlagUI.rectTransform.localScale = Vector3.one;
            languageLoadUI.gameObject.SetActive(false);

            isLoading = false;
        }

        //navigation methods
        public void PreviousLanguage() {
            if (!isLoading) {
                StartCoroutine("ChangeLanguageCoroutine", -1);
            }
        }

        public void NextLanguage() {
            if (!isLoading) {
                StartCoroutine("ChangeLanguageCoroutine", 1);
            }
        }
    }
}