using UnityEngine;

namespace DiceGame.Singleton
{
    public class SingletonMonoBase<T> : MonoBehaviour
        where T : SingletonMonoBase<T>
    {
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }

                return _instance;
            }
        }

        private static T _instance;

        virtual protected void Awake() {
            if(_instance != null) {
                Destroy(gameObject);
                return;
            }

            _instance = (T)this;
        }
    }
}