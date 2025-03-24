using Database;
using Database.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    public class ItemInGame
    {
        #region VARIABLES

        [SerializeField] int dataId;

        private ItemData itemData;

        #endregion

        #region PROPERTIES

        public ItemData ItemData
        {
            get
            {
                if (itemData == null)
                    itemData = MainDatabases.Instance.ItemsDatabase.GetItemDataFromCategories(dataId);
                return itemData;
            }
        }

        #endregion

        #region CONSTRUCTORS

        public ItemInGame() { }
        public ItemInGame(ItemData itemData)
        {
            this.itemData = itemData;
            dataId = itemData.Id;
        }

        #endregion

        #region METHODS

        #endregion
    }
}
