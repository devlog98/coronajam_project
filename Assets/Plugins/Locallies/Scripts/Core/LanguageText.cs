using System;

/*
 * Used to arrange data from and to Localization Files
*/

namespace Locallies.Core {
    [Serializable]
    public class LanguageText {
        // array of translations
        public LanguageString[] items;
    }
}