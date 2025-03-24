using Database.Items;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ItemsEditorWindow : BaseEditorWindow<ItemsDatabase, ItemCategory>
    {

        #region VARIABLES

        #endregion

        #region PROPERTIES

        protected override string DatabaseName => "Items database";

        #endregion

        #region METHODS

        [MenuItem("Tools/Items editor")]
        public static void OpenWindow()
        {
            ItemsEditorWindow window = GetWindow<ItemsEditorWindow>(string.Format($"Items editor"));
            window.minSize = new Vector2(800f, 800f);
            window.maxSize = new Vector2(800f, 800f);

            window.Show();
        }

        #endregion

    }
}