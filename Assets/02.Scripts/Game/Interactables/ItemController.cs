using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DiceGame.Game.Interactables {
    public class ItemController : MonoBehaviour {
        [field : SerializeField] public int itemID { get; private set; }
        [field : SerializeField] public int itemNum { get; private set; }

        public void Interaction() {
            PickUp();
        }

        private void PickUp() {
            var inventoryRepository = GameManager.instance.unitOfWork.inventoryRepository;

            //저장소를 전체 순환하면서
            //현재 id의 아이템을 차례대로 iteminfo.nummax까지 채우는 것을 시도하면서 모든 itemnum을 가능한만큼 소모하고
            //남은 개수가 있으면 db를 갱신한 후에 itemnum 남은갯수로 갱신

            if (itemNum == 0)
                Destroy(gameObject);
        }
    }
}
