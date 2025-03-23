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
        [SerializeField] protected string itemName;
        [SerializeField] protected Sprite icon;
        [SerializeField] protected int sellValue;


        #endregion

        #region PROPERTIES
        public int Id => id;
        public int SellValue => sellValue;
        public Sprite Icon => icon;
        public string ItemName => itemName;

        #endregion

        #region METHODS

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion
    }
}