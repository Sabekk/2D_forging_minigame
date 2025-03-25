using Gameplay.Machines;
using Gameplay.Management.Machines;
using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.HUD
{
    public class RightBar : HUDBar
    {
        #region VARIABLES

        [SerializeField] private Transform buttonsHolder;
        [SerializeField] private List<MachineButton> buttons;

        const string MACHINE_BUTTON_POOL = "HUD_MachineButton";

        #endregion

        #region PROPERTIES

        private MachinesManager Manager => MachinesManager.Instance;

        #endregion

        #region METHODS

        public override void Initialize()
        {
            base.Initialize();
            SpawnButtons();
        }

        public override void CleanUp()
        {
            base.CleanUp();
            for (int i = buttons.Count - 1; i >= 0; i--)
            {
                ObjectPool.Instance.ReturnToPool(buttons[i]);
            }
        }

        private void SpawnButtons()
        {
            if (Manager == null)
                return;

            buttonsHolder.DestroyChildrenImmediate();

            foreach (var machineInGame in Manager.MachinesInGame)
            {
                MachineButton button = ObjectPool.Instance.GetFromPool(MACHINE_BUTTON_POOL, HUDManager.HUD_POOL_CATEGORY).GetComponent<MachineButton>();
                button.transform.SetParent(buttonsHolder);
                button.transform.localScale = Vector3.one;
                button.SetMachine(machineInGame);
            }
        }

        #endregion
    }
}
