using DiceGame.Game;
using DiceGame.Game.Effects;
using DiceGame.Level;
using DiceGame.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DiceGame.Character
{
    public class PlayerController : MonoBehaviour, IHp
    {
        public static PlayerController instance;

        public const int DIRECTION_POSITIVE = 1;
        public const int DIRECTION_NEGATIVE = -1;
        // 1 : positive, -1 : negative
        public int direction { get; set; }
        
        public int nodeIndex { get; set; }
        public float hp => _hp;
        public float hpMin => _hpMin;
        public float hpMax => _hpMax;
        public State state
        {
            get => _state;
            set => _state = value;
        }      
        public IWeaponStrategy weaponStrateg {
            get => _weaponStrategy;
            set => _weaponStrategy = value;
        }

        public IHp target { get; set; }

        private float _hp;
        private float _hpMin = 0.0f;
        private float _hpMax = 100.0f;
        [SerializeField] float _moveSpeed = 1.0f;
        private float _attackPower = 10.0f;
        private Animator _animator;
        private State _state;

        private int _stateAnimatorHashID = Animator.StringToHash("State");
        private int _isDirtyAnimatorHashID = Animator.StringToHash("IsDirty");
        private int _SpeedAnimatorHashID = Animator.StringToHash("Speed");
        private int _WeaponAnimatorHashID = Animator.StringToHash("Weapon");
        private DamagePopUpFactory _damagePopUpFactory;
        private IWeaponStrategy _weaponStrategy;
        [SerializeField] Transform _rightHand;
        // event 한정자 : 외부 클래스에서는 이 대리자를 쓸 때 +=, -= 의 피연산자로만 사용가능
        public event Action<float> onHpDepleted;


        private void Awake()
        {
            instance = this;
            _hp = _hpMax;
            direction = DIRECTION_POSITIVE;
            _animator = GetComponent<Animator>();
            _damagePopUpFactory = new DamagePopUpFactory();

            var stateMachineBehaviours = _animator.GetBehaviours<StateMachineBehaviourBase>();
            for (int i = 0; i < stateMachineBehaviours.Length; i++)
            {
                stateMachineBehaviours[i].Init(this);
            }
        }
        private void Start() {
            if(_rightHand.childCount > 0) {
                weaponStrateg = _rightHand.GetChild(0).GetComponent<IWeaponStrategy>();
                Transform transForm = _rightHand.GetChild(0).transform;
                transForm.SetParent(_rightHand);
                transForm.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                _animator.SetInteger(_WeaponAnimatorHashID, (int)weaponStrateg.WeaponType);
            }
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.I)) {
               UIManager.instance.Get<UIIventory>().Toggle();
            }
            else if(Input.GetKeyDown(KeyCode.E)) {
               UIManager.instance.Get<UIEquipped>().Toggle();
            }
        }
        public void SetWeaponStrategy(IWeaponStrategy weapon, Transform transForm) {
            if(_rightHand.childCount > 0) {
                Destroy(_rightHand.GetChild(0).gameObject);
            }
            transForm.SetParent(_rightHand);
            transForm.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            weaponStrateg = weapon;
            _animator.SetInteger(_WeaponAnimatorHashID, (int)weaponStrateg.WeaponType);
        }

        public void DepleteHp(float value)
        {
            if (_hp <= _hpMin || value <= 0)
                return;

            _hp -= value;
            onHpDepleted?.Invoke(_hp);
        }

        /// <summary>
        /// 직접적인 이동 코루틴
        /// </summary>
        /// <param name="diceValue">이동할 거리</param>
        /// <returns></returns>
        public IEnumerator C_Move(int diceValue)
        {
            //주사위의 수 만큼 반복
            for (int i = 0; i < diceValue; i++)
            {
                //플레이어가 이동할 방향을 지정
                int nextIndex = nodeIndex + direction;
                //플레이어가 이동할 거리가 제일 첫번째 노드보다 작거나, 제일 마지막 노드보다 크면 중지
                if (nextIndex < 0 || nextIndex >= BoardGameMap.nodes.Count)
                    break;


                // 장애물 확인
                if (BoardGameMap.nodes[nextIndex].obstacle)
                {
                    yield return StartCoroutine(BoardGameMap.nodes[nextIndex].obstacle.C_Interaction(this)); // 장애물과 상호작용 루틴 시작
                }
                else
                {
                    float t = 0.0f;
                    while (t < 1.0f)
                    {
                        _animator.SetFloat(_SpeedAnimatorHashID, 1f);
                        //현재 노드에서 다음 노드로 이동
                        transform.position = Vector3.Lerp(BoardGameMap.nodes[nodeIndex].transform.position,
                                                          BoardGameMap.nodes[nextIndex].transform.position,
                                                          t);
                        t += _moveSpeed * Time.deltaTime;
                        yield return null;
                    }
                    _animator.SetFloat(_SpeedAnimatorHashID, 0f);

                    //이동이 끝나면 노드 번호 변경
                    nodeIndex = nextIndex;

                    
                }
            }

        }

        public void ChangeState(State newState)
        {
            _animator.SetInteger(_stateAnimatorHashID, (int)newState);
            _animator.SetBool(_isDirtyAnimatorHashID, true);
        }

        private void Hit(AnimationEvent e)
        {
            if (target != null)
            {
                weaponStrateg.Attack(target,_attackPower, out float amountDamaged);
                _damagePopUpFactory.Create(transform.position + Vector3.forward * 1.0f + Vector3.up * 1.2f,
                Quaternion.identity,
                amountDamaged,
                DamageType.Normal);
            }
        }

        [SerializeField] AudioSource _footStepAudioSource;
        private void FootL()
        {
            if (_footStepAudioSource.isPlaying)
                return;

            _footStepAudioSource.Play();
        }
        private void FootR()
        {
            if (_footStepAudioSource.isPlaying)
                return;

            _footStepAudioSource.Play();
        }
    }
}