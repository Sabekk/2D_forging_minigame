using Gameplay.Machines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Window.Machines
{
    public class MachinesWindow : UIWindowNested
    {
        #region VARIABLES

        [SerializeField] private MachineWindow_Forging forgingPart;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public void SelectMachine(MachineInGame machineInGame)
        {
            forgingPart.SelectMachine(machineInGame);
        }

        #endregion
    }
}