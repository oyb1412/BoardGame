using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace DiceGame.UI {
    public interface IUI {
        
        /// <summary>
        /// Canvas의 sortOrder
        /// </summary>
        int sortingOrder { get; set; }
        /// <summary>
        /// InputAction 을 수행할지에 대한 여부
        /// </summary>
        bool inputActionEnable { get; set; }
        event Action onShow;
        event Action onHide;
        
        /// <summary>
        /// 이 UI와 유저가 상호작용 할 때 실행할 내용
        /// </summary>
        void InputAction();
        /// <summary>
        /// 이 UI를 보여줄 줌
        /// </summary>
        void Show();
        /// <summary>
        /// 이 UI를 숨김
        /// </summary>
        void Hide();
        /// <summary>
        /// 현재Canvas의 GraphicsRaycaster 모듈로 RaycastTarget을 감지함
        /// </summary>
        /// <param name="result">감지된 결과를 캐싱해 둘 리스트</param>
        void RayCast(List<RaycastResult> result);
    }
}
