using Database;
using Database.Items;
using Gameplay.Character;
using Gameplay.Items;
using Gameplay.Management.Characters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Management.Items
{
    public class ItemsManager : GameplayManager<ItemsManager>
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Debug"), ValueDropdown(ItemsDatabase.GET_NON_CRAFTABLE_ITEM_DATA_METHOD)] private int debugIdItem;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public void AddItemToCharacter(int itemDataId, CharacterInGame character, int itemCount = 1)
        {
            ItemData itemData = MainDatabases.Instance.ItemsDatabase.GetItemDataFromCategories(itemDataId);
            AddItemToCharacter(itemData, character, itemCount);
        }

        public void AddItemToCharacter(ItemData itemData, CharacterInGame character, int itemCount = 1)
        {
            ItemInGame item = new ItemInGame(itemData);
            AddItemToCharacter(item, character, itemCount);
        }

        public void AddItemToCharacter(ItemInGame item, CharacterInGame character, int itemCount = 1)
        {
            if (character == null)
                return;

            if (item == null)
                return;

            character.InventoryController.InventoryModule.CollectItem(item, itemCount);
        }

        public void RemoveItem(int itemDataId, CharacterInGame character, int countToRemove = 1)
        {
            character.InventoryController.InventoryModule.RemoveItem(itemDataId, countToRemove);
        }

        public void RemoveItem(ItemInGame item, CharacterInGame character, int countToRemove = 1)
        {
            character.InventoryController.InventoryModule.RemoveItem(item, countToRemove);
        }

        [Button, FoldoutGroup("Debug")]
        private void AddDebugItem()
        {
            if (CharacterManager.Instance == null)
                return;

            AddItemToCharacter(debugIdItem, CharacterManager.Instance.Player, 1);
        }

        [Button, FoldoutGroup("Debug")]
        private void RemoveDebugItem()
        {
            if (CharacterManager.Instance == null)
                return;

            RemoveItem(debugIdItem, CharacterManager.Instance.Player, 1);
        }

        #endregion
    }
}