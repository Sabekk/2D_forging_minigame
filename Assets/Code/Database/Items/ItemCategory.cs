using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Items
{
    [Serializable]
    public class ItemCategory : BaseDataOfDatabase
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Base setting")] private bool canBeCrafted;
        [SerializeField, FoldoutGroup("Base setting")] private List<ItemData> itemsData;

        #endregion

        #region PROPERTIES

        public bool CanBeCrafted => canBeCrafted;
        public List<ItemData> ItemsData => itemsData;

        #endregion

        #region METHODS

        public void AddItemData(ItemData data)
        {
            if (itemsData == null)
                itemsData = new();
            itemsData.Add(data);
        }

        #endregion
    }
}