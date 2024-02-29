using UnityEngine;

namespace DiceGame.UI {
    /// <summary>
    /// 팝업용 UI에 할당되는 클래스
    /// </summary>
    public class UIPopUpBase : UIBase, IUIPopUp {

        public override void InputAction() {
            base.InputAction();
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
                // 유저가 다른 UI 와 상호작용하려고 시도 했다면 
                if (UIManager.instance.TryCastOther(this, out IUI other, out GameObject hovered)) {

                    //유저가 다른 PopUp을 선택했다면, 해당 PopUp을 가장 앞에 보여주게 함
                    if (other is IUIPopUp)
                        other.Show();
                }
            }
        }

        public override void Show() {
            base.Show();
            UIManager.instance.Push(this);
        }

        public override void Hide() {
            base.Hide();
            UIManager.instance.Pop(this);
        }


    }
}