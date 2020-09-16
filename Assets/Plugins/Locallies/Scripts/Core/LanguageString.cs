using System;

/*
 * Key-Value pair with the translated text
*/

namespace Locallies.Core {
    [Serializable]
    public class LanguageString {
        public string key;
        public string value;
    }
}