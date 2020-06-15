using System;

/*
 * Used to arrange data from and to Localization Files
*/

namespace Locallies.Core {
    [Serializable]
    public class LocalizationText {
        // array of translations
        public LocalizationString[] items;
    }
}