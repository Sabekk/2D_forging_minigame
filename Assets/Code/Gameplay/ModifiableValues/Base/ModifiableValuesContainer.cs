using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;
using Gameplay.Values;

namespace ModifiableValues
{
    [Serializable]
    public class ModifiableValuesContainer
    {
        #region VARIABLES

        [SerializeField] private Dictionary<string, ModifiableValue> allValues = new Dictionary<string, ModifiableValue>();

        #endregion

        #region PROPERTIES

        public Dictionary<string, ModifiableValue> AllValues => allValues;

        #endregion

        #region CONSTRUCTORS

        public ModifiableValuesContainer()
        {

        }

        #endregion

        #region METHODS

        public void Initialze()
        {
            allValues.Clear();
            PropertyInfo[] listOfProperties = GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in listOfProperties)
            {
                object propertyObject = propertyInfo.GetValue(this);
                if (propertyObject is ModifiableValue modifiableValue)
                {
                    allValues.Add(propertyInfo.Name, modifiableValue);
                }
            }
        }

        public void SetStartingValues<T1, T2>(List<T1> startingValues) where T1: StartingValue<T2> where T2: ModifiableValuesContainer
        {
            foreach (var startingValue in startingValues)
                SetStartingValue<T1, T2>(startingValue);
        }

        public void SetStartingValue<T1, T2>(T1 startingValue) where T1 : StartingValue<T2> where T2 : ModifiableValuesContainer
        {
            ModifiableValue value = GetValue(startingValue.ValueName);
            if (value == null)
            {
                Debug.LogError($"Niepoprawnie ustawiony starting value dla {GetType().Name}, nie odnaleziono wartoœci {startingValue.ValueName}");
                return;
            }

            value.SetRawValue(startingValue.Value);
        }

        public ModifiableValue GetValue(string valueId)
        {
            if (allValues.TryGetValue(valueId, out ModifiableValue value))
                return value;
            return null;
        }

        #endregion
    }
}
