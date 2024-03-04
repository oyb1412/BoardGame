using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DiceGame.Data;
public class LoginUI : MonoBehaviour
{
    [SerializeField] TMP_InputField _id;
    [SerializeField] TMP_InputField _pw;
    [SerializeField] Button _tryLogin;

    private void Start() {
        _tryLogin.onClick.AddListener(() => {
            if (string.IsNullOrEmpty(_id.text))
                return;

            if (string.IsNullOrEmpty(_pw.text))
                return;

            LoginInfomation.TryLogin(_id.text, _pw.text);

        });
    }

 
}
