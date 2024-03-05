using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DiceGame.Data {
    public static class LoginInfomation {
        public static ProfileDataModel profile { get; private set; }
        public static bool isLoggedIn => profile == null ? false : profile.isValid;
        public static bool isTesting = true;

        
        public static bool TryLogin(string id, string pw) {
            if(isTesting) {
                profile = new ProfileDataModel() { id = "tester", pw = "0000", nickname = "" };
            }

            if (isLoggedIn) {
                Debug.Log("[LoginInformation] : Logged in with" + profile.id);
                return true;

            } else {
                Debug.Log("[LoginInformation] : Failed to Login with" + profile.id);
                return false;
            }

        }
    }
}
