using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    [Serializable]
    public class StartingItems
    {
        #region VARIABLES

        [SerializeField] private List<StartingSpecialItem> specialItems;

        #endregion

        #region PROPERTIES

        public List<StartingSpecialItem> SpecialItems => specialItems;

        #endregion

        #region METHODS

        #endregion
    }
}