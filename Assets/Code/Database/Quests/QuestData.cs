using Gameplay.Effects;
using Gameplay.Quests;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Quests
{
    [Serializable]
    public class QuestData : BaseDataOfDatabase
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Base setting")] private bool hasLimitedTime;
        [SerializeField, FoldoutGroup("Base setting"), ShowIf(nameof(hasLimitedTime)), SuffixLabel("seconds")] private int limitedTime;
        [SerializeReference, FoldoutGroup("Objective setting")] List<QuestObjectiveBase> objectivesData;

        [SerializeReference] private List<GlobalEffect> globalRewards;
        [SerializeReference] private List<GlobalEffect> globalPenalty;
        [SerializeReference] private List<CharacterEffect> playerRewards;
        [SerializeReference] private List<CharacterEffect> playerPenalty;

        #endregion

        #region PROPERTIES

        public bool HasLimitedTime => hasLimitedTime;
        public int LimitedTime => limitedTime;
        public List<QuestObjectiveBase> ObjectivesData => objectivesData;
        public List<GlobalEffect> GlobalRewards => globalRewards;
        public List<GlobalEffect> GlobalPenalty => globalPenalty;
        public List<CharacterEffect> PlayerRewards => playerRewards;
        public List<CharacterEffect> PlayerPenalty => playerPenalty;

        #endregion

        #region METHODS

        #endregion
    }
}
