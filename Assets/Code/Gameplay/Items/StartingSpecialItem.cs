using Database.Items;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Gameplay.Items
{
    [Serializable]
    public class StartingSpecialItem
    {
        #region VARIABLES

        [SerializeField, ValueDropdown(ItemsDatabase.GET_NON_CRAFTABLE_ITEM_DATA_METHOD)] private int itemDataId;
        [SerializeField] private int count;
        [SerializeField, MinValue(0f), MaxValue(100f), SuffixLabel("%")] private float chanceToGet;

        #endregion

        #region PROPERTIES

        public int ItemDataId => itemDataId;
        public int Count => count;
        public float ChanceToGet => chanceToGet;

        #endregion

        #region METHODS

        #endregion
    }
}