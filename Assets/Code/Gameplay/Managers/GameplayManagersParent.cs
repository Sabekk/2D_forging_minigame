using Gameplay.Effects;

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
            managers.Add(EffectsManager.Instance);
        }

        #endregion
    }
}
