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

        [SerializeField] private float defaultTimeScale;
        private float previousTimeScale;

        #endregion

        #region PROPERTIES
        private bool TimeIsStopped => Time.timeScale == 0;

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

        public override void CleanUp()
        {
            Time.timeScale = defaultTimeScale;
            base.CleanUp();
        }

        public void TryToggleTime(bool state)
        {
            if (TimeIsStopped && state == true)
            {
                Time.timeScale = previousTimeScale != 0 ? previousTimeScale : defaultTimeScale;
            }
            else if (!TimeIsStopped && state == false)
            {
                Time.timeScale = previousTimeScale;
                Time.timeScale = 0;
            }
        }


        #endregion
    }
}
