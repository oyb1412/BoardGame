using UnityEngine;

namespace DiceGame.UI {
    /// <summary>
    /// �˾��� UI�� �Ҵ�Ǵ� Ŭ����
    /// </summary>
    public class UIPopUpBase : UIBase, IUIPopUp {

        public override void InputAction() {
            base.InputAction();
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
                // ������ �ٸ� UI �� ��ȣ�ۿ��Ϸ��� �õ� �ߴٸ� 
                if (UIManager.instance.TryCastOther(this, out IUI other, out GameObject hovered)) {

                    //������ �ٸ� PopUp�� �����ߴٸ�, �ش� PopUp�� ���� �տ� �����ְ� ��
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