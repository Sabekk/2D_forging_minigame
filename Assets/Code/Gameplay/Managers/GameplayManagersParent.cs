using Gameplay.Management.Characters;
using Gameplay.Management.Effects;
using Gameplay.Management.Timing;

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
        }

        #endregion
    }
}
