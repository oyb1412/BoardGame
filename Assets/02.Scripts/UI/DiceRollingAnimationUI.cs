using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DiceGame.UI
{
    public class DiceRollingAnimationUI : MonoBehaviour
    {
        public static DiceRollingAnimationUI instance;

        //주사위의 숫자(1~6)을 표기할 스프라이트
        [SerializeField] Sprite[] _diceSprites;
        //주사위가 회전하는 애니메이션 출력용 스프라이트
        [SerializeField] Sprite[] _diceRollingSprites;
        //주사위 스프라이트 표시 용 이미지
        private Image _diceImage;
        //각 애니메이션의 전환 시간
        [SerializeField] float _animationDuration;
        //애니메이션 실행 시간
        [SerializeField] float _animationSpeed;

        [SerializeField] float _animationDampingGain;
        //애니메이션을 실행시킬 시간
        [SerializeField] float _animationFinishDelay;


        private void Awake()
        {
            instance = this;

            _diceImage = transform.Find("Image - Dice").GetComponent<Image>();
        }

        public IEnumerator C_Animation(int diceValue)
        {
            //애니메이션 전환속도 조절용 필드
            float dampingRatio = 1.0f;
            //다음 애니메이션으로 전환을 체크하기 위한 필드
            float elapsedTime = 0.0f;

            //다음 애니메이션으로의 전환 시간만큼 애니메이션 실행
            while (elapsedTime < _animationDuration)
            {
                //스프라이트 애니메이션 실행(이미지 변경)
                _diceImage.sprite = _diceRollingSprites[Random.Range(0, _diceRollingSprites.Length)];
                //애니메이션 전환속도를 증가시키기 위한 연산
                dampingRatio *= (1.0f + _animationDampingGain);
                //애니메이션이 바뀌는 속도는 점점 증가한다.
                elapsedTime += Time.deltaTime * dampingRatio / _animationSpeed;
                //return타이밍이 시간이 지남에 따라 점점 빨라진다.
                yield return new WaitForSeconds(Time.deltaTime * dampingRatio / _animationSpeed);
            }
            //애니메이션이 끝났으므로, 이미지를 내가 뽑은 주사위의 눈금으로 변경
            _diceImage.sprite = _diceSprites[diceValue - 1];
            //모든 애니메이션의 최종 종료 시간이 지나면 코루틴 종료
            yield return new WaitForSeconds(_animationFinishDelay);
            //finishDelay만큼의 시간이 지난 후 한 프레임 이후 다음 행동을 해야하면 null을 적어준다
            yield return null;
        }
    }
}
