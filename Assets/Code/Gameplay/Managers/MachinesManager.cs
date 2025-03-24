using Database;
using Gameplay.Machines;
using Gameplay.Management.Timing;
using Gameplay.Management.Unlocks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Management.Machines
{
    public class MachinesManager : GameplayManager<MachinesManager>
    {
        #region ACTION

        public event Action<MachineInGame> OnMachineUnlocked;

        #endregion

        #region VARIABLES

        [SerializeField] private List<MachineInGame> machinesInGame;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public override void Initialzie()
        {
            base.Initialzie();
            machinesInGame = new();
            foreach (var machineData in MainDatabases.Instance.MachinesDatabase.Datas)
                machinesInGame.Add(new MachineInGame(machineData, CheckMachineIsUnlocked(machineData.Id)));
        }

        public override void AttachEvents()
        {
            base.AttachEvents();
            if (TimeManager.Instance)
                TimeManager.Instance.OnSeccondPassed += HandleSeccondPassed;

            if (UnlocksManager.Instance)
                UnlocksManager.Instance.MachinesUnlocks.OnUnlockedId += HandleMachineUnlocked;
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            if (TimeManager.Instance)
                TimeManager.Instance.OnSeccondPassed -= HandleSeccondPassed;

            if (UnlocksManager.Instance)
                UnlocksManager.Instance.MachinesUnlocks.OnUnlockedId -= HandleMachineUnlocked;

        }

        private bool CheckMachineIsUnlocked(int machineDataId)
        {
            if (UnlocksManager.Instance)
                return UnlocksManager.Instance.MachinesUnlocks.InUnlocked(machineDataId);

            return false;
        }

        #region HANDLERS

        private void HandleSeccondPassed()
        {
            foreach (var machine in machinesInGame)
                machine.TryChangeProduceTime();
        }

        private void HandleMachineUnlocked(int machineDataId)
        {
            MachineInGame machineInGame = machinesInGame.Find(x => x.MachineData.IdEquals(machineDataId));

            if (machineInGame == null)
                return;

            machineInGame.ToggleUnlockState(true);
            OnMachineUnlocked?.Invoke(machineInGame);
        }

        #endregion

        #endregion
    }
}
