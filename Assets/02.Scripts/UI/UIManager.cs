using DiceGame.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
namespace DiceGame.UI {
    public class UIManager : SingletonBase<UIManager> {
        private Dictionary<Type, IUI> _uis = new Dictionary<Type, IUI>();
        private List<IUIScreen> _screens = new List<IUIScreen>();
        private LinkedList<IUIPopUp> _popUps = new LinkedList<IUIPopUp>();
        private List<RaycastResult> _raycastResult = new List<RaycastResult>();

        /// <summary>
        /// UI 최초 등록
        /// </summary>
        /// <param name="ui">등록할 UI</param>
        /// <exception cref="Exception">동일한 UI가 씬에 두개 이상 존재할 때 호출</exception>
        public void Register(IUI ui) {
            if(_uis.TryAdd(ui.GetType(),ui)) {
                if (ui is IUIScreen)
                    _screens.Add((IUIScreen)ui);
            }
            else {
                throw new Exception($"[UIManager] : Failed to register {ui.GetType()}");
            }
        }

        /// <summary>
        /// UI 인터페이스 검색
        /// </summary>
        /// <typeparam name="T">가져오려는 UI 타입</typeparam>
        /// <returns>UI 인터페이스</returns>
        /// <exception cref="Exception">가져오려는 UI가 존재하지 않을 때 호출</exception>
        public T Get<T>()
            where T : IUI {
            if(_uis.TryGetValue(typeof(T), out IUI ui)) {
                return (T)ui;
            }
            else {
                throw new Exception($"[UIManager] : Failed to Get {typeof(T)}");
            }
        }

        /// <summary>
        /// 새로 열 PopUp의 정렬 순서를 가장 뒤로 보냄 
        /// </summary>
        /// <param name="ui">새로 보여줄 PopUp</param>
        public void Push(IUIPopUp ui) {
            int sortingOrder = 1;
            //기존에 popUp이 하나 이상 존재한다면
            if(_popUps.Last?.Value != null) {
                _popUps.Last.Value.inputActionEnable = false; // 기존 popUp의 입력처리 비활성화.
                sortingOrder = _popUps.Last.Value.sortingOrder + 1; // 기존 popUp 보다 앞으로 정렬

                if (_popUps.Count > 50)
                    RearrangePopUpSortingOrders();
            }
            ui.inputActionEnable = true; //새 popUp의 입력처리 활성화.
            ui.sortingOrder = sortingOrder;
            _popUps.Remove(ui); //새 popUp이 기존에 존재하던 팝업이면 제거
            _popUps.AddLast(ui); //새 popUp을 가장 위에 추가

            if(_popUps.Count == 1) {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }

        /// <summary>
        /// PopUp 을 제거, 이전 PopUp이 있다면 이전 PopUp의 입력처리 활성화
        /// </summary>
        /// <param name="ui">제거할 UI</param>
        public void Pop(IUIPopUp ui) {
            //제거하려는 Popup이 마지막이면서 이전 Popup이 있다면
            if(_popUps.Count >= 2 && ui == _popUps.Last.Value) 
                _popUps.Last.Previous.Value.inputActionEnable = true;

            _popUps.Remove(ui);

            if(_popUps.Count == 0) {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        public List<RaycastResult> RayCastAll() {
            _raycastResult.Clear();

            for(LinkedListNode<IUIPopUp> node = _popUps.Last; node != null; node = node.Previous) {
                node.Value.RayCast(_raycastResult);
            }

            foreach(var screen in _screens) {
                screen.RayCast(_raycastResult);
            }

            return _raycastResult;
        }

        /// <summary>
        /// 현재 포인터 위치에서 다른 캔버스의 RaycastTarget 이 있는지 검출
        /// </summary>
        /// <param name="ui">현재 ui</param>
        /// <param name="other">다른 캔버스의 ui</param>
        /// <returns>검출 여부</returns>
        public bool TryCastOther(IUI ui, out IUI other, out GameObject hovered) {
            other = null;
            hovered = null;
            _raycastResult.Clear();

            for (LinkedListNode<IUIPopUp> node = _popUps.Last; node != null; node = node.Previous) {

                node.Value.RayCast(_raycastResult);

                if(_raycastResult.Count > 0) {
                    if (node.Value == ui)
                        return false;
                    hovered = _raycastResult[0].gameObject;
                    other = hovered.transform.root.GetComponent<IUI>();
                    return true;
                }
            }

            foreach (var screen in _screens) {
                screen.RayCast(_raycastResult);

                if (_raycastResult.Count > 0) {
                    if (screen == ui)
                        return false;
                    hovered = _raycastResult[0].gameObject;
                    other = hovered.transform.root.GetComponent<IUI>();
                    return true;
                }
            }

            return false;
        }

        private void RearrangePopUpSortingOrders() {
            int sortingOrder = 1;
            foreach(var popUp in _popUps) {
                popUp.sortingOrder = sortingOrder++;
            }
        }
    }
}