using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Resources
{
    [Serializable]
    public class StartingResources
    {
        #region VARIABLES

        [SerializeField] private List<StartingResource> resources;

        #endregion

        #region PROPERTIES

        public List<StartingResource> Resources => resources;

        #endregion

        #region METHODS

        #endregion
    }
}