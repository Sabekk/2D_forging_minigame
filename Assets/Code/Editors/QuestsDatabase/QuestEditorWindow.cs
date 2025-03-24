using Database.Quests;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class QuestEditorWindow : BaseEditorWindow<QuestsDatabase, QuestData>
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        protected override string DatabaseName => "Quests database";

        #endregion

        #region METHODS

        [MenuItem("Tools/Quests editor")]
        public static void OpenWindow()
        {
            QuestEditorWindow window = GetWindow<QuestEditorWindow>(string.Format($"Quests editor"));
            window.minSize = new Vector2(800f, 800f);
            window.maxSize = new Vector2(800f, 800f);

            window.Show();
        }

        #endregion
    }
}