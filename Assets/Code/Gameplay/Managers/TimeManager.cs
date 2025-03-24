using Gameplay.Management;
using System;
using UnityEngine;

namespace Gameplay.Timing
{
    public class TimeManager : GameplayManager<TimeManager>
    {
        #region ACTIONS

        public event Action OnSeccondPassed;

        #endregion

        #region VARIABLES

        [SerializeField] private float rareTickTime = 1;
        [SerializeField, HideInInspector] private float currentTime = 0;

        #endregion

        #region PROPERTIES

        #endregion

        #region UNITY_METHODS

        private void Update()
        {
            currentTime += Time.deltaTime;

            if (currentTime >= rareTickTime)
            {
                OnSeccondPassed?.Invoke();
                currentTime = 0;
            }
        }

        #endregion

        #region METHODS

        #endregion
    }
}
