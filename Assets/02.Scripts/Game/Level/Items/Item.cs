using DiceGame.Character;
using UnityEngine;

namespace DiceGame.Level.items {
    public abstract class Item : MonoBehaviour {
        public Node node { get; set; }

        public abstract void Use(PlayerController controller);
    }
}
