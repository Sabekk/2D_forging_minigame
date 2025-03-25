using Gameplay.Character;
using Gameplay.Items;
using Gameplay.Management.Characters;
using Gameplay.Management.Resources;
using Gameplay.Management.UI;
using Gameplay.Resources;
using ObjectPooling;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UI.Window.Inventory;
using UnityEngine;

namespace UI.Window.Machines
{
    public class MachinesWindow_Inventory : UIWindowNested
    {
        #region VARIABLES

        [SerializeField, ValueDropdown(ObjectPoolDatabase.GET_POOL_UI_WINDOW_METHOD)] private int itemSlotPrefabId;
        [SerializeField, ValueDropdown(ObjectPoolDatabase.GET_POOL_UI_WINDOW_METHOD)] private int resourceSlotPrefabId;

        [SerializeField] private Transform resourcesContainer;
        [SerializeField] private Transform itemsContainer;
        [SerializeField] private int defaultItemsSlots = 10;

        //TODO Add to character inventory count slots value
        private List<ItemSlot> itemSlots;
        private List<ResourceSlot> resourceSlots;

        #endregion

        #region PROPERTIES

        private PlayerInGame Player => CharacterManager.Instance.Player;

        #endregion

        #region METHODS

        public override void Initialize()
        {
            base.Initialize();
            itemSlots = new();
            resourceSlots = new();
            InitialRefreshSlot();
        }

        public override void CleanUp()
        {
            for (int i = itemSlots.Count - 1; i >= 0; i--)
            {
                itemSlots[i].CleanUp();
                ObjectPool.Instance.ReturnToPool(itemSlots[i]);
            }

            for (int i = resourceSlots.Count - 1; i >= 0; i--)
            {
                resourceSlots[i].CleanUp();
                ObjectPool.Instance.ReturnToPool(resourceSlots[i]);
            }

            base.CleanUp();
        }

        protected override void AttachEvents()
        {
            base.AttachEvents();

            if (Player != null)
            {
                Player.InventoryController.InventoryModule.OnStackCreated += HandleStackCreated;
                Player.InventoryController.InventoryModule.OnStackRemoved += HandleStackRemoved;
            }
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();

            if (Player != null)
            {
                Player.InventoryController.InventoryModule.OnStackCreated -= HandleStackCreated;
                Player.InventoryController.InventoryModule.OnStackRemoved -= HandleStackRemoved;
            }
        }

        private void InitialRefreshSlot()
        {
            int diff = defaultItemsSlots - Player.InventoryController.InventoryModule.ItemStacks.Count;
            foreach (var itemStack in Player.InventoryController.InventoryModule.ItemStacks)
                AddItemSlot(itemStack);

            if (diff > 0)
                for (int i = 0; i < diff; i++)
                    AddItemSlot(null);

            foreach (var resource in ResourcesManager.Instance.Resources)
                AddResourceSlot(resource);
        }

        private void AddResourceSlot(ResourceInGame resource)
        {
            ResourceSlot slot = ObjectPool.Instance.GetFromPool(resourceSlotPrefabId, UIManager.Instance.DefaultUIPoolCategory).GetComponent<ResourceSlot>();
            slot.transform.SetParent(resourcesContainer);
            slot.SetItem(resource);
            resourceSlots.Add(slot);
        }

        private void AddItemSlot(ItemStackInGame itemStack)
        {
            ItemSlot slot = ObjectPool.Instance.GetFromPool(itemSlotPrefabId, UIManager.Instance.DefaultUIPoolCategory).GetComponent<ItemSlot>();
            slot.transform.SetParent(itemsContainer);
            slot.SetItem(itemStack);
            itemSlots.Add(slot);
        }

        #region HANDLERS

        private void HandleStackCreated(ItemStackInGame itemStackInGame)
        {
            ItemSlot slot = itemSlots.Find(x => x.ItemStack.IdEquals(itemStackInGame.Id));
            if (slot == null)
                AddItemSlot(itemStackInGame);
        }

        private void HandleStackRemoved(ItemStackInGame itemStackInGame)
        {
            ItemSlot slot = itemSlots.Find(x => x.ItemStack.IdEquals(itemStackInGame.Id));
            if (slot)
            {
                slot.CleanUp();
                ObjectPool.Instance.ReturnToPool(slot);
                itemSlots.Remove(slot);
            }
        }

        #endregion

        #endregion
    }
}