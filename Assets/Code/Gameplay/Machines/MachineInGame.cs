using Database;
using Database.Machines;
using Database.Recipes;
using Gameplay.Character;
using Gameplay.Management.Characters;
using Gameplay.Management.Machines;
using Gameplay.Management.Resources;
using System;
using UnityEngine;

namespace Gameplay.Machines
{
    [Serializable]
    public class MachineInGame
    {
        #region ACTION

        public event Action OnProductionStarted;
        public event Action<MachineInGame, int, bool> OnProductionFinished;
        public event Action OnProductionProgressed;

        #endregion

        #region VARIABLES

        [SerializeField] private int dataId;
        [SerializeField] private bool isUnlocked;
        [SerializeField] private MachineProduction currentProduction;

        private MachineData machineData;

        #endregion

        #region PROPERTIES

        public MachineData MachineData
        {
            get
            {
                if (machineData == null)
                    machineData = MainDatabases.Instance.MachinesDatabase.GetItemData(dataId);
                return machineData;
            }
        }

        public MachinesManager Manager => MachinesManager.Instance;

        public bool IsUnlocked => isUnlocked;
        public bool IsProducting => currentProduction != null;

        #endregion

        #region CONSTRUCTORS

        public MachineInGame() { }
        public MachineInGame(MachineData machineData, bool isUnlocked)
        {
            this.machineData = machineData;
            dataId = machineData.Id;
            this.isUnlocked = isUnlocked;
        }

        #endregion

        #region METHODS

        public void StartProduce(RecipeData recipe)
        {
            if (CanProduce(recipe) == false)
                return;

            PrepareToProduce(recipe);

            currentProduction = new MachineProduction(recipe.CreatedItemDataId, Manager.GetChanceForRecipe(recipe), Manager.GetTotalTimeForRecipe(recipe));
            OnProductionStarted?.Invoke();
        }

        public void PrepareToProduce(RecipeData recipe)
        {
            foreach (var needResource in recipe.Resources)
                ResourcesManager.Instance.AddResource(-needResource.NeededCount, needResource.ResourceDataId);

            foreach (var item in recipe.Items)
                CharacterManager.Instance.Player.InventoryController.InventoryModule.RemoveItem(item.ItemDataId, item.NeededCount);
        }

        public void ToggleUnlockState(bool state)
        {
            isUnlocked = state;
        }

        public void TryChangeProduceTime()
        {
            if (isUnlocked == false)
                return;

            if (currentProduction == null)
                return;

            currentProduction.ProgressProduction();

            if (currentProduction.CraftingEnded)
            {
                bool isSuccess = UnityEngine.Random.Range(0, 100f) <= currentProduction.SuccessChance;
                OnProductionFinished?.Invoke(this, currentProduction.CraftingItemId, isSuccess);
                currentProduction = null;
            }
            else
                OnProductionProgressed?.Invoke();
        }

        public bool CanProduce(RecipeData recipe)
        {
            foreach (var needResource in recipe.Resources)
                if (ResourcesManager.Instance.CanHandleResource(needResource.ResourceDataId, needResource.NeededCount) == false)
                    return false;

            foreach (var item in recipe.Items)
                if (CharacterManager.Instance.Player.InventoryController.InventoryModule.CanHandleItemsInStack(item.ItemDataId, item.NeededCount) == false)
                    return false;

            return true;
        }

        #endregion
    }
}
