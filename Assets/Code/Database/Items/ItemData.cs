using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Database.Items
{
    [Serializable]
    public class ItemData : BaseDataOfDatabase
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Base setting")] protected Sprite icon;
        [SerializeField, FoldoutGroup("Sell setting")] protected bool canSell;
        [SerializeField, FoldoutGroup("Sell setting"), ShowIf(nameof(canSell))] protected int sellValue;


        #endregion

        #region PROPERTIES

        public int SellValue => sellValue;
        public Sprite Icon => icon;
        public bool CanBeSelled => canSell;

        #endregion

        #region METHODS

        #endregion
    }
}