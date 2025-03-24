using Gameplay.Management.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Management.Unlocks
{
    [Serializable]
    public class UnlockCategory
    {
        #region ACTIONS

        public event Action<int> OnUnlockedId;

        #endregion

        #region VARIABLES

        [SerializeField] private List<int> unlockedIds;

        #endregion

        #region PROPERTIES

        public List<int> UnlockedIds
        {
            get
            {
                if (unlockedIds == null)
                    unlockedIds = new();
                return unlockedIds;
            }
        }

        #endregion

        #region METHODS

        public void UnlockId(int id)
        {
            if (InUnlocked(id) == false)
            {
                UnlockedIds.Add(id);
                OnUnlockedId?.Invoke(id);
            }
        }

        public void SetStartingValues()
        {
            if (CharacterManager.Instance && CharacterManager.Instance.Player != null)
                foreach (var startingUnlockedMachines in CharacterManager.Instance.Player.Data.StartingUnlockedMachines)
                    UnlockId(startingUnlockedMachines);
        }

        public bool InUnlocked(int id)
        {
            return UnlockedIds.Contains(id);
        }

        #endregion
    }
}
