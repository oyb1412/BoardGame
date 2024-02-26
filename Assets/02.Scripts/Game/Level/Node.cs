using DiceGame.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiceGame.Level.items;

namespace DiceGame.Level
{
    public abstract class Node : MonoBehaviour, INode, IComparable<Node>
    {
        public int nodeIndex => _nodeIndex;
        public Obstacle obstacle
        {
            get => _obstacle;
            set => _obstacle = value;
        }   
        public Item item
        {
            get => _item;
            set => _item = value;
        }


        [SerializeField] private int _nodeIndex;
        [SerializeField] private Obstacle _obstacle;
        [SerializeField] private Item _item;


        private void Awake()
        {
            //맵에 노드정보를 저장
            BoardGameMap.Register(this);
            
            if (_obstacle)
                _obstacle.node = this;
        }

        //플레이어가 도착하면 기본적으로 이동 방향을 전방으로 변경
        public virtual void OnPlayerHere()
        {
            PlayerController.instance.direction = PlayerController.DIRECTION_POSITIVE;
            item?.Use(PlayerController.instance);
        }

        public virtual void OnDiceRolled(int diceValue)
        {
        }

        public int CompareTo(Node other)
        {
            if (_nodeIndex < other._nodeIndex)
                return -1;
            else if (_nodeIndex > other._nodeIndex)
                return 1;

            return 0;
        }
    }
}
