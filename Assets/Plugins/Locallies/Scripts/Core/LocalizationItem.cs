using System;

/*
 * Key-Value pair with the translated text
*/

namespace Locallies.Core {
    [Serializable]
    public class LocalizationItem {
        public string key;
        public string value;
    }
}