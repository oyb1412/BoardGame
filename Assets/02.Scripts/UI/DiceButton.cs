using DiceGame.Game;
using UnityEngine;
using UnityEngine.UI;

namespace DiceGame.UI
{
    public class DiceButton : MonoBehaviour
    {
        private Button _button;


        private void Start()
        {
            _button = GetComponent<Button>();

            //현재 버튼이 클릭되었을때 실행될 함수를 저장하는 기능(에디터의 OnClick기능과 동일)
            _button.onClick.AddListener(() =>
            {
                DicePlayManager.instance.RollADice();
            });

            //주사위의 갯수가 변경되었을때의 이벤트 연결 (diceValue = 주사위의 갯수)
            DicePlayManager.instance.onDiceNumberChanged += (diceValue) =>
            {
                //아직 주사위의 갯수가 0개 이상이면 버튼을 활성화
                if (diceValue > 0)
                    ActiveButton();
                //주사위의 갯수가 0개 이하라면 버튼을 비활성화
                else
                    DeactiveButton();
            };
        }

        private void ActiveButton()
        {
            _button.interactable = true;
        }

        private void DeactiveButton()
        {
            _button.interactable = false;
        }
    }
}