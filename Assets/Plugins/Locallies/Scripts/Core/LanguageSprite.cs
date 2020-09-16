using System;
using UnityEngine;

/*
 * Key-Value pair with the translated sprite
*/

namespace Locallies.Core {
    [Serializable]
    public class LanguageSprite {
        public string key;
        public Sprite value;
    }
}