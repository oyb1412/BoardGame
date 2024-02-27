using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoginUI : MonoBehaviour
{
    [SerializeField] TMP_InputField _id;
    [SerializeField] TMP_InputField _pw;
    [SerializeField] Button _login;

    private void Start() {
        _login.onClick.AddListener(() => {
            if (string.IsNullOrEmpty(_id.text))
                return;

            if (string.IsNullOrEmpty(_pw.text))
                return;

            Login();
        });
    }

    private void Login() {
        SceneManager.LoadScene("DiceGame");
    }
}
