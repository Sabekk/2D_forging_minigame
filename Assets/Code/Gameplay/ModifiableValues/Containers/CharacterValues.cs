using ModifiableValues;
using UnityEngine;

namespace Gameplay.Values.Character
{
    public class CharacterValues : ModifiableValuesContainer
    {
        #region VARIABLES

        [SerializeField] private ModifiableValue additionalProductionSpeed = new("Additional production speed", 0, ValueType.OVERALL);
        [SerializeField] private ModifiableValue additionalSuccessChance = new("Additional success chance", 0, ValueType.PERCENTAGE);

        #endregion

        #region PROPERTIES

        public ModifiableValue AdditionalProductionSpeed => additionalProductionSpeed;
        public ModifiableValue AdditionalSuccessChance => additionalSuccessChance;

        #endregion

        #region METHODS

        #endregion
    }
}