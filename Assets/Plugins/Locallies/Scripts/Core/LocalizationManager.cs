using Locallies.Core;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
 * Singleton used to deliver translations anywhere
 * Put this script into an empty Game Object to use it
*/

namespace Locallies.Core {
    public static class LocalizationManager {
        // event that triggers element localization
        public static event Action<bool> MassLocalizationEvent = delegate { };

        //dictionary with data and related attributes
        private static Dictionary<string, string> localDictionary;
        private static string missingKey = "Localized string not found!";
        private static string locale;

        //Localization File used if none was found
        private static string defaultLocalizationFile = "pt_br.yml";

        // loads data from Localization File into dictionary
        public static void LoadLocalizationFile(string filename) {
            //searches Localization File
            string filepath = Path.Combine(Application.streamingAssetsPath, "Localization Files", filename);

            //loads data from file
            LocalizationData localizationData = new LocalizationData();
            bool success = LocalizationParser.ReadLocalizationFile(filepath, out localizationData);

            //if operation successful...
            if (success) {
                //creates and populates dictionary
                localDictionary = new Dictionary<string, string>();
                foreach (LocalizationItem item in localizationData.items) {
                    localDictionary.Add(item.key, item.value);
                }

                //sucessful debug
                Debug.Log("Data loaded! Dictionary contains " + localDictionary.Count + " entries!");

                //save locale
                locale = filename.Split('.')[0];

                //activates mass localization
                MassLocalize();
            }
            else {
                //error debug
                Debug.LogError("Cannot find Localization File!!");
            }
        }

        // localizes all elements listening to event
        public static void MassLocalize() {
            MassLocalizationEvent(true);
        }

        // gets value from dictionary or returns missing key message
        public static string Localize(string key) {
            //loads default Localization File if no dictionary
            if (localDictionary == null) {
                LoadLocalizationFile(defaultLocalizationFile);
            }

            localDictionary.TryGetValue(key, out string result);
            result = String.IsNullOrEmpty(result) ? missingKey : result;

            return result;
        }

        public static Sprite LocalizeImage(string key) {
            Sprite sprite = null;

            string filepath = Path.Combine(Application.streamingAssetsPath, "Localization Images", locale, key);
            if (File.Exists(filepath)) {
                byte[] fileData = File.ReadAllBytes(filepath);

                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(fileData);

                sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            }

            return sprite;
        }
    }
}