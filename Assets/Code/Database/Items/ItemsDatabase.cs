using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Items
{
    [CreateAssetMenu(menuName = "Database/ItemsDatabase", fileName = "ItemsDatabase")]
    public class ItemsDatabase : ScriptableObject
    {
        #region VARIABLES

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

        #endregion
    }
}
