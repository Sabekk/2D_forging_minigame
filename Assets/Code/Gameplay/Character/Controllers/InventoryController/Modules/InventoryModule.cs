using Gameplay.Character.Controller.Module;
using Gameplay.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Controller.Inventory
{
    public class InventoryModule : ControllerModuleBase
    {
        #region VARIABLES

        [SerializeField] private List<ItemStackInGame> itemStacks;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public void CollectItem(ItemInGame item, int count = 1)
        {
            ItemStackInGame stack = itemStacks.GetElementById(item.ItemData.Id);
            if (stack == null)
                stack = new ItemStackInGame(item, count);
            else
                stack.AddItemToStack(item);
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
        }

        public bool CanHandleItemsInStack(ItemInGame item, int count = 1)
        {
            ItemStackInGame stack = itemStacks.GetElementById(item.ItemData.Id);
            if (stack == null)
                return false;

            return stack.CountInStack >= count;
        }

        #endregion
    }
}
