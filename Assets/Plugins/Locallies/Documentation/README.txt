# LOCALLIES

Locallies is a localization system based on Unity's official Localization tutorial

## Features
- Support for Json and Yaml files
- Useful for Text and TextMeshPro elements
- Basic UI for the Player to choose a language
- Built-in editor to edit your Localization Files

## How to use
### Create Localization File
- Go to "Window/Locallies/Localization File Manager"
- Click on "New File"
- Then you must populate your file with keys and the respective translated values
- When finished, never forget to click on "Save File"*
- You can always load it again for editing with the "Load File" button

*For the sake of organization, Locallies will only search for files inside the folder "StreamingAssets/Locallies"

### Localize String
- Add the "LocalizeText" or "LocalizeTextMeshPro" component to the respective element*
- Fill the field "key" with the desired key for localization

*You can also take advantage from the prefabs on the Prefabs folder

### Change Language
- Put the "Language Manager" prefab on your scene
- Add or remove items from the field "languages"*

*Always remember to put the file name with its extension

### Change Default Fallbacks
Locallies has specific fallbacks that can be changed on the "LocalizationManager" script*
- Change the fallback Localization File in the field "defaultLocalizationFile"
- Change the fallback missing key value in the field "missingKey" 

*Found in "Code/Core"

## Demos
### Default Localization File Sample
- Shows how the Localization Manager loads a file by default

### Language Manager Sample
- Shows how you can use the Language Manager to change the currently applied language

## References
This plugin wouldn't exist without
- Unity's "Localization Tools" tutorial: https://www.youtube.com/watch?v=5Kt9jbnqzKA&list=PLX2vGYjWbI0TWkV9aEYq93bOX2kwseqUT
- Beetle Circus' "YamlDotNet for Unity" asset*: https://assetstore.unity.com/packages/tools/integration/yamldotnet-for-unity-36292

*Keep in mind that Locallies comes bundled with the YamlDotNet package to allow Yaml parsing