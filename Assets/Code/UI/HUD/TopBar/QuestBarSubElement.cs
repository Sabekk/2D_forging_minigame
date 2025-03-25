using Gameplay.Management.Quests;
using Gameplay.Quests;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Gameplay.HUD
{
    public class QuestBarSubElement : HUDBar
    {
        #region VARIABLES

        [SerializeField] private TextMeshProUGUI activeQuestsCount;

        #endregion

        #region PROPERTIES
        private QuestsManager Quests => QuestsManager.Instance;

        #endregion

        #region METHODS

        public override void Initialize()
        {
            base.Initialize();
            RefreshActiveQuests();
        }

        public override void AttachEvents()
        {
            base.AttachEvents();
            if (Quests)
            {
                Quests.OnQuestAdded += HandleQuestAdded;
                Quests.OnQuestChangedStatus += HandleQuestChangedStatus;
            }
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            if (Quests)
            {
                Quests.OnQuestAdded -= HandleQuestAdded;
                Quests.OnQuestChangedStatus -= HandleQuestChangedStatus;
            }
        }

        private void RefreshActiveQuests()
        {
            if (Quests == null)
                activeQuestsCount.SetText("0");
            else
                activeQuestsCount.SetText(Quests.AllQuests.Count(x => x.Status == QuestStatus.ACTIVE).ToString());
        }

        #region HANDLERS

        private void HandleQuestAdded(QuestInGame quest)
        {
            RefreshActiveQuests();
        }

        private void HandleQuestChangedStatus(QuestStatus status, QuestInGame quest)
        {
            RefreshActiveQuests();
        }

        #endregion

        #endregion


        #region UI_BUTTONS

        public void OpenQuestsWindow()
        {

        }

        #endregion
    }
}