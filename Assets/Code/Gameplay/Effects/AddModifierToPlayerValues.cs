using Gameplay.Character;
using Gameplay.Values;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Effects
{
    public class AddModifierToPlayerValues : PlayerEffect
    {
        #region VARIABLES


        [SerializeField, ValueDropdown("@CharacterValues.GetAllValuesDropdown<CharacterValues>()")] string valueName;
        [SerializeField] private int additionalValue;
        [SerializeField] private ValueType valueType;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public override void ExecuteEffect(PlayerInGame target)
        {
            base.ExecuteEffect(target);
            if (target == null)
                return;

            ModifiableValue value = target.ValuesController.CharacterValues.GetValue(valueName);

            if (value == null)
            {
                Debug.LogError($"Value didnt found for player: {valueName}");
                return;
            }

            Modifier modifier = new Modifier(Id, EffectName, additionalValue, valueType);
            value.AddModifier(modifier);
        }

        public override void RemoveEffect(PlayerInGame target)
        {
            base.RemoveEffect(target);
            if (target == null)
                return;

            ModifiableValue value = target.ValuesController.CharacterValues.GetValue(valueName);

            if (value == null)
            {
                Debug.LogError($"Value didnt found for player: {valueName}");
                return;
            }

            value.RemoveModifier(Id);
        }

        #endregion
    }
}