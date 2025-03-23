using Database;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public abstract class BaseEditorWindow<T1, T2> : OdinMenuEditorWindow where T1 : BaseDatabase<T2> where T2 : BaseDataOfDatabase, new()
    {
        #region VARIABLES

        private T1 database;

        #endregion

        #region PROPERTIES

        protected abstract string DatabaseName { get; }

        private T1 Database
        {
            get
            {
                if (database == null)
                {
                    var propertyObject = MainDatabases.Instance.GetPropertyByType(typeof(T1));
                    if (propertyObject != null)
                        database = (T1)propertyObject;
                }
                return database;
            }
        }


        #endregion

        #region METHODS

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTreeDrawingConfig style = new OdinMenuTreeDrawingConfig();
            OdinMenuTree tree = new OdinMenuTree(false, style);

            if (Database.Datas != null)
            {
                for (int i = 0; i < Database.Datas.Count; i++)
                {
                    var resourceData = Database.Datas[i];
                    tree.AddObjectAtPath(i + " - " + resourceData.DataName, resourceData);
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

            if (GUILayout.Button("Add new data", SirenixGUIStyles.Button))
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

            Debug.Log($"{DatabaseName} saving");
            SaveThisAsset();
        }

        private void SaveThisAsset()
        {
            if (EditorApplication.isUpdating)
            {
                return;
            }

            EditorUtility.SetDirty(Database);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void AddNewResourceData()
        {
            Database.AddNewItemData(new T2());
            ForceMenuTreeRebuild();
        }

        private void DeleteResourceData()
        {
            T2 dataToDelete = MenuTree.Selection.SelectedValue as T2;

            if (dataToDelete != null)
            {
                if (EditorUtility.DisplayDialog("Are you sure?", $"Delete this item data?: {dataToDelete.DataName}", "Yes", "Cancel") == false)
                    return;

                Database.DeleteItemData(dataToDelete);
                ForceMenuTreeRebuild();
            }
        }

        #endregion

    }
}