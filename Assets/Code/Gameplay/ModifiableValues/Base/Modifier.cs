using UnityEngine;

namespace Gameplay.Values
{
    public class Modifier : ValueBase
    {
        #region VARIABLES

        [SerializeField] private int effectId;

        #endregion

        #region PROPERTIES

        public int EffectId => effectId;

        #endregion

        #region CONSTRUCTORS

        public Modifier() { }

        public Modifier(int effectId, string valueName, float value, ValueType valueType) : base(valueName, value, valueType)
        {
            this.effectId = effectId;
        }

        #endregion

        #region METHODS

        //Methods for showing values etc.

        #endregion
    }
}