using UnityEngine;

/*
 * Specifies a language option to choose from via Language Selector
*/

namespace Locallies.Tools {
    public enum TextFormat { JSON, YAML };

    [CreateAssetMenu(fileName = "New Language", menuName = "Locallies/New Language")]
    public class Language : ScriptableObject {
        [SerializeField] private new string name; // language name

        [SerializeField] private TextAsset text; // language text file with keys and values
        [SerializeField] private TextFormat textFormat; // language text format
        [SerializeField] private Sprite[] sheet; // language translated images

        public TextAsset Text { get { return text; } }
        public Sprite[] Sheet { get { return sheet; } }
        public TextFormat TextFormat { get { return textFormat; } }
    }
}