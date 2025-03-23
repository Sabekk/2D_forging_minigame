using Database.Recipes;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class RecipesEditorWindow : BaseEditorWindow<RecipesDatabase, RecipesCategory>
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        protected override string DatabaseName => "Recipes database";

        #endregion

        #region METHODS

        [MenuItem("Tools/Recipes editor")]
        public static void OpenWindow()
        {
            RecipesEditorWindow window = GetWindow<RecipesEditorWindow>(string.Format($"Recipes editor"));
            window.minSize = new Vector2(800f, 800f);
            window.maxSize = new Vector2(800f, 800f);

            window.Show();
        }

        #endregion
    }
}