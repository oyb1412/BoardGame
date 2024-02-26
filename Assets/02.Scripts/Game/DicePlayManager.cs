using DiceGame.UI;
using DiceGame.Character;
using System;
using System.Collections;
using DiceGame.Level;
using DiceGame.Singleton;

namespace DiceGame.Game
{
    public class DicePlayManager : SingletonMonoBase<DicePlayManager>
    {
        //_diceNumber의 값을 변경하기 위한 public프로퍼티
        public int diceNumber
        {
            get => _diceNumber;
            set
            {
                //기존 주사위 갯수와 변경하려는 갯수가 같으면 리턴
                if (_diceNumber == value)
                    return;

                //갯수를 변경
                _diceNumber = value;
                //주사위 변경 이벤트를 호출
                onDiceNumberChanged?.Invoke(_diceNumber);
            }
        }

        //주사위의 갯수
        private int _diceNumber = 3;
        //현재 RollADice코루틴이 진행중인지 판단하는 트리거
        private bool _isCorouting;
        //주사위의 갯수가 바뀔때마다 호출되는 이벤트
        public event Action<int> onDiceNumberChanged;
        public event Action onRollingDiceStarted;
        public event Action onRollingDiceFinished;


        public void RollADice()
        {
            //이미 코루틴이 실행중이면 리턴
            if (_isCorouting)
                return;

            //주사위의 갯수를 차감
            diceNumber--;
            onRollingDiceStarted?.Invoke();
            //주사위의 값을 랜덤으로 지정
            int diceValue =  UnityEngine.Random.Range(1, 7);
            //코루틴 트리거를 실행중으로 변경(이 트리거는 항상 코루틴 내부가 아닌, 코루틴 실행 전에 바꿔주는것이 좋다)
            _isCorouting = true;
            StartCoroutine(C_RollADice(diceValue));
        }

        IEnumerator C_RollADice(int diceValue)
        {
            //C_Animation코루틴이 끝나면 다음 행으로 넘어감
            yield return StartCoroutine(DiceRollingAnimationUI.instance.C_Animation(diceValue));
            //플레이어가 위치한 노드에서 플레이어가 주사위를 굴림
            BoardGameMap.nodes[PlayerController.instance.nodeIndex].OnDiceRolled(diceValue);
            //직접적인 플레이어 이동 코루틴이 끝나면 다음 행으로 넘어감
            yield return StartCoroutine(PlayerController.instance.C_Move(diceValue));
            //플레이어가 도착한 노드
            BoardGameMap.nodes[PlayerController.instance.nodeIndex].OnPlayerHere();
            onRollingDiceFinished?.Invoke();
            //코루틴 트리거를 실행종료로 변경
            _isCorouting = false;
        }
    }
}