using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardGamePlayManager : MonoBehaviour
{
    //각 노드의 트랜스폼
    [SerializeField] Transform[] _nodes;
    //플레이어의 트랜스폼
    [SerializeField] Transform _player;
    //이동속도
    [SerializeField] float _moveSpeed = 1.0f;
    //현재 플레이어가 위치한 노드의 번호
    private int _playerLocationIndex;

    [SerializeField] BoardGamePlayStatusUI _statusUI;

    private void Start()
    {
        RollADice();
    }

    /// <summary>
    /// 주사위를 굴린다
    /// </summary>
    public void RollADice()
    {
        //주사위의 눈금은 랜덤하게 결정
        int value = Random.Range(1, 7);
        //주사위가 돌아가는 애니메이션이 실행 뒤, 애니메이션이 끝나면 DoMove함수를 호출한다.
        _statusUI.PlayRollingAnimation(value, DoMove);
    }

    private void DoMove(int value)
    {
        StartCoroutine(C_Move(_playerLocationIndex, value));
        _playerLocationIndex = _playerLocationIndex + value <= _nodes.Length - 1 ? _playerLocationIndex + value : _nodes.Length - 1;
    }

    /// <summary>
    /// 이동을 위한 코루틴
    /// </summary>
    /// <param name="currentIndex">현재 위치한 노드의 번호</param>
    /// <param name="value">이동할 노드의 수</param>
    /// <returns></returns>
    IEnumerator C_Move(int currentIndex, int value)
    {
        //이동할 노드의 수 만큼 반복
        for (int i = 0; i < value; i++)
        {
            //도중에 마지막 노드에 도착하면 중단
            if (currentIndex + 1 >= _nodes.Length)
                break;

            float t = 0.0f;
            while (t < 1.0f)
            {
                //다음 노드로 t초에 걸쳐 이동
                _player.position = Vector3.Lerp(_nodes[currentIndex].position, _nodes[currentIndex + 1].position, t);
                t += _moveSpeed * Time.deltaTime;
                yield return null;
            }
            //이동 위치 정확하게 고정
            _player.position = _nodes[currentIndex + 1].position;
            //이동이 끝났으니 현재 위치 번호도 변경
            currentIndex++;
            //한 칸 이동이 끝나면 0.5초 후에 다음 칸으로 이동
            yield return new WaitForSeconds(0.5f);
        }
    }
}
