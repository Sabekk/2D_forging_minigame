using Gameplay.Character.Controller.Module;
using Gameplay.Items;
using Gameplay.Management.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Controller.Inventory
{
    public class InventoryModule : ControllerModuleBase
    {
        #region ACTIONS

        public event Action<ItemInGame> OnItemCollected;
        public event Action<ItemInGame> OnItemRemoved;
        public event Action<ItemStackInGame> OnStackCreated;
        public event Action<ItemStackInGame> OnStackRemoved;

        #endregion

        #region VARIABLES

        [SerializeField] private List<ItemStackInGame> itemStacks;

        #endregion

        #region PROPERTIES

        public List<ItemStackInGame> ItemStacks => itemStacks ??= new();

        #endregion

        #region METHODS

        public void SetStartingItems()
        {
            if (ItemsManager.Instance)
                foreach (var startingSecialItem in Character.Data.StartingItems.SpecialItems)
                {
                    if (UnityEngine.Random.Range(0f, 100f) <= startingSecialItem.ChanceToGet)
                        ItemsManager.Instance.AddItemToCharacter(startingSecialItem.ItemDataId, Character, startingSecialItem.Count);
                }
        }

        public void CollectItem(ItemInGame item, int count = 1)
        {
            ItemStackInGame stack = itemStacks.GetElementById(item.ItemData.Id);
            if (stack == null)
            {
                stack = new ItemStackInGame(item, count);
                ItemStacks.Add(stack);
                OnStackCreated?.Invoke(stack);
            }
            else
                stack.AddItemToStack(item);

            OnItemCollected?.Invoke(item);
        }

        public void RemoveItem(int itemDataId, int count = 1)
        {
            ItemStackInGame stack = itemStacks.Find(x => x.Item.ItemData.IdEquals(itemDataId));
            if (stack == null)
                return;

            RemoveItem(stack.Item, count);
        }

        public void RemoveItem(ItemInGame item, int count = 1)
        {
            ItemStackInGame stack = itemStacks.GetElementById(item.ItemData.Id);
            if (stack == null)
            {
                Debug.LogError($"Stack of item {item.ItemData.DataName} didn't fount. Check Inventory module");
            }
            else
                stack.RemoveItemFromStack(count);

            if (stack.ShouldBeRemoved)
            {
                ItemStacks.Remove(stack);
                OnStackRemoved?.Invoke(stack);
            }

            OnItemRemoved?.Invoke(item);
        }

        public bool CanHandleItemsInStack(ItemInGame item, int count = 1)
        {
            ItemStackInGame stack = ItemStacks.GetElementById(item.ItemData.Id);
            if (stack == null)
                return false;

            return stack.CountInStack >= count;
        }

        #endregion
    }
}
