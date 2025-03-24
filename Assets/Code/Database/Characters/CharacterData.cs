using Database.Machines;
using Gameplay.Values.Character;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Character
{
    [Serializable]
    public class CharacterData : BaseDataOfDatabase
    {
        #region VARIABLES

        [SerializeField] private List<CharacterStartingValue> startingValues;
        [SerializeField, ValueDropdown(MachinesDatabase.GET_MACHINES_DATA_METHOD)] private List<int> startingUnlockedMachines;

        #endregion

        #region PROPERTIES

        public List<CharacterStartingValue> StartingValues => startingValues;
        public List<int> StartingUnlockedMachines => startingUnlockedMachines;

        #endregion

        #region METHODS

        #endregion
    }
}