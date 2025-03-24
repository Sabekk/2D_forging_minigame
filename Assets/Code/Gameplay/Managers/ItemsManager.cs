using Database.Items;
using Gameplay.Character;
using Gameplay.Items;

namespace Gameplay.Management.Items
{
    public class ItemsManager : GameplayManager<ItemsManager>
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS


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

        #endregion
    }
}