using Locallies.Core;
using UnityEngine.UI;

/*
 * Localizes element of type Text
 * Put this script into a Text object to use it
*/

namespace Locallies.Tools {
    public class LocalizeText : LocalizeObject {
        // reference of Text object
        private Text element;

        // initial setup
        private void Awake() {
            element = GetComponent<Text>();
        }

        // updates in game text
        public override void Localize(bool canLocalize) {
            element.text = LocalizationManager.LocalizeString(key);
        }
    }
}