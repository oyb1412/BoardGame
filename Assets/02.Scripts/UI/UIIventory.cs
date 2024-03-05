using DiceGame.Data;
using DiceGame.Game;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DiceGame.UI {
    /// <summary>
    /// �κ��丮UI ���� Ŭ����
    /// </summary>
    public class UIIventory : UIPopUpBase {
        //�� �κ��丮 ���� ������
        [SerializeField] InventorySlot _slotPrefab;
        //�κ��丮 ������ ������ �θ� 
        [SerializeField] Transform _slotContent;
        //�� ������ ������ ������ ����Ʈ
        private List<InventorySlot> _slots = new List<InventorySlot>();

        //���� ���� ����ϰ� �ִ� �������丮 �����
        private IRepositoryOfT<InventorySlotDataModel> _repository;
        private int _selectedSlotID = NOT_SELECTED;
        private const int NOT_SELECTED = -1;
        [SerializeField] Image _selecetedFollowingPreview;
        List<RaycastResult> result = new List<RaycastResult>();

        protected override void Awake() {
            base.Awake();

            //GameManager���� ������ ���� ���� ������� �������丮�� �����´�.
            _repository = GameManager.instance.unitOfWork.inventoryRepository;

            //���Կ� Ư�� ���̵� �ο��ϱ� ���� ����
            int count = 0;
            //������� �������丮�� �����ϴ� ��� �������� ��ȸ�ϸ� ������ �����´�.
            foreach(var item in _repository.GetAllItem()) {
                //���� �������� ����Ʈ ��ġ�� �����Ѵ�.
                var slot = Instantiate(_slotPrefab,_slotContent);
                //������ ���Կ� id�� �ο��ϰ�, ���� �ο��� id�� ���� 1 ���δ�.
                slot.slotID = count++;

                slot.Refresh(item.itemID, item.itemNum);

                //����, �ʱ�ȭ�� ���� ������ ������ ����Ʈ�� �����Ѵ�.
                _slots.Add(slot);
            }

            //�̺�Ʈ�� ������ ������ �� ������ ���� �̹���, �ؽ�Ʈ�� ����Ǵ� �Լ��� ������Ų��.
            _repository.onItemUpdated += (slotID, slotData) => { _slots[slotID].Refresh(slotData.itemID, slotData.itemNum); };

        }

        public override void InputAction() {
            base.InputAction();

            if(Input.GetMouseButtonDown(0)) {
                if(_selectedSlotID == NOT_SELECTED) {
                    RayCast(result);

                    if(result.Count > 0) {
                        foreach(var item in result) {
                            if(item.gameObject.TryGetComponent(out InventorySlot slot)) {
                                if(_repository.GetItemByID(slot.slotID).isEmpty) {
                                    Select(slot.slotID);
                                    return;
                                }

                            }
                        }
                    }
                } else if (Input.GetMouseButtonDown(1)) {
                    if (_selectedSlotID != NOT_SELECTED) {
                        Deselect();
                    }
                }
            } else {
                result.Clear(); // ����ϰ� ����� 
                RayCast(result); // �����ɽ�Ʈ �õ�

                // ���� ��ȣ�ۿ� �Ҹ��Ѱ� �� ĵ������ �ִ�.
                if (result.Count > 0) // 0���� ũ�ٸ�
                {
                    foreach (var result in result) {
                        // �ɽ��õ� Ÿ���߿� ������ �ִٸ�. �ش� ������ ����
                        if (result.gameObject.TryGetComponent(out InventorySlot slot)) {
                            // �ٸ� ������ ���õǾ��ٸ� ����
                            if (_selectedSlotID != slot.slotID) // ������ ���� ID�� ĳ���õ� ������ ID�� �ٸ��ٸ�
                            {
                                var selectedSlotData = _repository.GetItemByID(_selectedSlotID);
                                var castedSlotData = _repository.GetItemByID(slot.slotID);
                                _repository.UpdateItem( new InventorySlotDataModel(selectedSlotData), slot.slotID); // �̷��� ����� castedSlotData�� ���� �ȹٲ� �׳� selectedSLotData�� �ѱ�� �ٲ�
                                _repository.UpdateItem(castedSlotData, _selectedSlotID);
                                Deselect(); // �ڷ� �� �مf���ϱ� ������ �������ָ� ��
                                return;
                            }

                        }
                    }
                }
            }

            if(_selectedSlotID != NOT_SELECTED) {
                _selecetedFollowingPreview.transform.position = Input.mousePosition;
            }
        }

        private void Select(int slotID) {
            _selectedSlotID = slotID;
            _selecetedFollowingPreview.sprite = _slots[slotID]._icon.sprite;
            _selecetedFollowingPreview.enabled = true;
            _selecetedFollowingPreview.transform.position = Input.mousePosition;
        }  
        private void Deselect() {
            _selectedSlotID = NOT_SELECTED;
            _selecetedFollowingPreview.sprite = null;
            _selecetedFollowingPreview.enabled = false;
        }
    }
}