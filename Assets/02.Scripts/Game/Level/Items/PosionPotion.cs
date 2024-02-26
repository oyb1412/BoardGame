using DiceGame.Character;

namespace DiceGame.Level.items {
    public class PosionPotion : Item {
        private float _damage = 10f;

        public override void Use(PlayerController controller) {
            controller.DepleteHp(_damage);
            Destroy(gameObject);
        }
    }
}