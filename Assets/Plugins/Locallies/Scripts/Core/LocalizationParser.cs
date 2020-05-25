using System;
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
        //writes data into file
        public static bool WriteLocalizationFile(string filepath, LocalizationData localizationData) {
            bool success = false;

            //if filepath is valid...
            if (!String.IsNullOrEmpty(filepath)) {
                //serialize data based on extension
                string fileData = null;
                string fileExtension = Path.GetExtension(filepath);

                switch (fileExtension) {
                    case ".json":
                        fileData = ToJson(localizationData);
                        break;
                    case ".yml":
                        fileData = ToYaml(localizationData);
                        break;
                }

                //write data
                File.WriteAllText(filepath, fileData);
                success = true;
            }

            //result feedback
            return success;
        }

        //reads data from file
        public static bool ReadLocalizationFile(string filepath, out LocalizationData localizationData) {
            bool success = false;
            localizationData = null;


            //ANDROID TEST
            UnityWebRequest request = UnityWebRequest.Get(filepath);

            request.SendWebRequest();

            //HACK PERIGOSÍSSIMO
            while (!request.isDone) { Debug.Log(request.uri); }

            //read data based on extension
            string fileData = request.downloadHandler.text;
            string fileExtension = Path.GetExtension(filepath);

            switch (fileExtension) {
                case ".json":
                    localizationData = FromJson(fileData);
                    break;
                case ".yml":
                    localizationData = FromYaml(fileData);
                    break;
            }

            success = true;


            //if file is valid...
            //if (File.Exists(filepath)) {
            //    //read data based on extension
            //    string fileData = File.ReadAllText(filepath);
            //    string fileExtension = Path.GetExtension(filepath);

            //    switch (fileExtension) {
            //        case ".json":
            //            localizationData = FromJson(fileData);
            //            break;
            //        case ".yml":
            //            localizationData = FromYaml(fileData);
            //            break;
            //    }

            //    success = true;
            //}

            //result feedback
            return success;
        }

        //json utility methods
        private static string ToJson(LocalizationData data) {
            return JsonUtility.ToJson(data);
        }
        private static LocalizationData FromJson(string data) {
            return JsonUtility.FromJson<LocalizationData>(data);
        }

        //yaml utility methods
        private static string ToYaml(LocalizationData data) {
            ISerializer serializer = new SerializerBuilder().Build();
            return serializer.Serialize(data);
        }
        private static LocalizationData FromYaml(string data) {
            IDeserializer deserializer = new DeserializerBuilder().Build();
            return deserializer.Deserialize<LocalizationData>(data);
        }
    }
}