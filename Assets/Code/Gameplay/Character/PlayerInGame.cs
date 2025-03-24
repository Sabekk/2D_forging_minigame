using Gameplay.Player.Controller.Unlocks;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerInGame : CharacterInGame
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Controllers")] protected UnlocksController unlocksController;

        #endregion

        #region PROPERTIES

        public UnlocksController UnlocksController => unlocksController;

        #endregion

        #region METHODS

        protected override void CreateControllers()
        {
            base.CreateControllers();
            unlocksController = new UnlocksController();

        }

        protected override void SetControllers()
        {
            base.SetControllers();
            controllers.Add(unlocksController);
        }

        #endregion
    }
}
