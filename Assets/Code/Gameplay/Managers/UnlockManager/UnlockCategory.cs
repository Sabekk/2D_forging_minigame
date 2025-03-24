using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Management.Unlocks
{
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

        public bool InUnlocked(int id)
        {
            return UnlockedIds.Contains(id);
        }

        #endregion
    }
}
