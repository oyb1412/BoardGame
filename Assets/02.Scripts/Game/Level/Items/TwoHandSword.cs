using DiceGame.Character;
using DiceGame.Game.Effects;
using DiceGame.Game;
using UnityEngine;


namespace DiceGame.Level.items {
    public class TwoHandSword : ItemEquipment, IWeaponStrategy {
        public void Attack(IHp target, float attackPower) {
            var _damagePopUpFactory = new DamagePopUpFactory();
            _damagePopUpFactory.Create(transform.position + Vector3.forward * 1.0f + Vector3.up * 1.2f,
                            Quaternion.identity,
                            attackPower,
                            DamageType.Normal);
            target.DepleteHp(attackPower);

        }

        public override void Use(PlayerController controller) {
            controller.EquipWeapon(this, renderModel);
        }
    }
}