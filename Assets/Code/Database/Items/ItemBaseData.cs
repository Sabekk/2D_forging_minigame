using Sirenix.OdinInspector;
using UnityEngine;

namespace Database.Items
{
    public class ItemBaseData : BaseDataOfDatabase
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