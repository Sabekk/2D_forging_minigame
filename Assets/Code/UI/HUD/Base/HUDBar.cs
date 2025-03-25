using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.HUD
{
    public abstract class HUDBar : MonoBehaviour, IAttachableEvents
    {
        #region VARIABLES

        [SerializeField] private List<HUDBar> subElements;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public virtual void Initialize()
        {
            AttachEvents();
            subElements.ForEach(x => x.Initialize());
        }

        public virtual void CleanUp()
        {
            DetachEvents();
            subElements.ForEach(x => x.CleanUp());
        }

        public virtual void AttachEvents() { }
        public virtual void DetachEvents() { }

        #endregion
    }
}