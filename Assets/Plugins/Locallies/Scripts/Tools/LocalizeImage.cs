using Locallies.Core;
using UnityEngine.UI;

/*
 * Localizes element of type Image
 * Put this script into an Image object to use it
*/

namespace Locallies.Tools {
    public class LocalizeImage : LocalizeObject {
        // reference of Text object
        private Image element;

        // initial setup
        private void Awake() {
            element = GetComponent<Image>();
        }

        // updates in game image
        public override void Localize(bool canLocalize) {
            element.sprite = LocalizationManager.LocalizeSprite(key);
        }
    }
}