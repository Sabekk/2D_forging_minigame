using Database.Machines;
using Gameplay.Management.Unlocks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Effects
{
    public class UnlockMachineEffect : UnlockManagerEffects
    {
        #region VARIABLES

        [SerializeField, ValueDropdown(MachinesDatabase.GET_MACHINES_DATA_METHOD)] private int unlockMachineId;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public override void ExecuteEffect()
        {
            base.ExecuteEffect();
            if (UnlocksManager.Instance)

                UnlocksManager.Instance.MachinesUnlocks.UnlockId(unlockMachineId);

        }

        public override void RemoveEffect()
        {
            base.RemoveEffect();
            if (UnlocksManager.Instance)
                UnlocksManager.Instance.MachinesUnlocks.LockId(unlockMachineId);

        }

        #endregion
    }
}