using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Database.Items
{
    [Serializable]
    public class ItemData : IIdEqualable
    {
        #region VARIABLES

        [SerializeField, ReadOnly] protected int id = Guid.NewGuid().GetHashCode();
        [SerializeField, FoldoutGroup("Base setting")] protected string itemName;
        [SerializeField, FoldoutGroup("Base setting")] protected Sprite icon;
        [SerializeField, FoldoutGroup("Sell setting")] protected bool canSell;
        [SerializeField, FoldoutGroup("Sell setting"), ShowIf(nameof(canSell))] protected int sellValue;


        #endregion

        #region PROPERTIES

        public int Id => id;
        public int SellValue => sellValue;
        public Sprite Icon => icon;
        public string ItemName => itemName;
        public bool CanBeSelled => canSell;

        #endregion

        #region METHODS

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion
    }
}