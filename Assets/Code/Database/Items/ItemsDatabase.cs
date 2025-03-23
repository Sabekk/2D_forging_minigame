using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Items
{
    [CreateAssetMenu(menuName = "Database/ItemsDatabase", fileName = "ItemsDatabase")]
    public class ItemsDatabase : BaseDatabase<ItemData>
    {
        #region VARIABLES

        public const string GET_ITEM_DATA_METHOD = "@" + nameof(ItemsDatabase) + "." + nameof(GetItemDatas) + "()";

        #endregion

        #region PROPERTIES


        #endregion

        #region METHODS

        public static IEnumerable GetItemDatas()
        {
            ValueDropdownList<int> values = new();
            foreach (ItemData itemData in MainDatabases.Instance.ItemsDatabase.Datas)
                values.Add(itemData.DataName, itemData.Id);

            return values;
        }

        #endregion
    }
}
