using ModifiableValues;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Gameplay.Values
{
    public class StartingValue<T> where T : ModifiableValuesContainer
    {
        #region VARIABLES

        [ValueDropdown(nameof(GetAllModifiableNamesFromContainer))]
        [SerializeField] string valueName;
        [SerializeField] float value;

        #endregion

        #region PROPERTIES

        public string ValueName => valueName;
        public float Value => value;

        #endregion


        #region METHODS

        public List<string> GetAllModifiableNamesFromContainer()
        {
            List<string> listOfNames = new List<string>();

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (var property in properties)
                listOfNames.Add(property.Name);

            return listOfNames;
        }

        #endregion
    }
}