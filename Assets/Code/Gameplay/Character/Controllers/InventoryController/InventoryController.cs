using UnityEngine;

namespace Gameplay.Character.Controller.Inventory
{
    public class InventoryController : ControllerBase
    {
        #region VARIABLES

        [SerializeField] private InventoryModule inventoryModule;

        #endregion

        #region PROPERTIES

        public InventoryModule InventoryModule => inventoryModule;

        #endregion

        #region METHODS

        public override void CreateModules()
        {
            base.CreateModules();
            inventoryModule = new InventoryModule();
        }

        public override void SetModules()
        {
            base.SetModules();
            modules.Add(inventoryModule);
        }

        #endregion
    }
}