using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Recipes
{
    [CreateAssetMenu(menuName = "Database/RecipesDatabase", fileName = "RecipesDatabase")]
    public class RecipesDatabase : BaseDatabase<RecipesCategory>
    {
        #region VARIABLES

        public const string GET_RECIPE_CATEGORY_DATA_METHOD = "@" + nameof(RecipesDatabase) + "." + nameof(GetRecipeCategoryDatas) + "()";

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public RecipeData FindRecipeData(int recipeId)
        {
            RecipeData recipe = null;
            foreach (var category in Datas)
            {
                recipe = category.RecipeDatas.GetElementById(recipeId);
                if (recipe != null)
                    return recipe;
            }
            return recipe;
        }

        public RecipeData FindRecipeData(int recipeId, int recipeCategoryId)
        {
            RecipesCategory category = GetItemData(recipeCategoryId);
            if (category == null)
            {
                Debug.LogError($"Wrong recipe category id: [{recipeCategoryId}]. Check database");
                return null;
            }

            RecipeData recipe = category.RecipeDatas.Find(x => x.IdEquals(recipeId));
            if (recipe == null)
            {
                Debug.LogError($"Wrong recipe data id: [{recipeId}]. Check database");
                return null;
            }

            return recipe;
        }

        public IList<ValueDropdownItem<int>> GetRecipeFromCategory(int recipeCategoryId)
        {
            List<ValueDropdownItem<int>> values = new();

            foreach (var category in Datas)
            {
                if (category.IdEquals(recipeCategoryId))
                    foreach (var recipe in category.RecipeDatas)
                        values.Add(new ValueDropdownItem<int>(recipe.DataName, recipe.Id));
            }

            return values;
        }


        public static IEnumerable GetRecipeCategoryDatas()
        {
            ValueDropdownList<int> values = new();
            foreach (RecipesCategory recipeCategory in MainDatabases.Instance.RecipesDatabase.Datas)
                values.Add(recipeCategory.DataName, recipeCategory.Id);

            return values;
        }

        #endregion
    }
}
