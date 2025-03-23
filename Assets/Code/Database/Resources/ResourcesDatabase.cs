using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Resources
{
    [CreateAssetMenu(menuName = "Database/ResourcesDatabase", fileName = "ResourcesDatabase")]
    public class ResourcesDatabase : BaseDatabase<ResourceData>
    {
        #region VARIABLES

        public const string GET_RESOURCE_DATA_METHOD = "@" + nameof(ResourcesDatabase) + "." + nameof(GetResourceDatas) + "()";

        [SerializeField] private List<ResourceData> resourceDatas;

        #endregion

        #region PROPERTIES

        public List<ResourceData> ResourceDatas => resourceDatas;

        #endregion

        #region METHODS

        public void AddNewResourceData(ResourceData resourceData)
        {
            resourceDatas.Add(resourceData);
        }

        public void DeleteResourceData(ResourceData resourceData)
        {
            resourceDatas.Remove(resourceData);
        }

        public ResourceData GetResourceData(int id)
        {
            return ResourceDatas.Find(x => x.IdEquals(id));
        }

        public static IEnumerable GetResourceDatas()
        {
            ValueDropdownList<int> values = new();
            foreach (ResourceData resourceData in MainDatabases.Instance.ResourcesDatabase.Datas)
                values.Add(resourceData.DataName, resourceData.Id);

            return values;
        }

        #endregion
    }
}
