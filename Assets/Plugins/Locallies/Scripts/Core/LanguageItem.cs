using System;
using UnityEngine;

/*
 * Specifies a language option to choose from via Language Manager
*/

namespace Locallies.Core {
    [Serializable]
    public class LanguageItem {
        // Localization File name and correspondent locale flag
        public string fileName;
        public Sprite languageFlagSprite;
    }
}