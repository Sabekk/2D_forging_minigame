using Gameplay.Player.Controller.Inventory.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Player.Controller.Inventory
{
    public class InventoryController : ControllerBase
    {
        #region VARIABLES

        [SerializeField] private ResourcesModule resourcesModule;

        #endregion

        #region PROPERTIES

        public ResourcesModule ResourcesModule => resourcesModule;


        #endregion

        #region METHODS

        public override void CreateModules()
        {
            base.CreateModules();
            resourcesModule = new ResourcesModule();
        }

        public override void SetModules()
        {
            base.SetModules();
            modules.Add(resourcesModule);
        }

        #endregion
    }
}