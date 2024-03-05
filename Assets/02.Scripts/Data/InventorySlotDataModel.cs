using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DiceGame.Data {

    [Serializable]
    public class InventorySlotDataModel  {

        [JsonConstructor]
        public InventorySlotDataModel() { }
        public InventorySlotDataModel(int itemID, int itemNum) { this.itemID = itemID; this.itemNum = itemNum; }
        public InventorySlotDataModel(InventorySlotDataModel copy) { itemID = copy.itemID ; itemNum = copy.itemNum; }

        [JsonIgnore]
        public bool isEmpty => itemID == 0 || itemNum == 0;

        public int itemID;
        public int itemNum;

        public bool Equals(InventorySlotDataModel other) {
            return (itemID == other.itemID) && (itemNum == other.itemNum);
        }
    }
}