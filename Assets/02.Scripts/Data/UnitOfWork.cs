namespace DiceGame.Data {


    /// <summary>
    /// ���� ����� ����ҵ��� ������ ����
    /// </summary>
    public class UnitOfWork : IUnitOfWork {
        public UnitOfWork() {
            inventoryRepository = new InventoryRepository();
        }

        public IRepositoryOfT<InventorySlotDataModel> inventoryRepository { get; private set; }
    }
}