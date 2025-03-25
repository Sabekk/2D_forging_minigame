using Gameplay.Resources;

namespace UI.Window.Inventory
{
    public class ResourceSlot : SlotBase
    {
        #region ACTIONS

        #endregion

        #region VARIABLES

        private ResourceInGame resource;

        #endregion

        #region PROPERTIES
        public ResourceInGame Resource => resource;

        #endregion

        #region METHODS

        public void SetItem(ResourceInGame resource)
        {
            this.resource = resource;
            presentId = resource != null ? resource.ResourceData.Id : -1;
            RefreshItemInSlot();
            RefreshCountInSlot();
            SetSelected(false);
            AttachEvents();
        }

        public override void RemoveItem()
        {
            base.RemoveItem();

            resource = null;
            RefreshItemInSlot();
        }

        public override void RefreshCountInSlot()
        {
            if (resource == null)
                countOfItems.gameObject.SetActive(false);
            else
            {
                countOfItems.gameObject.SetActive(true);
                countOfItems.SetText(resource.CurrentValue.ToString());
            }
        }

        public override void AttachEvents()
        {
            base.AttachEvents();
            if (resource != null)
            {
                resource.OnValueChanged += RefreshCountInSlot;
            }
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            if (resource != null)
            {
                resource.OnValueChanged -= RefreshCountInSlot;
            }
        }

        protected override void SetIcon()
        {
            if (HasItem)
            {
                itemIcon.gameObject.SetActiveOptimize(true);
                itemIcon.sprite = resource.ResourceData.Icon;
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