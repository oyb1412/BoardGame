namespace DiceGame.Data {

    /// <summary>
    /// ��� Repository ������ �������ִ� ����
    /// </summary>
    public interface IUnitOfWork {
        /// <summary>
        /// InventoryRepository ���� (Inventory UI ��� �κ��丮 �����Ϳ� ���� ���������� �ʿ��Ҷ� ������ ��������)
        /// </summary>
        IRepositoryOfT<InventorySlotDataModel> inventoryRepository { get; }
    }
}