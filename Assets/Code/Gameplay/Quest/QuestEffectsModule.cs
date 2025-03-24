using Gameplay.Management.Quests;
using UnityEngine;

namespace Gameplay.Quests
{
    public class QuestEffectsModule
    {
        #region VARIABLES

        [SerializeField] int questId;

        private QuestInGame questInGame;

        #endregion

        #region PROPERTIES

        private QuestInGame QuestInGame
        {
            get
            {
                if (QuestsManager.Instance == null)
                    return null;

                if (questInGame == null)
                    questInGame = QuestsManager.Instance.AllQuests.GetElementById(questId);
                return questInGame;
            }
        }

        #endregion

        #region CONSTRUCTORS

        public QuestEffectsModule() { }
        public QuestEffectsModule(QuestInGame questInGame)
        {
            this.questInGame = questInGame;
        }

        #endregion

        #region METHODS
        public string GetEffectsDescription(bool positiveEffects)
        {
            return "";
        }

        public void HandleTaskCompleted()
        {
            //TODO Positive effects
        }

        public void HandleTaskFailed()
        {
            //TODO Negative effects
        }

        #endregion
    }
}