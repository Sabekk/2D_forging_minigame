using Database.Items;
using Database.Resources;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Recipes
{
    [Serializable]
    public class RecipeData : BaseDataOfDatabase
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Base setting"), ValueDropdown(ItemsDatabase.GET_ITEM_DATA_METHOD)] private int createdItemDataId;
        [SerializeField, FoldoutGroup("Base setting")] private bool canBroke;
        [SerializeField, FoldoutGroup("Base setting"), SuffixLabel("seconds")] private int craftingTime;
        [SerializeField, FoldoutGroup("Success setting"), ShowIf(nameof(canBroke)), MinValue(0f), MaxValue(100f), SuffixLabel("%")] private int successChance;
        [SerializeField, FoldoutGroup("Need setting")] private List<NeededResources> neededResources;
        [SerializeField, FoldoutGroup("Need setting")] private List<NeededItems> neededItems;

        #endregion

        #region PROPERTIES

        public int CreatedItemDataId => createdItemDataId;
        public int CraftingTime => craftingTime;
        public int SuccessChance => successChance;
        public bool CanBroke => canBroke;
        public List<NeededResources> Resources => neededResources;
        public List<NeededItems> Items => neededItems;

        #endregion

        #region METHODS

        #endregion

        #region STRUCTS

        [Serializable]
        public struct NeededResources
        {
            #region VARIABLES

            [SerializeField, ValueDropdown(ResourcesDatabase.GET_RESOURCE_DATA_METHOD)] private int resourceDataId;
            [SerializeField] private int neededCount;

            #endregion

            #region PROPERTIES

            public int ResourceDataId => resourceDataId;
            public int NeededCount => neededCount;

            #endregion
        }

        [Serializable]
        public struct NeededItems
        {
            #region VARIABLES

            [SerializeField, ValueDropdown(ItemsDatabase.GET_ITEM_DATA_METHOD)] private int itemDataId;
            [SerializeField] private int neededCount;

            #endregion

            #region PROPERTIES

            public int ItemDataId => itemDataId;
            public int NeededCount => neededCount;

            #endregion
        }

        #endregion
    }
}