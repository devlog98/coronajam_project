using UnityEngine;

/*
 * Put at scene in order to load a language for tests
*/

namespace Locallies.Tools {
    public class DefaultLanguage : MonoBehaviour {
        public static DefaultLanguage instance;

        // language 
        [SerializeField] private Language language;
        public Language Language { get { return language; } }

        //setting singleton instance
        private void Awake() {
            if (instance != null && instance != this) {
                Destroy(this.gameObject);
            }
            else {
                instance = this;
            }
        }
    }
}