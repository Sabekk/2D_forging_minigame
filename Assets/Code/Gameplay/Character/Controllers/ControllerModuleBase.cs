using UnityEngine;

namespace Gameplay.Character.Controller.Module
{
    public abstract class ControllerModuleBase : IAttachableEvents
    {
        #region VARIABLES

        [SerializeField, HideInInspector] private CharacterInGame character;

        #endregion

        #region PROPERTIES

        public CharacterInGame Character => character;

        #endregion

        #region METHODS

        public virtual void Initialize(CharacterInGame character)
        {
            this.character = character;
        }

        public virtual void CleanUp() { }

        public virtual void OnUpdate() { }

        public virtual void AttachEvents() { }

        public virtual void DetachEvents() { }

        #endregion
    }
}