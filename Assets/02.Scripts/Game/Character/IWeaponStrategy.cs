using DiceGame.Game;
using DiceGame.Game.Effects;

namespace DiceGame.Character {
    public interface IWeaponStrategy {
        void Attack(IHp target, float damage , out float amoutDamaged);
        public WeaponType WeaponType { get;  }

    }
}