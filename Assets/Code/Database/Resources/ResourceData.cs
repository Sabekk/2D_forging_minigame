using Database.Items;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Database.Resources
{
    [Serializable]
    public class ResourceData : ItemBaseData
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Buy setting")] protected bool canBuying;
        [SerializeField, FoldoutGroup("Buy setting"), ShowIf(nameof(canBuying))] protected int buyValue;

        [SerializeField, FoldoutGroup("Filling setting")] protected bool autoFilling;
        [SerializeField, FoldoutGroup("Filling setting"), ShowIf(nameof(autoFilling))] protected int timeToReceive;
        [SerializeField, FoldoutGroup("Filling setting"), ShowIf(nameof(autoFilling))] protected int receiveCount;

        #endregion

        #region PROPERTIES

        public bool AutoFilling => autoFilling;
        public bool CanBuying => canBuying;
        public int BuyValue => buyValue;
        public int TimeToReceive => timeToReceive;
        public int ReceiveCount => receiveCount;

        #endregion

        #region METHODS

        #endregion
    }
}
