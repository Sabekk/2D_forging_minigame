using Gameplay.Values.Character;
using UnityEngine;

namespace Gameplay.Character.Controller.Values
{
    public class ValuesController : ControllerBase
    {
        #region VARIABLES

        [SerializeField] private CharacterValues characterValues;

        #endregion

        #region PROPERTIES

        public CharacterValues CharacterValues => characterValues;

        #endregion

        #region METHODS

        public override void Initialize(CharacterInGame character)
        {
            base.Initialize(character);
            characterValues = new CharacterValues();
        }

        public override void SetStartingData()
        {
            base.SetStartingData();
            characterValues.Initialze();
            characterValues.SetStartingValues<CharacterStartingValue, CharacterValues>(Character.Data.StartingValues);
        }

        #endregion
    }
}