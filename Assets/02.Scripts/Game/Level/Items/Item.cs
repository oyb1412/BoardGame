using DiceGame.Character;
using DiceGame.Game;
using UnityEngine;

namespace DiceGame.Level.items {
    public abstract class Item : MonoBehaviour {
        public abstract void Use(PlayerController controller);
    }
}
