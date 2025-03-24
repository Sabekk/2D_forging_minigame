using Database;
using Database.Resources;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Gameplay.Resources
{
    [Serializable]
    public class ResourceInGame
    {
        #region ACTIONS

        public event Action OnValueChanged;

        #endregion

        #region VARIABLES

        [SerializeField, ReadOnly] private int dataId;
        [SerializeField] private int currentValue;

        private ResourceData resourceData;

        #endregion

        #region PROPERTIES

        public ResourceData ResourceData
        {
            get
            {
                if (resourceData == null)
                    resourceData = MainDatabases.Instance.ResourcesDatabase.GetItemData(dataId);
                return resourceData;
            }
        }

        public int CurrentValue
        {
            get { return currentValue; }
            set
            {
                currentValue = Math.Clamp(value, 0, int.MaxValue);
                OnValueChanged?.Invoke();
            }
        }

        #endregion

        #region CONSTRUCTORS

        public ResourceInGame() { }
        public ResourceInGame(ResourceData resourceData)
        {
            this.resourceData = resourceData;
            dataId = resourceData.Id;
        }

        #endregion

        #region METHODS

        public void AddValue(int delta)
        {
            CurrentValue += delta;
        }

        #endregion
    }
}