using Gameplay.Player.Controller.Module;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay.Player.Controller
{
    public class ControllerBase: IAttachableEvents
    {
        #region VARIABLES

        [SerializeField, HideInInspector] private CharacterInGame character;
        [SerializeField, HideInInspector] protected List<ControllerModuleBase> modules;

        #endregion

        #region PROPERTIES

        public CharacterInGame Character => character;

        #endregion

        #region CONSTRUCTORS

        public ControllerBase()
        {
            CreateModules();
        }

        #endregion

        #region METHODS

        public virtual void Initialize(CharacterInGame character)
        {
            this.character = character;
            SetModules();

            modules.ForEach(c => c.Initialize(character));
        }

        public virtual void CleanUp() => modules.ForEach(c => c.CleanUp());

        public virtual void OnUpdate() => modules.ForEach(c => c.OnUpdate());

        public virtual void CreateModules() { }

        public virtual void SetModules() => modules = new();

        public virtual void AttachEvents() => modules.ForEach(c => c.AttachEvents());

        public virtual void DetachEvents() => modules.ForEach(c => c.DetachEvents());

        #endregion
    }
}