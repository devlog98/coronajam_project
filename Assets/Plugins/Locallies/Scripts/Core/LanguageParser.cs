using Locallies.Tools;
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
    public static class LanguageParser {
        //reads all files from locale
        public static bool LoadLocalizationFile(Language language, out LanguageText localizationText, out LanguageSheet localizationSheet) {
            bool success = false;

            // setup for data
            localizationText = ReadLocalizationText(language.LocalizationText, language.LocalizationTextFormat);

            // read sheet
            localizationSheet = ReadLocalizationSprite(language.LocalizationSprite);

            success = true;

            return success;
        }

        //writes text data into file
        public static bool WriteLocalizationText(string filepath, LanguageText localizationText) {
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
        public static LanguageText ReadLocalizationText(TextAsset file, LocalizationTextFormat format) {
            LanguageText localizationText = null;

            string fileData = file.text;

            // if file is valid...
            if (!String.IsNullOrEmpty(fileData)) {
                switch (format) {
                    case LocalizationTextFormat.JSON:
                        localizationText = FromJson(fileData);
                        break;
                    case LocalizationTextFormat.YAML:
                        localizationText = FromYaml(fileData);
                        break;
                }
            }

            // result feedback
            return localizationText;
        }

        //reads sheet data from file
        public static LanguageSheet ReadLocalizationSprite(Sprite[] file) {
            LanguageSheet localizationSheet = new LanguageSheet();

            List<LanguageSprite> sprites = new List<LanguageSprite>();
            foreach (Sprite sprite in file) {
                LanguageSprite localizationSprite = new LanguageSprite();
                localizationSprite.key = Path.GetFileNameWithoutExtension(sprite.name);
                localizationSprite.value = sprite;

                sprites.Add(localizationSprite);
            }

            localizationSheet.items = sprites.ToArray();

            // result feedback
            return localizationSheet;
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
        private static string ToJson(LanguageText data) {
            return JsonUtility.ToJson(data);
        }
        private static LanguageText FromJson(string data) {
            return JsonUtility.FromJson<LanguageText>(data);
        }

        //yaml utility methods
        private static string ToYaml(LanguageText data) {
            ISerializer serializer = new SerializerBuilder().Build();
            return serializer.Serialize(data);
        }
        private static LanguageText FromYaml(string data) {
            IDeserializer deserializer = new DeserializerBuilder().Build();
            return deserializer.Deserialize<LanguageText>(data);
        }
    }
}