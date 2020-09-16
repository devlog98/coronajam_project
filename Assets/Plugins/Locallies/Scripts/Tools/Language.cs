using UnityEngine;

/*
 * Specifies a language option to choose from via Language Manager
*/

namespace Locallies.Tools {
    public enum LocalizationTextFormat { JSON, YAML };

    [CreateAssetMenu(fileName = "New Language Item", menuName = "Locallies/New Language Item")]
    public class Language : ScriptableObject {
        [SerializeField] private new string name; // language name

        [SerializeField] private TextAsset localizationText; // language text file with keys and values
        [SerializeField] private Sprite[] localizationSprite; // language translated images
        [SerializeField] private LocalizationTextFormat localizationTextFormat;

        public TextAsset LocalizationText { get { return localizationText; } }
        public Sprite[] LocalizationSprite { get { return localizationSprite; } }
        public LocalizationTextFormat LocalizationTextFormat { get { return localizationTextFormat; } }
    }
}