using Database;
using Gameplay.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Management.Resources
{
    public class ResourcesManager : GameplayManager<ResourcesManager>
    {
        #region VARIABLES

        [SerializeField] private List<ResourceInGame> resources;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public override void Initialzie()
        {
            base.Initialzie();
            resources = new();
            CreateResources();
        }

        public void AddResource(int delta, ResourceInGame resource)
        {
            resource.AddValue(delta);
        }

        public void AddResource(int delta, int dataId)
        {
            ResourceInGame resource = resources.Find(x => x.ResourceData.IdEquals(dataId));
            if (resource != null)
                AddResource(delta, resource);
            else
            {
                Debug.LogError($"Resource didnt found. Check resources data. Id: {dataId}");
            }
        }

        private void CreateResources()
        {
            foreach (var resourceData in MainDatabases.Instance.ResourcesDatabase.Datas)
                resources.Add(new ResourceInGame(resourceData));
        }

        #endregion
    }
}