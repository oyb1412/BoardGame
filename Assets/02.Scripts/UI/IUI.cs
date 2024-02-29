using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace DiceGame.UI {
    public interface IUI {
        
        /// <summary>
        /// Canvas�� sortOrder
        /// </summary>
        int sortingOrder { get; set; }
        /// <summary>
        /// InputAction �� ���������� ���� ����
        /// </summary>
        bool inputActionEnable { get; set; }
        event Action onShow;
        event Action onHide;
        
        /// <summary>
        /// �� UI�� ������ ��ȣ�ۿ� �� �� ������ ����
        /// </summary>
        void InputAction();
        /// <summary>
        /// �� UI�� ������ ��
        /// </summary>
        void Show();
        /// <summary>
        /// �� UI�� ����
        /// </summary>
        void Hide();
        /// <summary>
        /// ����Canvas�� GraphicsRaycaster ���� RaycastTarget�� ������
        /// </summary>
        /// <param name="result">������ ����� ĳ���� �� ����Ʈ</param>
        void RayCast(List<RaycastResult> result);
    }
}
