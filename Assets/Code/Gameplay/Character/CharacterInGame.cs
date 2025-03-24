using Database;
using Database.Character;
using Gameplay.Player.Controller;
using Gameplay.Player.Controller.Effects;
using Gameplay.Player.Controller.Inventory;
using Gameplay.Player.Controller.Values;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Player
{
    public class CharacterInGame : IAttachableEvents
    {
        #region VARIABLES

        [SerializeField] protected List<ControllerBase> controllers;
        [SerializeField] private int dataId;

        [SerializeField, FoldoutGroup("Controllers")] protected ValuesController valuesController;
        [SerializeField, FoldoutGroup("Controllers")] protected InventoryController inventoryController;
        [SerializeField, FoldoutGroup("Controllers")] protected EffectsController effectsController;

        private CharacterData data;

        #endregion

        #region PROPERTIES

        public ValuesController ValuesController => valuesController;
        public InventoryController InventoryController => inventoryController;
        public EffectsController EffectsController => effectsController;

        public CharacterData Data
        {
            get
            {
                if (data == null)
                    data = MainDatabases.Instance.CharactersDatabase.GetItemData(dataId);
                return data;
            }
        }

        #endregion

        #region CONSTRUCTORS

        public CharacterInGame()
        {
            CreateControllers();
        }

        #endregion

        #region METHODS

        public void Initialize()
        {
            SetControllers();
            InitializeControllers();
        }

        public void CleanUp()
        {
            CleanUpControllers();
        }


        public void SetData(CharacterData data)
        {
            dataId = data.Id;
            this.data = data;
        }


        public void AttachEvents()
        {
            AttachControllers();
        }

        public void DetachEvents()
        {
            DetachControllers();
        }

        protected virtual void CreateControllers()
        {
            valuesController = new ValuesController();
            inventoryController = new InventoryController();
            effectsController = new EffectsController();
        }

        protected virtual void SetControllers()
        {
            controllers = new();

            controllers.Add(valuesController);
            controllers.Add(inventoryController);
            controllers.Add(effectsController);
        }
        protected void CleanUpControllers() => controllers.ForEach(m => m.CleanUp());

        private void InitializeControllers() => controllers.ForEach(m => m.Initialize(this));

        private void AttachControllers() => controllers.ForEach(m => m.AttachEvents());

        private void DetachControllers() => controllers.ForEach(m => m.DetachEvents());

        #endregion
    }
}
