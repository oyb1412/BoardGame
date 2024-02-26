using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class BoardGamePlayStatusUI : MonoBehaviour
{ 
    [SerializeField] GameObject[] _numberImages;
    [SerializeField] float _rollingAnimationDuration;
    private int _currentImageIndex;


    public void PlayRollingAnimation(int value, Action<int> onFinished)
    {
        StartCoroutine(C_RollingAnimation(value, onFinished));
    }

    IEnumerator C_RollingAnimation(int value, Action<int> onFinished)
    {
        int count = 0;
        float elapsedTime = 0.0f;

        while (elapsedTime < _rollingAnimationDuration)
        {
            //반복문과 랜덤함수를 통해 랜덤한 이미지의 활성화, 비활성화를 반복한다.
            _numberImages[_currentImageIndex].SetActive(false);
            _currentImageIndex = Random.Range(0, _numberImages.Length);
            _numberImages[_currentImageIndex].SetActive(true);
            count++;
            elapsedTime += Time.deltaTime * count;
            //시간이 지날수록 count가 곱해지기 때문에, 이미지 변경은 점점 느려진다.
            yield return new WaitForSeconds(Time.deltaTime * count);
        }

        //애니메이션이 종료되면 정해진 값의 이미지를 강제로 활성화시킨다.
        _numberImages[_currentImageIndex].SetActive(false);
        _currentImageIndex = value - 1;
        _numberImages[_currentImageIndex].SetActive(true);

        //onFineshed에 전달된 함수(실제 이동)를 실행한다.
        onFinished?.Invoke(value);
    }
}