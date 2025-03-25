using Gameplay.Effects;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Items
{
    [Serializable]
    public class ItemData : ItemBaseData
    {
        #region VARIABLES

        [SerializeReference] private List<CharacterEffect> effectsWhileOwnInInventory;

        #endregion

        #region PROPERTIES

        public List<CharacterEffect> EffectsWhileOwnInInventory => effectsWhileOwnInInventory;

        #endregion

        #region METHODS

        #endregion
    }
}