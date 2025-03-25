using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using Database.Machines;
using Gameplay.Machines;
using Gameplay.Management.Machines;
using ObjectPooling;
using Gameplay.Management.UI;
using UI.Window.Machines;

namespace Gameplay.HUD
{
    [RequireComponent(typeof(Button))]
    public class MachineButton : MonoBehaviour, IPoolable
    {
        #region VARIABLES

        [SerializeField, ValueDropdown(ObjectPoolDatabase.GET_POOL_UI_WINDOW_METHOD)] private int windowId;

        [SerializeField] private Button button;
        [SerializeField] private Image mainIcon;
        [SerializeField] private Image blockedIcon;
        [SerializeField] private Image timerIcon;
        [SerializeField] private TextMeshProUGUI timeLeftText;

        #endregion

        #region PROPERTIES

        private MachineInGame Machine { get; set; }
        private MachinesManager Manager => MachinesManager.Instance;

        public PoolObject Poolable { get; set; }

        #endregion

        #region METHODS

        public void SetMachine(MachineInGame machine)
        {
            if (Machine != null)
                DetachEvents();

            Machine = machine;
            SetMachineImage();
            ToggleBlockState(Machine.IsUnlocked);
            SetProductionState(Machine.IsProducting);

            AttachEvents();
        }

        public void AssignPoolable(PoolObject poolable)
        {
            Poolable = poolable;
        }

        private void AttachEvents()
        {
            Machine.OnProductionStarted += HandleProductionStarted;
            Machine.OnProductionFinished += HandleProductionFinished;
            Machine.OnProductionProgressed += HandleProductionProgressed;

            if (Manager != null)
                Manager.OnMachineUnlocked += HandleMachineUnlocked;
        }

        private void DetachEvents()
        {
            Machine.OnProductionStarted -= HandleProductionStarted;
            Machine.OnProductionFinished -= HandleProductionFinished;
            Machine.OnProductionProgressed -= HandleProductionProgressed;

            if (Manager != null)
                Manager.OnMachineUnlocked -= HandleMachineUnlocked;
        }

        private void SetMachineImage()
        {
            mainIcon.sprite = Machine.MachineData.Icon;
        }

        private void SetProductionState(bool isProducing)
        {
            timerIcon.gameObject.SetActive(isProducing);
            timeLeftText.gameObject.SetActive(isProducing);
        }

        private void ToggleBlockState(bool isUnlocked)
        {
            button.interactable = isUnlocked;
            blockedIcon.gameObject.SetActive(!isUnlocked);

            if (!isUnlocked)
                SetProductionState(false);
        }

        private void UpdateTimer()
        {
            if (Machine.IsProducting == false)
            {
                SetProductionState(false);
                return;
            }

            timerIcon.fillAmount = Machine.CurrentProduction.PercentageProgress;
            timeLeftText.SetText(Machine.CurrentProduction.TimeLeft.ToString());
        }

        #region HANDLERS

        private void HandleMachineUnlocked(MachineInGame machine)
        {
            if (Machine.MachineData.IdEquals(machine.MachineData.Id))
                ToggleBlockState(true);
        }

        private void HandleProductionStarted()
        {
            SetProductionState(true);
            UpdateTimer();
        }

        private void HandleProductionFinished(MachineInGame machine, int itemId, bool isSuccessed)
        {
            //TODO animation of success/fail
            SetProductionState(false);
        }

        private void HandleProductionProgressed()
        {
            UpdateTimer();
        }

        #endregion

        #region UI_HANDLERS

        public void HandleButtonClick()
        {
            UIManager.Instance.OpenWindow<MachinesWindow>(windowId);
        }

        #endregion

        #endregion
    }
}
