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

            //����Ҹ� ��ü ��ȯ�ϸ鼭
            //���� id�� �������� ���ʴ�� iteminfo.nummax���� ä��� ���� �õ��ϸ鼭 ��� itemnum�� �����Ѹ�ŭ �Ҹ��ϰ�
            //���� ������ ������ db�� ������ �Ŀ� itemnum ���������� ����

            if (itemNum == 0)
                Destroy(gameObject);
        }
    }
}
