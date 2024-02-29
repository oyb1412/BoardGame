using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DiceGame.UI {
    /// <summary>
    /// Canvas 기본 단위 컴포넌트
    /// 각 캔버스에 할당됨
    /// </summary>
    public abstract class UIBase : MonoBehaviour, IUI {
        public int sortingOrder {
            get => canvas.sortingOrder;
            set => canvas.sortingOrder = value;
        }
        public bool inputActionEnable { get ; set ; }
        public bool isShowed => canvas.enabled;
        protected Canvas canvas;
        protected GraphicRaycaster raycastModule;

        public event Action onShow;
        public event Action onHide;

        protected virtual void Awake() {
            canvas = GetComponent<Canvas>();
            raycastModule = GetComponent<GraphicRaycaster>();
            UIManager.instance.Register(this);
        }

        public virtual void InputAction() {
            
        }
        public virtual void Show() {
            if (canvas.enabled == false) {
                canvas.enabled = true;
                onShow?.Invoke();
            }
        }
        public virtual void Hide() {
            if (canvas.enabled == true) {
                canvas.enabled = false;
                onHide?.Invoke();
            }
        }
        public void Toggle() {
            if (isShowed)
                Hide();
            else
                Show();
        }
        /// <summary>
        /// 마우스 포인터가 이 오브젝트 위에 존재하는가 체크
        /// </summary>
        /// <param name="result"></param>
        public void RayCast(List<RaycastResult> result) {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            raycastModule.Raycast(pointerEventData, result);
        }


    }
}