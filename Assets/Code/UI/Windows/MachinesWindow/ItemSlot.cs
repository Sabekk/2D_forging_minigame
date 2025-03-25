using Gameplay.Items;

namespace UI.Window.Inventory
{
    public class ItemSlot : SlotBase
    {
        #region ACTIONS

        #endregion

        #region VARIABLES

        private ItemStackInGame itemStack;

        #endregion

        #region PROPERTIES
        public ItemStackInGame ItemStack => itemStack;

        #endregion

        #region METHODS

        public void SetItem(ItemStackInGame itemStack)
        {
            this.itemStack = itemStack;
            presentId = itemStack != null ? itemStack.Id : -1;
            RefreshItemInSlot();
            RefreshCountInSlot();
            SetSelected(false);
        }

        public override void RemoveItem()
        {
            itemStack = null;
            RefreshItemInSlot();
        }

        public override void RefreshCountInSlot()
        {
            if (itemStack == null)
                countOfItems.gameObject.SetActive(false);
            else
            {
                countOfItems.gameObject.SetActive(true);
                countOfItems.SetText(itemStack.CountInStack.ToString());
            }
        }

        protected override void SetIcon()
        {
            if (HasItem)
            {
                itemIcon.gameObject.SetActiveOptimize(true);
                itemIcon.sprite = itemStack.Item.ItemData.Icon;
            }
            else
            {
                itemIcon.sprite = null;
                itemIcon.gameObject.SetActiveOptimize(false);
            }
        }

        protected void RefreshItemInSlot()
        {
            SetIcon();
        }

        #region HANDLERS

        #endregion

        #endregion
    }
}