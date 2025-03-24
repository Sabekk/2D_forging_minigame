using ModifiableValues;
using UnityEngine;

namespace Gameplay.Values.Character
{
    public class CharacterValues : ModifiableValuesContainer
    {
        #region VARIABLES

        [SerializeField] private ModifiableValue additionalProductionSpeed = new();

        #endregion

        #region PROPERTIES

        public ModifiableValue AdditionalProductionSpeed => additionalProductionSpeed;

        #endregion

        #region METHODS

        #endregion
    }
}