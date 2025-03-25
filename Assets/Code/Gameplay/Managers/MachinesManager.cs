using Database;
using Database.Recipes;
using Gameplay.Machines;
using Gameplay.Management.Characters;
using Gameplay.Management.Items;
using Gameplay.Management.Timing;
using Gameplay.Management.Unlocks;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Management.Machines
{
    public class MachinesManager : GameplayManager<MachinesManager>
    {
        #region ACTION

        public event Action<MachineInGame> OnMachineUnlocked;

        #endregion

        #region VARIABLES

        [SerializeField] private List<MachineInGame> machinesInGame;
        [SerializeField, FoldoutGroup("Debug"), ValueDropdown(RecipesDatabase.GET_RECIPE_CATEGORY_DATA_METHOD)] private int debugRecipeCategory;
        [SerializeField, FoldoutGroup("Debug"), ValueDropdown("@MainDatabases.Instance.RecipesDatabase.GetRecipeFromCategory(debugRecipeCategory)")] private int debugRecipeFromCategory;

        #endregion

        #region PROPERTIES

        public List<MachineInGame> MachinesInGame => machinesInGame;

        #endregion

        #region METHODS

        public override void Initialzie()
        {
            base.Initialzie();
            machinesInGame = new();
            foreach (var machineData in MainDatabases.Instance.MachinesDatabase.Datas)
            {
                MachineInGame machine = new MachineInGame(machineData, CheckMachineIsUnlocked(machineData.Id));
                AttachEventsOfMachine(machine);
                machinesInGame.Add(machine);
            }
        }

        public override void AttachEvents()
        {
            base.AttachEvents();
            if (TimeManager.Instance)
                TimeManager.Instance.OnSecondPassed += HandleSecondPassed;

            if (UnlocksManager.Instance)
                UnlocksManager.Instance.MachinesUnlocks.OnUnlockedId += HandleMachineUnlocked;
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            if (TimeManager.Instance)
                TimeManager.Instance.OnSecondPassed -= HandleSecondPassed;

            if (UnlocksManager.Instance)
                UnlocksManager.Instance.MachinesUnlocks.OnUnlockedId -= HandleMachineUnlocked;

            foreach (var machine in machinesInGame)
                DetachEventsOfMachine(machine);
        }

        public float GetChanceForRecipe(RecipeData recipeData)
        {
            float characterChance = 0;
            if (CharacterManager.Instance)
                characterChance = CharacterManager.Instance.Player.ValuesController.CharacterValues.AdditionalSuccessChance.CurrentRawValue;
            float defaultChance = recipeData.SuccessChance;

            return Math.Clamp(characterChance + defaultChance, 0, 100);
        }

        public int GetTotalTimeForRecipe(RecipeData recipeData)
        {
            float reducedTime = 0;
            if (CharacterManager.Instance)
                reducedTime = CharacterManager.Instance.Player.ValuesController.CharacterValues.AdditionalProductionSpeed.CurrentValue;
            float defaultTime = recipeData.CraftingTime;

            return Mathf.RoundToInt(defaultTime - reducedTime);
        }

        private void AttachEventsOfMachine(MachineInGame machine)
        {
            machine.OnProductionFinished += HandleProductionFinished;
        }

        private void DetachEventsOfMachine(MachineInGame machine)
        {
            machine.OnProductionFinished -= HandleProductionFinished;
        }

        private bool CheckMachineIsUnlocked(int machineDataId)
        {
            if (UnlocksManager.Instance)
                return UnlocksManager.Instance.MachinesUnlocks.InUnlocked(machineDataId);

            return false;
        }

        #region HANDLERS

        private void HandleProductionFinished(MachineInGame machine, int itemId, bool isSuccess)
        {
            if (isSuccess == false)
                return;

            if (CharacterManager.Instance && ItemsManager.Instance)
                ItemsManager.Instance.AddItemToCharacter(itemId, CharacterManager.Instance.Player);
        }

        private void HandleSecondPassed()
        {
            foreach (var machine in machinesInGame)
                machine.TryChangeProduceTime();
        }

        private void HandleMachineUnlocked(int machineDataId)
        {
            MachineInGame machineInGame = machinesInGame.Find(x => x.MachineData.IdEquals(machineDataId));

            if (machineInGame == null)
                return;

            machineInGame.ToggleUnlockState(true);
            OnMachineUnlocked?.Invoke(machineInGame);
        }

        #endregion

        [Button, FoldoutGroup("Debug")]
        private void DebugProduce()
        {
            RecipesCategory category = MainDatabases.Instance.RecipesDatabase.GetItemData(debugRecipeCategory);
            RecipeData recipeData = category.RecipeDatas.GetElementById(debugRecipeFromCategory);
            MachineInGame machine = machinesInGame.Find(x => x.MachineData.IdEquals(category.IntendedMachineDataId));
            if (machine != null)
                machine.StartProduce(recipeData);
        }

        #endregion
    }
}
