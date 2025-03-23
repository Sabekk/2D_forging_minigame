using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Database.Machines
{
    [Serializable]
    public sealed class MachineData : BaseDataOfDatabase
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Base setting")] private Sprite icon;

        #endregion

        #region PROPERTIES

        public Sprite Icon => icon;

        #endregion

        #region METHODS

        #endregion
    }
}