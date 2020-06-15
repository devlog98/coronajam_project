using System;

/*
 * Key-Value pair with the translated text
*/

namespace Locallies.Core {
    [Serializable]
    public class LocalizationString {
        public string key;
        public string value;
    }
}