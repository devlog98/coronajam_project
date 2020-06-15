using Locallies.Core;
using TMPro;

/*
 * Localizes element of type TextMeshPro
 * Put this script into a TextMeshPro object to use it
*/

namespace Locallies.Tools {
    public class LocalizeTextMeshPro : LocalizeObject {
        // reference of TextMeshPro object
        private TextMeshProUGUI element;

        // initial setup
        private void Awake() {
            element = GetComponent<TextMeshProUGUI>();
        }

        // updates in game text
        public override void Localize(bool canLocalize) {
            element.text = LocalizationManager.LocalizeString(key);
        }
    }
}