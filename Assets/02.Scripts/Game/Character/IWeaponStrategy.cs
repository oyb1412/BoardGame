namespace DiceGame.Character {
    public interface IWeaponStrategy {
        void Attack(IHp target, float attackPower);
    }
}