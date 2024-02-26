namespace DiceGame.Level
{
    public interface INode
    {
        //플레이어가 이 노드에 도착
        void OnPlayerHere();
        //플레이어가 이 노드에서 주사위를 굴림
        void OnDiceRolled(int diceValue);
    }
}