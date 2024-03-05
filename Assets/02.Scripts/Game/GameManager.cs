using DiceGame.Data;
using DiceGame.Data.Mock;
using DiceGame.Singleton;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal.ShaderGraph;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

namespace DiceGame.Game {

    public enum GameState {
        None,
        Login,
        WaitUntilLoggedIn,
        LoadResources,
        WaitUntilResourcesLoaded,
        InGame,
    }
    public class GameManager : SingletonMonoBase<GameManager> {

        [field: SerializeField] public bool isTesting { get; private set; }
        public IUnitOfWork unitOfWork { get; private set; }

        [SerializeField] private GameState _state;
        public GameState state {
            get {
                return _state;
            }
            set {
                if (value == _state)
                    return;

                _state = value;
            }
        }
        protected override void Awake() {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
        private void Update() {
            Workflow();
        }
        private void Workflow() {
            switch (_state) {
                case GameState.None:
                    break;
                case GameState.Login: {
                        SceneManager.LoadScene("Login");
                        _state++;
                    }
                    break;
                case GameState.WaitUntilLoggedIn: {
                        if (LoginInfomation.isLoggedIn)
                            _state++;
                    }
                    break;
                case GameState.LoadResources:
                    if (isTesting)
                        unitOfWork = new MockUnitOfWork();
                    else
                        unitOfWork = new UnitOfWork();
                    state++;
                    break;
                case GameState.WaitUntilResourcesLoaded:
                    SceneManager.LoadScene("DicePlay");
                    state++;
                    break;
                case GameState.InGame:

                    break;
                default:
                    break;
            }
        }
    }
}
