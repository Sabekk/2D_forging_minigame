using Database;
using Database.Quests;
using Gameplay.Management.Timing;
using Gameplay.Quests;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Management.Quests
{
    public class QuestsManager : GameplayManager<QuestsManager>
    {
        #region ACTIONS

        public event Action<QuestInGame> OnQuestAdded;
        public event Action<QuestStatus, QuestInGame> OnQuestChangedStatus;

        #endregion

        #region VARIABLES

        [SerializeField, ValueDropdown(QuestsDatabase.GET_QUESTS_DATA_METHOD)] private List<int> debugQuests;
        [SerializeField] private List<QuestInGame> allQuests;

        #endregion

        #region PROPERTIES

        public List<QuestInGame> AllQuests => allQuests;

        #endregion

        #region METHODS

        public override void CleanUp()
        {
            base.CleanUp();
            foreach (QuestInGame quest in allQuests)
                quest.Dispose();
        }

        public override void SetStartingValues()
        {
            base.SetStartingValues();

            if (debugQuests != null)
                foreach (var debugQuest in debugQuests)
                    AddQuest(debugQuest);
        }

        public void AddQuest(int questDataId)
        {
            QuestData questData = MainDatabases.Instance.QuestsDatabase.GetItemData(questDataId);
            AddQuest(questData);
        }

        public void AddQuest(QuestData questData)
        {
            if (questData == null)
                return;

            QuestInGame questInGame = new QuestInGame(questData);
            AddQuest(questInGame);
        }

        public void AddQuest(QuestInGame questInGame)
        {
            if (questInGame == null)
                return;

            questInGame.Initialize();
            allQuests.Add(questInGame);

            OnQuestAdded?.Invoke(questInGame);
        }

        public override void AttachEvents()
        {
            base.AttachEvents();
            if (TimeManager.Instance)
                TimeManager.Instance.OnSecondPassed += HandleSecondPassed;
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            if (TimeManager.Instance)
                TimeManager.Instance.OnSecondPassed -= HandleSecondPassed;
        }

        #region HANDLERS

        private void HandleSecondPassed()
        {
            foreach (var quest in allQuests)
                quest.TryReduceTimeLeft();
        }

        #endregion

        #endregion
    }
}