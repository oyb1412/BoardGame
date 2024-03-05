using DiceGame.Data.Mock;

namespace DiceGame.Data {
    public class MockUnitOfWork : IUnitOfWork {
        public MockUnitOfWork() {
            inventoryRepository = new MockInventoryRepository();
        }

        public IRepositoryOfT<InventorySlotDataModel> inventoryRepository { get; private set; }
    }
}