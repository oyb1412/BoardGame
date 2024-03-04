using System;

namespace DiceGame.Data {
    [Serializable]
    public class ProfileDataModel {
        public bool isValid => string.IsNullOrEmpty(id) == false &&
            string.IsNullOrEmpty(id) == false;

        public string id;
        public string pw;
        public string nickname;
    }
}