using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Items
{
    [CreateAssetMenu(menuName = "Database/ItemsDatabase", fileName = "ItemsDatabase")]
    public class ItemsDatabase : ScriptableObject
    {
        #region VARIABLES

        public const string GET_ITEM_DATA_METHOD = "@" + nameof(ItemsDatabase) + "." + nameof(GetItemDatas) + "()";

        [SerializeField] private List<ItemData> itemDatas;

        #endregion

        #region PROPERTIES

        public List<ItemData> ItemDatas => itemDatas;

        #endregion

        #region METHODS

        public ItemData GetItemData(int id)
        {
            return ItemDatas.Find(x => x.IdEquals(id));
        }

        public static IEnumerable GetItemDatas()
        {
            ValueDropdownList<int> values = new();
            foreach (ItemData itemData in MainDatabases.Instance.ItemsDatabase.ItemDatas)
                values.Add(itemData.ItemName, itemData.Id);

            return values;
        }

        #endregion
    }
}
