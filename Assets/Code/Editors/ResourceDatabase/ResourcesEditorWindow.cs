using Database;
using Database.Resources;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ResourcesEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Resouerce Editor")]

        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public static void OpenWindow()
        {
            ItemsEditorWindow window = GetWindow<ItemsEditorWindow>(string.Format("Resouerce data editor"));
            window.minSize = new Vector2(800f, 800f);
            window.maxSize = new Vector2(800f, 800f);

            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTreeDrawingConfig style = new OdinMenuTreeDrawingConfig();
            OdinMenuTree tree = new OdinMenuTree(false, style);

            if (MainDatabases.Instance.ResourcesDatabase.ResourceDatas != null)
            {
                for (int i = 0; i < MainDatabases.Instance.ResourcesDatabase.ResourceDatas.Count; i++)
                {
                    ResourceData itemData = MainDatabases.Instance.ResourcesDatabase.ResourceDatas[i];
                    tree.AddObjectAtPath(i + " - " + itemData.ItemName, itemData);
                }
            }

            return tree;
        }

        protected override void OnImGUI()
        {
            base.OnImGUI();

            EditorGUILayout.LabelField("ToolBox", SirenixGUIStyles.BoldLabelCentered);
            SirenixEditorGUI.HorizontalLineSeparator(Color.white, 2);

            SirenixEditorGUI.BeginHorizontalToolbar();

            if (GUILayout.Button("Add new resource data", SirenixGUIStyles.Button))
            {
                AddNewResourceData();
            }

            if (GUILayout.Button("Delete selected data", SirenixGUIStyles.Button))
            {
                DeleteResourceData();
            }

            SirenixEditorGUI.EndHorizontalToolbar();

            SirenixEditorGUI.BeginHorizontalToolbar();

            if (GUILayout.Button("Refresh", SirenixGUIStyles.Button))
            {
                ForceMenuTreeRebuild();
            }

            SirenixEditorGUI.EndHorizontalToolbar();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Debug.Log("Resource database saving");
            SaveThisAsset();
        }

        private void SaveThisAsset()
        {
            if (EditorApplication.isUpdating)
            {
                return;
            }

            EditorUtility.SetDirty(MainDatabases.Instance.ItemsDatabase);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void AddNewResourceData()
        {
            MainDatabases.Instance.ResourcesDatabase.AddNewResourceData(new ResourceData());
            ForceMenuTreeRebuild();
        }

        private void DeleteResourceData()
        {
            ResourceData resourceDataToDelete = MenuTree.Selection.SelectedValue as ResourceData;

            if (resourceDataToDelete != null)
            {
                if (EditorUtility.DisplayDialog("Are you sure?", $"Delete this item data?: {resourceDataToDelete.ItemName}", "Yes", "Cancel") == false)
                    return;

                MainDatabases.Instance.ResourcesDatabase.DeleteResourceData(resourceDataToDelete);
                ForceMenuTreeRebuild();
            }
        }

        #endregion

    }
}