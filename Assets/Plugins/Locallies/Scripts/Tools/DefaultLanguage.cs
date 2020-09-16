using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Locallies.Tools {
    public class DefaultLanguage : MonoBehaviour {
        public static DefaultLanguage instance;

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