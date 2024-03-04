using DiceGame;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace DiceGame {
    public class ItemInfoResources : MonoBehaviour {
        public ItemInfo this[int id] {
            get {
                return _dictionary[id];
            }
        }
        [SerializeField] List<ItemInfo> _list;
        Dictionary<int, ItemInfo> _dictionary;

        #region singleton
        private static ItemInfoResources s_instance;
        public static ItemInfoResources instance {
            get {
                if (s_instance == null) {
                    s_instance =  Instantiate(Resources.Load<ItemInfoResources>(nameof(ItemInfoResources)));
                }
                return s_instance;
            }
        }
        #endregion

        private void Awake() {
            _dictionary = new Dictionary<int, ItemInfo>();
            foreach(var item in _list) {
                _dictionary.TryAdd(item.id, item);
            }
        }
    }
}
