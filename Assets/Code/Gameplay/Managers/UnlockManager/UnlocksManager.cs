using Gameplay.Management.Characters;
using System;
using UnityEngine;

namespace Gameplay.Management.Unlocks
{
    public class UnlocksManager : GameplayManager<UnlocksManager>
    {
        #region VARIABLES

        [SerializeField] private UnlockCategory machinesUnlocks;

        #endregion

        #region PROPERTIES

        public UnlockCategory MachinesUnlocks => machinesUnlocks;

        #endregion

        #region METHODS

        #endregion
    }
}
