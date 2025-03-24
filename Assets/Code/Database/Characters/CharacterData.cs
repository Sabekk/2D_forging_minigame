using Gameplay.Values.Character;
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

        #endregion

        #region PROPERTIES

        public List<CharacterStartingValue> StartingValues => startingValues;

        #endregion

        #region METHODS

        #endregion
    }
}