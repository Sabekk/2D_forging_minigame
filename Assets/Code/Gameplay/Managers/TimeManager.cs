using System;
using UnityEngine;

namespace Gameplay.Management.Timing
{
    public class TimeManager : GameplayManager<TimeManager>
    {
        #region ACTIONS

        public event Action OnSecondPassed;

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
                OnSecondPassed?.Invoke();
                currentTime = 0;
            }
        }

        #endregion

        #region METHODS

        #endregion
    }
}
