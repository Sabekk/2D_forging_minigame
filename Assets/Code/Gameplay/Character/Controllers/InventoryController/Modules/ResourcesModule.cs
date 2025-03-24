using Gameplay.Management.Resources;
using Gameplay.Player.Controller.Module;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Player.Controller.Inventory.Resources
{
    public class ResourcesModule : ControllerModuleBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public override void Initialize(CharacterInGame character)
        {
            base.Initialize(character);
            AddDefaultResources();
        }

        private void AddDefaultResources()
        {
            if (ResourcesManager.Instance)
            {
                if (Character.Data.StartingResources != null)
                    foreach (var startingResource in Character.Data.StartingResources.Resources)
                    {
                        int value = 0;
                        if (startingResource.RandomInRange == false)
                            value = startingResource.StartingValue;
                        else
                            value = Random.Range(startingResource.StartingValue, startingResource.StartingValueMax + 1);

                        ResourcesManager.Instance.AddResource(value, startingResource.ResourceDataId);
                    }
            }
        }


        #endregion
    }
}
