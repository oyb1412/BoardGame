using System;
using System.Reflection;

namespace DiceGame.Singleton
{
    public class SingletonBase<T>
        where T : SingletonBase<T>
    {
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)Activator.CreateInstance(typeof(T));
                }

                return _instance;
            }
        }

        private static T _instance;
    }
}