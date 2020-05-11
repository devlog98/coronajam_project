using Locallies.Tools;
using UnityEngine;

/*
 * Base for all Game Objects to be localized
*/

namespace Locallies.Core {
    public abstract class LocalizeObject : MonoBehaviour {
        // used to find the correct translation
        [SerializeField] protected string key;

        // listeners setup
        protected void OnEnable() {
            LocalizationManager.MassLocalizationEvent += Localize;
        }
        protected void OnDisable() {
            LocalizationManager.MassLocalizationEvent -= Localize;
        }

        // initial localization
        private void Start() {
            Localize(true);
        }

        // updates in game text
        public abstract void Localize(bool canLocalize);
    }
}