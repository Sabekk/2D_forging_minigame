using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Management
{
    public abstract class ManagersParent : MonoSingleton<ManagersParent>
    {
        #region ACTION

        public event Action OnManagersInitialized;

        #endregion

        #region VARIABLES

        [SerializeField] protected List<IGameplayManager> managers = new();

        #endregion

        #region PROPERTIES

        public bool Initialized { get; set; }

        #endregion

        #region UNITY_METHODS

        private void Start()
        {
            InitializeManagers();
            LateInitializeManagers();

            SetStartingValues();

            Initialized = true;
            OnManagersInitialized?.Invoke();
        }

        private void OnDestroy()
        {
            CleanUpManagers();
        }

        #endregion

        #region METHODS

        protected abstract void SetManagers();

        private void InitializeManagers()
        {
            if (managers == null)
                managers = new();

            SetManagers();

            for (int i = 0; i < managers.Count; i++)
                managers[i].Initialzie();
        }

        private void LateInitializeManagers()
        {
            for (int i = 0; i < managers.Count; i++)
                managers[i].LateInitialzie();
        }

        private void SetStartingValues()
        {
            for (int i = 0; i < managers.Count; i++)
                managers[i].SetStartingValues();
        }

        private void CleanUpManagers()
        {
            for (int i = 0; i < managers.Count; i++)
                managers[i].CleanUp();
        }

        #endregion
    }
}