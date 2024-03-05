using System;
using System.Collections.Generic;

namespace DiceGame.Data {

    /// <summary>
    /// 저장소의 기본 상호작용
    /// </summary>
    /// <typeparam name="T">저장소에서 취급하는 데이터 형식</typeparam>
    public interface IRepositoryOfT<T> {
        /// <summary>
        /// 데이터가 변경되었을때 알림 통지 int번째 아이템이 T로 변경되었음
        /// </summary>
        event Action<int, T> onItemUpdated;

        /// <summary>
        /// 모든 데이터 순회용
        /// </summary>
        IEnumerable<T> GetAllItem();

        /// <summary>
        /// 데이터 검색
        /// </summary>
        /// <param name="id">검색할 데이터의 id</param>
        /// <returns>검색된 데이터</returns>
        T GetItemByID(int id);

        /// <summary>
        /// 데이터 삽입
        /// </summary>
        /// <param name="item">삽입할 데이터</param>
        void InsertItem(T item);

        /// <summary>
        /// 데이터 변경
        /// </summary>
        /// <param name="item">덮어씌울 아이템</param>
        /// <param name="id">변경할 아이템 id</param>
        void UpdateItem(T item, int id);

        /// <summary>
        /// 데이터 삭제
        /// </summary>
        /// <param name="item">삭제할 데이터</param>
        void DeleteItem(T item);

        /// <summary>
        /// 변동내역 db에 일괄 저장
        /// </summary>
        void Save();
    }
}