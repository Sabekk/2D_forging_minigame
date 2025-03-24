using Database.Resources;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Gameplay.Resources
{
    [Serializable]
    public class StartingResource
    {
        #region VARIABLES

        [SerializeField, ValueDropdown(ResourcesDatabase.GET_RESOURCE_DATA_METHOD)] private int resourceDataId;
        [SerializeField] private bool randomInRange;
        [SerializeField] private int startingValue;
        [SerializeField, ShowIf(nameof(randomInRange))] private int startingValueMax;

        #endregion

        #region PROPERTIES

        public int ResourceDataId => resourceDataId;
        public int StartingValue => startingValue;
        public int StartingValueMax => startingValueMax;
        public bool RandomInRange => randomInRange;

        #endregion

        #region METHODS

        #endregion
    }
}