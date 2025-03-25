using Gameplay.Items;
using Gameplay.Management.Effects;

namespace Gameplay.Character.Controller.Effects
{
    public class EffectsController : ControllerBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        private EffectsManager Manager => EffectsManager.Instance != null ? EffectsManager.Instance : null;

        #endregion

        #region METHODS

        public override void AttachEvents()
        {
            base.AttachEvents();
            Character.InventoryController.InventoryModule.OnItemCollected += HandleItemCollected;
            Character.InventoryController.InventoryModule.OnItemRemoved += HandleItemRemoved;
            Character.InventoryController.InventoryModule.OnStackCreated += HandleStackCreated;
            Character.InventoryController.InventoryModule.OnStackRemoved += HandleStackRemoved;
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            Character.InventoryController.InventoryModule.OnItemCollected -= HandleItemCollected;
            Character.InventoryController.InventoryModule.OnItemRemoved -= HandleItemRemoved;
            Character.InventoryController.InventoryModule.OnStackCreated -= HandleStackCreated;
            Character.InventoryController.InventoryModule.OnStackRemoved -= HandleStackRemoved;
        }

        private void AddStackableEffectsFromItem(ItemInGame item) => AddEffectsFromItem(item, true);
        private void AddNonStackableEffectsFromItem(ItemInGame item) => AddEffectsFromItem(item, false);
        private void RemoveStackableEffectsFromItem(ItemInGame item) => RemoveEffectsFromItem(item, true);
        private void RemoveNonStackableEffectsFromItem(ItemInGame item) => RemoveEffectsFromItem(item, false);

        private void RemoveEffectsFromItem(ItemInGame item, bool stackable)
        {
            if (Manager == null)
                return;
            if (item.ItemData.EffectsWhileOwnInInventory == null)
                return;

            foreach (var effect in item.ItemData.EffectsWhileOwnInInventory)
            {
                if (effect.IsStackable == stackable)
                    Manager.RemoveEffect(effect, Character);
            }
        }

        private void AddEffectsFromItem(ItemInGame item, bool stackable)
        {
            if (Manager == null)
                return;
            if (item.ItemData.EffectsWhileOwnInInventory == null)
                return;

            foreach (var effect in item.ItemData.EffectsWhileOwnInInventory)
            {
                if (effect.IsStackable == stackable)
                    Manager.ExecuteEffect(effect, Character);
            }
        }

        #region HANDLERS

        private void HandleItemCollected(ItemInGame item) => AddStackableEffectsFromItem(item);
        private void HandleItemRemoved(ItemInGame item) => RemoveStackableEffectsFromItem(item);
        private void HandleStackCreated(ItemStackInGame itemStack) => AddNonStackableEffectsFromItem(itemStack.Item);
        private void HandleStackRemoved(ItemStackInGame itemStack) => RemoveNonStackableEffectsFromItem(itemStack.Item);

        #endregion

        #endregion
    }
}
