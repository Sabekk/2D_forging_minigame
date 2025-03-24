using Gameplay.Management.Unlocks;

namespace Gameplay.Player.Controller.Unlocks
{
    public class UnlocksController : ControllerBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public override void Initialize(CharacterInGame character)
        {
            base.Initialize(character);
            UnlockDefaultMachines();
        }

        private void UnlockDefaultMachines()
        {
            if (UnlocksManager.Instance)
            {
                foreach (var unlockedMachineId in Character.Data.StartingUnlockedMachines)
                    UnlocksManager.Instance.MachinesUnlocks.UnlockId(unlockedMachineId);
            }
        }

        #endregion
    }
}