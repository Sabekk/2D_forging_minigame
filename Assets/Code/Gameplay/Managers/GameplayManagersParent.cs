using Gameplay.Management.Characters;
using Gameplay.Management.Effects;
using Gameplay.Management.Items;
using Gameplay.Management.Machines;
using Gameplay.Management.Quests;
using Gameplay.Management.Resources;
using Gameplay.Management.Timing;
using Gameplay.Management.Unlocks;

namespace Gameplay.Management
{
    public class GameplayManagersParent : ManagersParent
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        protected override void SetManagers()
        {
            managers.Add(TimeManager.Instance);
            managers.Add(EffectsManager.Instance);
            managers.Add(CharacterManager.Instance);
            managers.Add(ResourcesManager.Instance);
            managers.Add(UnlocksManager.Instance);
            managers.Add(MachinesManager.Instance);
            managers.Add(ItemsManager.Instance);
            managers.Add(QuestsManager.Instance);
        }

        #endregion
    }
}
