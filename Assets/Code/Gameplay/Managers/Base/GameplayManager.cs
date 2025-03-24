using UnityEngine;

namespace Gameplay.Management
{
    public abstract class GameplayManager<T> : MonoSingleton<T>, IAttachableEvents, IGameplayManager where T : MonoBehaviour
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        public bool Initialized { get; protected set; }

        #endregion

        #region METHODS

        public virtual void Initialzie() => Initialized = true;
        /// <summary>
        /// Late initialization with attaching events
        /// </summary>
        public virtual void LateInitialzie()
        {
            AttachEvents();
        }
        /// <summary>
        /// Clearing with detaching events
        /// </summary>
        public virtual void CleanUp()
        {
            DetachEvents();
            Initialized = false;
        }

        public virtual void SetStartingValues() { }
        public virtual void AttachEvents() { }
        public virtual void DetachEvents() { }

        #endregion
    }
}