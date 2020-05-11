using System;

/*
 * Used to arrange data from and to Localization Files
*/

namespace Locallies.Core {
    [Serializable]
    public class LocalizationData {
        //array of translations
        public LocalizationItem[] items;
    }
}