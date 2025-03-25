using Database;
using Gameplay.Management.Characters;
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

        public List<ResourceInGame> Resources => resources;

        #endregion

        #region METHODS

        public override void Initialzie()
        {
            base.Initialzie();
            resources = new();
            CreateResources();
        }

        public override void SetStartingValues()
        {
            base.SetStartingValues();
            if (CharacterManager.Instance && CharacterManager.Instance.Player != null)
            {
                if (CharacterManager.Instance.Player.Data.StartingResources == null)
                    return;

                foreach (var startingResource in CharacterManager.Instance.Player.Data.StartingResources.Resources)
                {
                    int value = 0;
                    if (startingResource.RandomInRange == false)
                        value = startingResource.StartingValue;
                    else
                        value = Random.Range(startingResource.StartingValue, startingResource.StartingValueMax + 1);

                    AddResource(value, startingResource.ResourceDataId);
                }
            }
        }

        public override void AttachEvents()
        {
            base.AttachEvents();
            if (TimeManager.Instance)
                TimeManager.Instance.OnSecondPassed += HandleSecondPassed;

            foreach (var resource in Resources)
                resource.OnTimerUpdated += () => HandleTimerResourceUpdated(resource);
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            if (TimeManager.Instance)
                TimeManager.Instance.OnSecondPassed -= HandleSecondPassed;

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

        public bool CanHandleResource(int resourceDataId, int count)
        {
            ResourceInGame resource = resources.Find(x => x.ResourceData.IdEquals(resourceDataId));
            if (resource == null)
                return false;

            return resource.CurrentValue >= count;
        }

        private void CreateResources()
        {
            foreach (var resourceData in MainDatabases.Instance.ResourcesDatabase.Datas)
                Resources.Add(new ResourceInGame(resourceData));
        }

        #region HANDLERS

        private void HandleSecondPassed()
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