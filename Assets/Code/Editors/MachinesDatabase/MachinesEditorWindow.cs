using Database.Machines;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class MachinesEditorWindow : BaseEditorWindow<MachinesDatabase, MachineData>
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        protected override string DatabaseName => "Machines database";

        #endregion

        #region METHODS

        [MenuItem("Tools/Machines editor")]
        public static void OpenWindow()
        {
            MachinesEditorWindow window = GetWindow<MachinesEditorWindow>(string.Format($"Machines editor"));
            window.minSize = new Vector2(800f, 800f);
            window.maxSize = new Vector2(800f, 800f);

            window.Show();
        }

        #endregion
    }
}