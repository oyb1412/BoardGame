using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace DiceGame.Data.Mock {
    public class MockInventoryRepository : MonoBehaviour , IRepositoryOfT<InventorySlotDataModel> {
        private const int DEFAULT_CAPACITY = 30;
        private readonly string _path;
        private List<InventorySlotDataModel> _inventorySlotDataModels;

        public MockInventoryRepository() {
            _path = Application.persistentDataPath + "/Inventory.json";

            if(File.Exists(_path)) {
                _inventorySlotDataModels = JsonConvert.DeserializeObject<List<InventorySlotDataModel>>(File.ReadAllText(_path));
            } else {
                _inventorySlotDataModels = new List<InventorySlotDataModel>(DEFAULT_CAPACITY);

                for (int i = 0; i < DEFAULT_CAPACITY; i++) {
                    _inventorySlotDataModels.Add(new InventorySlotDataModel(101, 1));
                }
                string obj = JsonConvert.SerializeObject(_inventorySlotDataModels);
                File.WriteAllText(_path, obj);
            }
        }


        public event Action<int, InventorySlotDataModel> onItemUpdated;

        public void DeleteItem(InventorySlotDataModel item) {
            _inventorySlotDataModels.Remove(item);
        }

        public IEnumerable<InventorySlotDataModel> GetAllItem() {
            return _inventorySlotDataModels;
        }

        public InventorySlotDataModel GetItemByID(int id) {
            return _inventorySlotDataModels[id];
        }

        public void InsertItem(InventorySlotDataModel item) {
            _inventorySlotDataModels.Add(item);
            Save();
        }

        public void Save() {
            string obj = JsonConvert.SerializeObject(_inventorySlotDataModels);
            File.WriteAllText(_path, obj);
        }

        public void UpdateItem(InventorySlotDataModel item, int id) {
            _inventorySlotDataModels[id] = item;
            Save();
            onItemUpdated?.Invoke(id, item);
        }


    }
}