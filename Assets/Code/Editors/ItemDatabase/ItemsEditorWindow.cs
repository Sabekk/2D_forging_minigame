using Database;
using Database.Items;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ItemsEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Items Editor")]

        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public static void OpenWindow()
        {
            ItemsEditorWindow window = GetWindow<ItemsEditorWindow>(string.Format("Items data editor"));
            window.minSize = new Vector2(800f, 800f);
            window.maxSize = new Vector2(800f, 800f);

            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTreeDrawingConfig style = new OdinMenuTreeDrawingConfig();
            OdinMenuTree tree = new OdinMenuTree(false, style);

            if (MainDatabases.Instance.ItemsDatabase.ItemDatas != null)
            {
                for (int i = 0; i < MainDatabases.Instance.ItemsDatabase.ItemDatas.Count; i++)
                {
                    ItemData itemData = MainDatabases.Instance.ItemsDatabase.ItemDatas[i];
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

            if (GUILayout.Button("Add new item data", SirenixGUIStyles.Button))
            {
                AddNewItemData();
            }

            if (GUILayout.Button("Delete selected data", SirenixGUIStyles.Button))
            {
                DeleteItemData();
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

            Debug.Log("Items database saving");
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

        private void AddNewItemData()
        {
            MainDatabases.Instance.ItemsDatabase.AddNewItemData(new ItemData());
            ForceMenuTreeRebuild();
        }

        private void DeleteItemData()
        {
            ItemData itemDataToDelete = MenuTree.Selection.SelectedValue as ItemData;

            if (itemDataToDelete != null)
            {
                if (EditorUtility.DisplayDialog("Are you sure?", $"Delete this item data?: {itemDataToDelete.ItemName}", "Yes", "Cancel") == false)
                    return;

                MainDatabases.Instance.ItemsDatabase.DeleteItemData(itemDataToDelete);
                ForceMenuTreeRebuild();
            }
        }

        #endregion

    }
}