using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Database.Recipes
{
    [CreateAssetMenu(menuName = "Database/RecipesDatabase", fileName = "RecipesDatabase")]
    public class RecipesDatabase : BaseDatabase<RecipesCategory>
    {
        #region VARIABLES

        public const string GET_RECIPES_DATA_METHOD = "@" + nameof(RecipesDatabase) + "." + nameof(GetRecipeDatas) + "()";

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public static IEnumerable GetRecipeDatas()
        {
            ValueDropdownList<int> values = new();
            foreach (RecipesCategory recipeCategory in MainDatabases.Instance.RecipesDatabase.Datas)
                values.Add(recipeCategory.DataName, recipeCategory.Id);

            return values;
        }

        #endregion
    }
}
