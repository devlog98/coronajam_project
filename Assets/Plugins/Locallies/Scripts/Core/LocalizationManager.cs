using Locallies.Core;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
 * Static class used to manage translations anywhere
 * Responsible for loading files and returning translations
*/

namespace Locallies.Core {
    public static class LocalizationManager {
        // triggers localization on all elements at scene
        public static event Action<bool> MassLocalizationEvent = delegate { };

        // localized data
        private static Dictionary<string, string> textDictionary;
        private static Dictionary<string, Sprite> sheetDictionary;
        private static string missingKey = "Localized string not found!";
        private static string missingSprite;

        // default locale to be used
        private static string defaultLocale = "en";

        // loads data from Localization File
        public static void LoadLocalizationFile(string locale) {
            // loads data from file
            LocalizationText localizationText = new LocalizationText();
            LocalizationSheet localizationSheet = new LocalizationSheet();

            bool success = LocalizationParser.LoadLocalizationFile(locale, out localizationText, out localizationSheet);

            if (success) {
                // creates and populates dictionary
                textDictionary = new Dictionary<string, string>();
                foreach (LocalizationString item in localizationText.items) {
                    textDictionary.Add(item.key, item.value);
                }

                sheetDictionary = new Dictionary<string, Sprite>();
                foreach (LocalizationSprite item in localizationSheet.items) {
                    sheetDictionary.Add(item.key, item.value);
                }

                // sucessful debug
                Debug.Log("Data loaded! Dictionary contains " + textDictionary.Count + " entries!");

                // mass localization
                MassLocalize();
            }
            else {
                // error debug
                Debug.LogError("Cannot find Localization File!!");
            }
        }

        // localizes all elements listening to event
        public static void MassLocalize() {
            MassLocalizationEvent(true);
        }

        // gets value from dictionary or returns missing key message
        public static string LocalizeString(string key) {
            // loads default Localization File if no dictionary
            if (textDictionary == null) {
                LoadLocalizationFile(defaultLocale);
            }

            textDictionary.TryGetValue(key, out string result);
            result = String.IsNullOrEmpty(result) ? missingKey : result;

            return result;
        }

        public static Sprite LocalizeSprite(string key) {
            // loads default Localization File if no dictionary
            if (sheetDictionary == null) {
                LoadLocalizationFile(defaultLocale);
            }

            sheetDictionary.TryGetValue(key, out Sprite result);
            if (result == null) {
                sheetDictionary.TryGetValue(missingSprite, out result);
            }

            return result;
        }
    }
}