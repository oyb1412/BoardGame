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
        /// UI ���� ���
        /// </summary>
        /// <param name="ui">����� UI</param>
        /// <exception cref="Exception">������ UI�� ���� �ΰ� �̻� ������ �� ȣ��</exception>
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
        /// UI �������̽� �˻�
        /// </summary>
        /// <typeparam name="T">���������� UI Ÿ��</typeparam>
        /// <returns>UI �������̽�</returns>
        /// <exception cref="Exception">���������� UI�� �������� ���� �� ȣ��</exception>
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
        /// ���� �� PopUp�� ���� ������ ���� �ڷ� ���� 
        /// </summary>
        /// <param name="ui">���� ������ PopUp</param>
        public void Push(IUIPopUp ui) {
            int sortingOrder = 1;
            //������ popUp�� �ϳ� �̻� �����Ѵٸ�
            if(_popUps.Last?.Value != null) {
                _popUps.Last.Value.inputActionEnable = false; // ���� popUp�� �Է�ó�� ��Ȱ��ȭ.
                sortingOrder = _popUps.Last.Value.sortingOrder + 1; // ���� popUp ���� ������ ����

                if (_popUps.Count > 50)
                    RearrangePopUpSortingOrders();
            }
            ui.inputActionEnable = true; //�� popUp�� �Է�ó�� Ȱ��ȭ.
            ui.sortingOrder = sortingOrder;
            _popUps.Remove(ui); //�� popUp�� ������ �����ϴ� �˾��̸� ����
            _popUps.AddLast(ui); //�� popUp�� ���� ���� �߰�

            if(_popUps.Count == 1) {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }

        /// <summary>
        /// PopUp �� ����, ���� PopUp�� �ִٸ� ���� PopUp�� �Է�ó�� Ȱ��ȭ
        /// </summary>
        /// <param name="ui">������ UI</param>
        public void Pop(IUIPopUp ui) {
            //�����Ϸ��� Popup�� �������̸鼭 ���� Popup�� �ִٸ�
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
        /// ���� ������ ��ġ���� �ٸ� ĵ������ RaycastTarget �� �ִ��� ����
        /// </summary>
        /// <param name="ui">���� ui</param>
        /// <param name="other">�ٸ� ĵ������ ui</param>
        /// <returns>���� ����</returns>
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