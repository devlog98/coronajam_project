using Locallies.Core;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

/*
 * Creates an editor window in order to manage Localization Files without any additional software
 * Window located at Window/Locallies/Localization File Editor
*/

namespace Locallies.Tools {
    public class LocalizationFileEditor : EditorWindow {
        //data from the Localization File
        public LocalizationText localizationText;

        //current file properties
        private string filename = "language";
        private string fileExtension = "json";

        // window scroll position
        private Vector2 scrollPosition;

        //initializes window at path
        [MenuItem("Window/Locallies/Localization File Editor")]
        private static void Init() {
            EditorWindow.GetWindow(typeof(LocalizationFileEditor), false, "Localization File Editor", true).Show();
        }

        //window behaviour
        private void OnGUI() {
            //title
            GUILayout.Label("Localization File Editor", EditorStyles.boldLabel);
            GUILayout.Label("You can edit and create Localization Files using the buttons below", EditorStyles.label);
            GUILayout.Label("");

            //begin scroll
            scrollPosition = BeginScrollView();

            //if file is loaded...
            if (localizationText != null) {
                //allows data to be edited via Inspector
                SerializedObject serializedObject = new SerializedObject(this);
                SerializedProperty serializedProperty = serializedObject.FindProperty("localizationData");
                EditorGUILayout.PropertyField(serializedProperty, true);

                //apply changes
                serializedObject.ApplyModifiedProperties();
            }

            //begin horizontal layout
            GUILayout.BeginHorizontal();

            if (localizationText != null) {
                //save file button
                if (GUILayout.Button("Save File")) {
                    SaveLocalizationFile();
                }
            }

            //load file button
            if (GUILayout.Button("Load File")) {
                LoadLocalizationFile();
            }

            //new file button
            if (GUILayout.Button("New File")) {
                NewLocalizationFile();
            }

            //end horizontal layout and scroll
            GUILayout.EndHorizontal();
            EndScrollView();
        }

        //creates new file
        private void NewLocalizationFile() {
            localizationText = new LocalizationText();
            filename = "New Localization File";
        }

        //saves current file
        private void SaveLocalizationFile() {
            //save window
            string filepath = EditorUtility.SaveFilePanel("Save Localization File", Path.Combine(Application.streamingAssetsPath, "Localization Files"), filename, fileExtension);

            //operation
            bool success = LocalizationParser.WriteLocalizationText(filepath, localizationText);

            //if operation successful...
            if (success) {
                //store file properties
                filename = Path.GetFileNameWithoutExtension(filepath);
                fileExtension = Path.GetExtension(filepath).Replace(".", "");
            }
        }

        //loads file
        private void LoadLocalizationFile() {
            //load window
            string filepath = EditorUtility.OpenFilePanel("Load Localization File", Path.Combine(Application.streamingAssetsPath, "Localization Files"), "*json;*yml");

            //operation
            bool success = LocalizationParser.ReadLocalizationText(filepath, out localizationText);

            //if operation successful...
            if (success) {
                //store file properties
                filename = Path.GetFileNameWithoutExtension(filepath);
                fileExtension = Path.GetExtension(filepath).Replace(".", "");
            }
        }

        //begins scroll
        private Vector2 BeginScrollView() {
            //window scroll properties
            bool horizontalScroll = true;
            bool verticalScroll = true;

            //window size properties
            float windowWidth = this.position.width;
            float windowHeight = this.position.height;

            //returns scroll position
            return GUILayout.BeginScrollView(scrollPosition, horizontalScroll, verticalScroll, GUILayout.Width(windowWidth), GUILayout.Height(windowHeight));
        }

        //ends scroll
        private void EndScrollView() {
            GUILayout.EndScrollView();
        }
    }
}