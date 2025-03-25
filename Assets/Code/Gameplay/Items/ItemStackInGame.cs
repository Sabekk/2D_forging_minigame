using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    public class ItemStackInGame : IIdEqualable
    {
        #region ACTIONS

        public event Action OnCountInStackChanged;

        #endregion

        #region VARIABLES

        [SerializeField] private ItemInGame item;
        [SerializeField] private int countInStack;

        #endregion

        #region PROPERTIES

        public int Id => Item.ItemData.Id;
        public ItemInGame Item => item;
        public int CountInStack => countInStack;
        public bool ShouldBeRemoved => CountInStack <= 0;


        #endregion

        #region CONSTRUCTORS

        public ItemStackInGame() { }
        public ItemStackInGame(ItemInGame item, int count)
        {
            this.item = item;
            countInStack = count;
        }

        #endregion

        #region METHODS

        public void AddItemToStack(ItemInGame item, int count = 1)
        {
            if (this.item.ItemData.IdEquals(item.ItemData.Id))
            {
                countInStack += count;
                OnCountInStackChanged?.Invoke();
            }
        }

        public void RemoveItemFromStack(int count = 1)
        {
            if (this.item.ItemData.IdEquals(item.ItemData.Id))
            {
                countInStack -= count;
                OnCountInStackChanged?.Invoke();
            }
        }

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion
    }
}