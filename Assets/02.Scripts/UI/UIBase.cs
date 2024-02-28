using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DiceGame.UI {
    /// <summary>
    /// Canvas �⺻ ���� ������Ʈ
    /// </summary>
    public abstract class UIBase : MonoBehaviour, IUI {
        public int sortingOrder {
            get => canvas.sortingOrder;
            set => canvas.sortingOrder = value;
        }
        public bool inputActionEnable { get ; set ; }

        protected Canvas canvas;
        protected GraphicRaycaster raycastModule;

        protected virtual void Awake() {
            canvas = GetComponent<Canvas>();
            raycastModule = GetComponent<GraphicRaycaster>();
            UIManager.instance.Register(this);
        }

        public virtual void InputAction() {
            
        }
        public virtual void Show() {
            canvas.enabled = true;
        }
        public virtual void Hide() {
            canvas.enabled = false;
        }

        public void RayCast(List<RaycastResult> result) {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            raycastModule.Raycast(pointerEventData, result);
        }


    }
}