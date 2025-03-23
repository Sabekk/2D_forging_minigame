using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Resources
{
    [CreateAssetMenu(menuName = "Database/ResourcesDatabase", fileName = "ResourcesDatabase")]
    public class ResourcesDatabase : ScriptableObject
    {
        #region VARIABLES

        [SerializeField] private List<ResourceData> resourceDatas;

        #endregion

        #region PROPERTIES

        public List<ResourceData> ResourceDatas => resourceDatas;

        #endregion

        #region METHODS

        public ResourceData GetItemData(int id)
        {
            return ResourceDatas.Find(x => x.IdEquals(id));
        }

        #endregion
    }
}
