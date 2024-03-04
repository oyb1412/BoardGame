using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DiceGame {
    [CreateAssetMenu(fileName = "new ItemInfo", menuName ="ScriptableObjects/ItemInfo")]
    public class ItemInfo : ScriptableObject {
        [field : SerializeField] public int id { get; private set; }
        [field: SerializeField] public string descrpition { get; private set; }
        [field: SerializeField] public int maxNumber { get; private set; }
        [field: SerializeField] public Sprite icon { get; private set; }

    }
}
