using Gameplay.Management;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.HUD
{
    public class HUDManager : MonoSingleton<HUDManager>, IAttachableEvents
    {
        #region VARIABLES

        public const string HUD_POOL_CATEGORY = "HUD";

        [SerializeField] private List<HUDBar> hudBars;

        #endregion

        #region PROPERTIES

        #endregion

        #region UNITY_METHODS

        private void Start()
        {
            if (GameplayManagersParent.Instance.Initialized == false)
                AttachEvents();
            else
                HandleManagersInitialized();
        }

        private void OnDestroy()
        {
            DetachEvents();
            hudBars.ForEach(x => x.CleanUp());
        }

        #endregion

        #region METHODS

        public void AttachEvents()
        {
            if (GameplayManagersParent.Instance)
                GameplayManagersParent.Instance.OnManagersInitialized += HandleManagersInitialized;
        }

        public void DetachEvents()
        {
            if (GameplayManagersParent.Instance)
                GameplayManagersParent.Instance.OnManagersInitialized -= HandleManagersInitialized;
        }


        #region HANDLERS

        private void HandleManagersInitialized()
        {
            hudBars.ForEach(x => x.Initialize());
        }

        #endregion

        #endregion
    }
}
