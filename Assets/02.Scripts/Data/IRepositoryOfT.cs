using System;
using System.Collections.Generic;

namespace DiceGame.Data {

    /// <summary>
    /// ������� �⺻ ��ȣ�ۿ�
    /// </summary>
    /// <typeparam name="T">����ҿ��� ����ϴ� ������ ����</typeparam>
    public interface IRepositoryOfT<T> {
        /// <summary>
        /// �����Ͱ� ����Ǿ����� �˸� ���� int��° �������� T�� ����Ǿ���
        /// </summary>
        event Action<int, T> onItemUpdated;

        /// <summary>
        /// ��� ������ ��ȸ��
        /// </summary>
        IEnumerable<T> GetAllItem();

        /// <summary>
        /// ������ �˻�
        /// </summary>
        /// <param name="id">�˻��� �������� id</param>
        /// <returns>�˻��� ������</returns>
        T GetItemByID(int id);

        /// <summary>
        /// ������ ����
        /// </summary>
        /// <param name="item">������ ������</param>
        void InsertItem(T item);

        /// <summary>
        /// ������ ����
        /// </summary>
        /// <param name="item">����� ������</param>
        /// <param name="id">������ ������ id</param>
        void UpdateItem(T item, int id);

        /// <summary>
        /// ������ ����
        /// </summary>
        /// <param name="item">������ ������</param>
        void DeleteItem(T item);

        /// <summary>
        /// �������� db�� �ϰ� ����
        /// </summary>
        void Save();
    }
}