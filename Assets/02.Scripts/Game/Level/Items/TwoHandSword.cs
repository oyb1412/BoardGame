using DiceGame.Character;
using DiceGame.Game.Effects;
using DiceGame.Game;
using UnityEngine;


namespace DiceGame.Level.items {
    public class TwoHandSword : ItemEquipment, IWeaponStrategy {
        public WeaponType WeaponType => WeaponType.TwoHandedSword;
        [SerializeField] float _damageGain = 1.5f;

        public void Attack(IHp target, float damage, out float amountDamaged) {

            amountDamaged = damage * _damageGain;
            target.DepleteHp(damage * _damageGain);
        }


        public override void Use(PlayerController controller) {
            controller.SetWeaponStrategy(this,transform);
        }
    }
}