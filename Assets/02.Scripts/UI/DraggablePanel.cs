using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace DiceGame.UI {
    public class DraggablePanel : MonoBehaviour, IDragHandler {
        private RectTransform _rectTransform;
        [SerializeField] bool _resetPositionOnEnable = true;
        private Vector2 _originPosition;
        private void Awake() {
            _rectTransform = GetComponent<RectTransform>();
            _originPosition = transform.localPosition;
            if(transform.root.TryGetComponent(out IUI ui))
                transform.root.GetComponent<IUI>().onShow += ResetPosition;
        }
        public void OnDrag(PointerEventData eventData) {
            _rectTransform.anchoredPosition += eventData.delta;
        }

        public void ResetPosition() {
            if (_resetPositionOnEnable)
                _rectTransform.anchoredPosition = _originPosition;
        }

    }
}
