using Database.Resources;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ResourcesEditorWindow : BaseEditorWindow<ResourcesDatabase, ResourceData>
    {

        #region VARIABLES

        #endregion

        #region PROPERTIES

        protected override string DatabaseName => "Resouerce database";

        #endregion

        #region METHODS

        [MenuItem("Tools/Resouerce Editor")]
        public static void OpenWindow()
        {
            ResourcesEditorWindow window = GetWindow<ResourcesEditorWindow>(string.Format("Resouerce data editor"));
            window.minSize = new Vector2(800f, 800f);
            window.maxSize = new Vector2(800f, 800f);

            window.Show();
        }

        #endregion

    }
}