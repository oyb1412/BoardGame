using DiceGame.Data;
using DiceGame.Game;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DiceGame.UI {
    /// <summary>
    /// 인벤토리UI 관리 클래스
    /// </summary>
    public class UIIventory : UIPopUpBase {
        //각 인벤토리 슬롯 프리펩
        [SerializeField] InventorySlot _slotPrefab;
        //인벤토리 슬롯을 생성할 부모 
        [SerializeField] Transform _slotContent;
        //각 슬롯의 정보를 보관할 리스트
        private List<InventorySlot> _slots = new List<InventorySlot>();

        //현재 내가 사용하고 있는 리포지토리 저장용
        private IRepositoryOfT<InventorySlotDataModel> _repository;
        private int _selectedSlotID = NOT_SELECTED;
        private const int NOT_SELECTED = -1;
        [SerializeField] Image _selecetedFollowingPreview;
        List<RaycastResult> result = new List<RaycastResult>();

        protected override void Awake() {
            base.Awake();

            //GameManager에서 선별한 현재 내가 사용중인 리포지토리를 가져온다.
            _repository = GameManager.instance.unitOfWork.inventoryRepository;

            //슬롯에 특정 아이디를 부여하기 위한 변수
            int count = 0;
            //사용중인 리포지토리에 존재하는 모든 아이템을 순회하며 정보를 가져온다.
            foreach(var item in _repository.GetAllItem()) {
                //슬롯 프리펩을 컨텐트 위치에 생성한다.
                var slot = Instantiate(_slotPrefab,_slotContent);
                //생성한 슬롯에 id를 부여하고, 다음 부여할 id의 값을 1 높인다.
                slot.slotID = count++;

                slot.Refresh(item.itemID, item.itemNum);

                //생성, 초기화가 끝난 슬롯의 정보를 리스트에 보관한다.
                _slots.Add(slot);
            }

            //이벤트에 슬롯의 정보와 그 정보를 토대로 이미지, 텍스트가 변경되는 함수를 구독시킨다.
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
                result.Clear(); // 깔끔하게 만들고 
                RayCast(result); // 레이케스트 시도

                // 뭔가 상호작용 할만한게 이 캔버스에 있다.
                if (result.Count > 0) // 0보다 크다면
                {
                    foreach (var result in result) {
                        // 케스팅된 타겟중에 슬롯이 있다면. 해당 슬롯을 선택
                        if (result.gameObject.TryGetComponent(out InventorySlot slot)) {
                            // 다른 슬롯이 선택되었다면 스왑
                            if (_selectedSlotID != slot.slotID) // 선택한 슬롯 ID와 캐스팅된 슬롯의 ID가 다르다면
                            {
                                var selectedSlotData = _repository.GetItemByID(_selectedSlotID);
                                var castedSlotData = _repository.GetItemByID(slot.slotID);
                                _repository.UpdateItem( new InventorySlotDataModel(selectedSlotData), slot.slotID); // 이렇게 해줘야 castedSlotData의 값이 안바뀜 그냥 selectedSLotData를 넘기면 바뀜
                                _repository.UpdateItem(castedSlotData, _selectedSlotID);
                                Deselect(); // 자료 다 바꿧으니까 선택을 해제해주면 됨
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