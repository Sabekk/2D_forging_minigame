using Gameplay.Character;
using Gameplay.Items;
using Gameplay.Management.Characters;
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

        [SerializeField] private Transform resourcesContainer;
        [SerializeField] private Transform itemsContainer;
        [SerializeField] private int defaultItemsSlots = 10;

        //TODO Add to character inventory count slots value
        private List<ItemSlot> itemSlots;

        #endregion

        #region PROPERTIES

        private PlayerInGame Player => CharacterManager.Instance.Player;

        #endregion

        #region METHODS

        public override void Initialize()
        {
            base.Initialize();
            itemSlots = new();
            InitialRefreshSlot();
        }

        public override void CleanUp()
        {
            for (int i = itemSlots.Count - 1; i >= 0; i--)
            {
                itemSlots[i].CleanUp();
                ObjectPool.Instance.ReturnToPool(itemSlots[i]);
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
        }

        private void AddResourceSlot(ResourceInGame resource)
        {
            //TODO
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