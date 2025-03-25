using Database.Machines;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Recipes
{
    [Serializable]
    public class RecipesCategory : BaseDataOfDatabase
    {
        #region VARIABLES

        [SerializeField, ValueDropdown(MachinesDatabase.GET_MACHINES_DATA_METHOD)] private int intendedMachineDataId;
        [SerializeField, OnValueChanged(nameof(SetCategoryForElements))] private List<RecipeData> recipeDatas;

        #endregion

        #region PROPERTIES

        public int IntendedMachineDataId => intendedMachineDataId;
        public List<RecipeData> RecipeDatas => recipeDatas;

        #endregion

        #region METHODS

        private void SetCategoryForElements()
        {
            foreach (var recipe in recipeDatas)
                recipe.SetCategoryId(Id);

        }

        #endregion
    }
}