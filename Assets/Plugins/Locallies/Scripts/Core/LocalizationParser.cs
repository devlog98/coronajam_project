 using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using YamlDotNet.Serialization;

/*
 * Reads and writes data into Localization Files
 * Supports .json and .yml files
*/

namespace Locallies.Core {
    public static class LocalizationParser {
        //reads all files from locale
        public static bool LoadLocalizationFile(string locale, out LocalizationText localizationText, out LocalizationSheet localizationSheet) {
            bool success = false;

            // get all files from locale folder
            string path = Path.Combine(Application.streamingAssetsPath, "Localization Files", locale);
            string[] filenames = new string[] { locale + ".yml", "locallies_flag.png", "text1.png", "text_2.png", "tutorial.png" };//GetFilenamesFromPath(path);

            // setup for data
            localizationText = new LocalizationText();
            localizationSheet = new LocalizationSheet();
            List<LocalizationString> localizationStrings = new List<LocalizationString>();
            List<LocalizationSprite> localizationSprites = new List<LocalizationSprite>();

            foreach (string file in filenames) {
                string filepath = Path.Combine(path, file);
                string fileExtension = Path.GetExtension(filepath);

                if (fileExtension == ".yml" || fileExtension == ".json") {
                    // read text
                    bool finished = false;
                    LocalizationText text = new LocalizationText();
                    finished = ReadLocalizationText(filepath, out text);

                    if (finished) {
                        localizationStrings.AddRange(text.items);
                        success = true;
                    }
                }
                else {
                    // read sheet
                    bool finished = false;
                    LocalizationSprite sprite = new LocalizationSprite();
                    finished = ReadLocalizationSprite(filepath, out sprite);

                    if (finished) {
                        localizationSprites.Add(sprite);
                        success = true;
                    }
                }
            }

            // store data inside object
            localizationText.items = localizationStrings.ToArray();
            localizationSheet.items = localizationSprites.ToArray();

            return success;
        }

        //writes text data into file
        public static bool WriteLocalizationText(string filepath, LocalizationText localizationText) {
            bool success = false;

            //if filepath is valid...
            if (!String.IsNullOrEmpty(filepath)) {
                //serialize data based on extension
                string fileData = null;
                string fileExtension = Path.GetExtension(filepath);

                switch (fileExtension) {
                    case ".json":
                        fileData = ToJson(localizationText);
                        break;
                    case ".yml":
                        fileData = ToYaml(localizationText);
                        break;
                }

                //write data
                File.WriteAllText(filepath, fileData);
                success = true;
            }

            //result feedback
            return success;
        }

        //reads text data from file
        public static bool ReadLocalizationText(string filepath, out LocalizationText localizationText) {
            bool success = false;
            localizationText = null;

            // searches Localization File
            UnityWebRequest fileRequest = UnityWebRequest.Get(filepath);
            fileRequest.SendWebRequest();

            while (!fileRequest.isDone) { }

            string fileData = fileRequest.downloadHandler.text;

            // if file is valid...
            if (!String.IsNullOrEmpty(fileData)) {
                // read data based on extension
                string fileExtension = Path.GetExtension(filepath);

                switch (fileExtension) {
                    case ".json":
                        localizationText = FromJson(fileData);
                        break;
                    case ".yml":
                        localizationText = FromYaml(fileData);
                        break;
                }

                success = true;
            }

            // result feedback
            return success;
        }

        //reads sheet data from file
        public static bool ReadLocalizationSprite(string filepath, out LocalizationSprite localizationSprite) {
            bool success = false;
            localizationSprite = new LocalizationSprite();

            // searches Localization File
            UnityWebRequest fileRequest = UnityWebRequest.Get(filepath);
            fileRequest.SendWebRequest();

            while (!fileRequest.isDone) { }

            byte[] fileData = fileRequest.downloadHandler.data;

            // if file is valid...
            if (fileData != null) {
                // read and save data
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(fileData);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                localizationSprite.key = Path.GetFileNameWithoutExtension(filepath);
                localizationSprite.value = sprite;

                success = true;
            }

            // result feedback
            return success;
        }

        //get files from path
        private static string[] GetFilenamesFromPath(string path) {
            List<String> filenames = new List<String>();
            
            string[] files = Directory.GetFiles(path);

            foreach (string file in files) {
                string fileExtension = Path.GetExtension(file);

                // ignore .meta files
                if (fileExtension != ".meta") {
                    filenames.Add(file);
                }
            }

            return filenames.ToArray();
        }

        //json utility methods
        private static string ToJson(LocalizationText data) {
            return JsonUtility.ToJson(data);
        }
        private static LocalizationText FromJson(string data) {
            return JsonUtility.FromJson<LocalizationText>(data);
        }

        //yaml utility methods
        private static string ToYaml(LocalizationText data) {
            ISerializer serializer = new SerializerBuilder().Build();
            return serializer.Serialize(data);
        }
        private static LocalizationText FromYaml(string data) {
            IDeserializer deserializer = new DeserializerBuilder().Build();
            return deserializer.Deserialize<LocalizationText>(data);
        }
    }
}