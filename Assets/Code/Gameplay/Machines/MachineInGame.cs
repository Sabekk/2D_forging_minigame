using Database;
using Database.Machines;
using System;
using UnityEngine;

namespace Gameplay.Machines
{
    [Serializable]
    public class MachineInGame
    {
        #region VARIABLES

        [SerializeField] private int dataId;
        [SerializeField] private bool isUnlocked;

        private MachineData machineData;

        #endregion

        #region PROPERTIES

        public MachineData MachineData
        {
            get
            {
                if (machineData == null)
                    machineData = MainDatabases.Instance.MachinesDatabase.GetItemData(dataId);
                return machineData;
            }
        }

        public bool IsUnlocked => isUnlocked;

        #endregion

        #region CONSTRUCTORS

        public MachineInGame() { }
        public MachineInGame(MachineData machineData, bool isUnlocked)
        {
            this.machineData = machineData;
            dataId = machineData.Id;
            this.isUnlocked = isUnlocked;
        }

        #endregion

        #region METHODS

        public void ToggleUnlockState(bool state)
        {
            isUnlocked = state;
        }

        public void TryChangeProduceTime()
        {
            if (isUnlocked == false)
                return;

        }

        #endregion
    }
}
