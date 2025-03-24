using Database;
using Gameplay.Management.Timing;
using Gameplay.Resources;
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

        public List<ResourceInGame> Resources=> resources;

        #endregion

        #region METHODS

        public override void Initialzie()
        {
            base.Initialzie();
            resources = new();
            CreateResources();
        }

        public override void AttachEvents()
        {
            base.AttachEvents();
            if (TimeManager.Instance)
                TimeManager.Instance.OnSeccondPassed += HandleSeccondPassed;

            foreach (var resource in Resources)
                resource.OnTimerUpdated += () => HandleTimerResourceUpdated(resource);
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            if (TimeManager.Instance)
                TimeManager.Instance.OnSeccondPassed -= HandleSeccondPassed;

            foreach (var resource in Resources)
                resource.OnTimerUpdated -= () => HandleTimerResourceUpdated(resource);
        }

        public void AddResource(int delta, ResourceInGame resource)
        {
            resource.AddValue(delta);
        }

        public void AddResource(int delta, int dataId)
        {
            ResourceInGame resource = Resources.Find(x => x.ResourceData.IdEquals(dataId));
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
                Resources.Add(new ResourceInGame(resourceData));
        }

        #region HANDLERS

        private void HandleSeccondPassed()
        {
            foreach (var resource in Resources)
                resource.UpdateTime();
        }

        private void HandleTimerResourceUpdated(ResourceInGame resource)
        {
            if (resource.TimerShouldBeReseted)
            {
                resource.AddValue(resource.ResourceData.ReceiveCount);
                resource.ResetTimer();
            }
        }

        #endregion

        #endregion
    }
}