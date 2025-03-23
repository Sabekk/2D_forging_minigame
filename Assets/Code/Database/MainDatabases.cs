using Database.Items;
using Database.Machines;
using Database.Quests;
using Database.Recipes;
using Database.Resources;
using System;
using System.Reflection;
using UnityEngine;

namespace Database
{
    [CreateAssetMenu(menuName = "Database/MainDatabases", fileName = "MainDatabases")]
    public class MainDatabases : ScriptableSingleton<MainDatabases>
    {
        #region VARIABLES

        [SerializeField] private ItemsDatabase itemsDatabase;
        [SerializeField] private RecipesDatabase recipesDatabase;
        [SerializeField] private ResourcesDatabase resourcesDatabase;
        [SerializeField] private MachinesDatabase machinesDatabase;
        [SerializeField] private QuestsDatabase questsDatabase;

        #endregion

        #region PROPERTIES

        public new static MainDatabases Instance => GetInstance("Singletons/MainDatabases");


        public ItemsDatabase ItemsDatabase => itemsDatabase;
        public RecipesDatabase RecipesDatabase => recipesDatabase;
        public ResourcesDatabase ResourcesDatabase => resourcesDatabase;
        public MachinesDatabase MachinesDatabase => machinesDatabase;
        public QuestsDatabase QuestsDatabase => questsDatabase;

        #endregion

        #region METHODS

        public object GetPropertyByType(Type type)
        {
            PropertyInfo[] properties = typeof(MainDatabases).GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == type)
                {
                    return property.GetValue(Instance);
                }
            }

            return null;
        }

        #endregion
    }
}