using Locallies.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YamlDotNet.Serialization;

/*
 * Reads and writes data into Languages
 * Supports .json and .yml files
*/

namespace Locallies.Core {
    public static class LanguageParser {
        //reads all files from language
        public static bool ReadLanguage(Language language, out LanguageText languageText, out LanguageSheet languageSheet) {
            bool success = true;

            // read text
            languageText = ReadLanguageText(language.Text, language.TextFormat);
            if (language.Text != null && languageText == null) {
                success = false;
            }

            // read sheet
            languageSheet = ReadLanguageSheet(language.Sheet);
            if (language.Sheet != null && languageSheet.items.Length == 0) {
                success = false;
            }

            return success;
        }

        //reads text data from Language
        public static LanguageText ReadLanguageText(TextAsset text, TextFormat textFormat) {
            LanguageText languageText = null;

            // if file is valid...
            string textData = text.text;
            if (!String.IsNullOrEmpty(textData)) {
                switch (textFormat) {
                    case TextFormat.JSON:
                        languageText = FromJson(textData);
                        break;
                    case TextFormat.YAML:
                        languageText = FromYaml(textData);
                        break;
                }
            }

            // result feedback
            return languageText;
        }

        //reads sheet data from file
        public static LanguageSheet ReadLanguageSheet(Sprite[] sprites) {
            LanguageSheet languageSheet = new LanguageSheet();

            // loop through all sprites
            List<LanguageSprite> languageSprites = new List<LanguageSprite>();
            foreach (Sprite sprite in sprites) {
                LanguageSprite languageSprite = new LanguageSprite();

                languageSprite.key = Path.GetFileNameWithoutExtension(sprite.name);
                languageSprite.value = sprite;
                languageSprites.Add(languageSprite);
            }
            languageSheet.items = languageSprites.ToArray();

            // result feedback
            return languageSheet;
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