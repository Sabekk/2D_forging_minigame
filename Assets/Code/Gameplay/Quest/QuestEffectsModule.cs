using Gameplay.Management.Characters;
using Gameplay.Management.Effects;
using Gameplay.Management.Quests;
using System.Text;
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
            StringBuilder builder = new();

            if (EffectsManager.Instance)
            {
                if (positiveEffects)
                {
                    builder.AppendLine(EffectsManager.Instance.GetEffectsDescription(QuestInGame.Data.GlobalRewards));
                    builder.AppendLine(EffectsManager.Instance.GetEffectsDescription(QuestInGame.Data.PlayerRewards, CharacterManager.Instance.Player));
                }
                else
                {
                    builder.AppendLine(EffectsManager.Instance.GetEffectsDescription(QuestInGame.Data.GlobalPenalty));
                    builder.AppendLine(EffectsManager.Instance.GetEffectsDescription(QuestInGame.Data.PlayerPenalty, CharacterManager.Instance.Player));
                }

            }
            return builder.ToString();
        }

        public void HandleTaskCompleted()
        {
            //TODO Add targets of effects
            if (EffectsManager.Instance)
            {
                EffectsManager.Instance.ExecuteEffects(QuestInGame.Data.GlobalRewards);

                if (CharacterManager.Instance)
                    EffectsManager.Instance.ExecuteEffects(QuestInGame.Data.PlayerRewards, CharacterManager.Instance.Player);
            }
        }

        public void HandleTaskFailed()
        {
            //TODO Add targets of effects

            if (EffectsManager.Instance)
            {
                EffectsManager.Instance.ExecuteEffects(QuestInGame.Data.GlobalPenalty);

                if (CharacterManager.Instance)
                    EffectsManager.Instance.ExecuteEffects(QuestInGame.Data.PlayerPenalty, CharacterManager.Instance.Player);
            }
        }

        #endregion
    }
}