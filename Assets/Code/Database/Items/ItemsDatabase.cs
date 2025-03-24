using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Items
{
    [CreateAssetMenu(menuName = "Database/ItemsDatabase", fileName = "ItemsDatabase")]
    public class ItemsDatabase : BaseDatabase<ItemCategory>
    {
        #region VARIABLES

        public const string GET_CRAFTABLE_ITEM_DATA_METHOD = "@" + nameof(ItemsDatabase) + "." + nameof(GetCraftedItemDatas) + "(true)";
        public const string GET_NON_CRAFTABLE_ITEM_DATA_METHOD = "@" + nameof(ItemsDatabase) + "." + nameof(GetCraftedItemDatas) + "(false)";

        #endregion

        #region PROPERTIES


        #endregion

        #region METHODS

        public static IEnumerable GetCraftedItemDatas(bool craftableOnly)
        {
            ValueDropdownList<int> values = new();
            foreach (var categoryItems in MainDatabases.Instance.ItemsDatabase.Datas)
            {
                if (categoryItems.CanBeCrafted == craftableOnly)
                    foreach (var itemData in categoryItems.ItemsData)
                    {
                        values.Add(itemData.DataName, itemData.Id);
                    }
            }

            return values;
        }

        public ItemData GetItemDataFromCategories(int id)
        {
            ItemData itemData = null;
            foreach (var itemCategoryData in Datas)
            {
                itemData = itemCategoryData.ItemsData.GetElementById(id);
                if (itemData != null)
                    return itemData;
            }

            return itemData;
        }

        #endregion
    }
}
