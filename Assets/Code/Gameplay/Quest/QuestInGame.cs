using Database.Quests;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Quests
{
    [Serializable]
    public class QuestInGame : ElementInGameFromData<QuestData>, IDisposable, IIdEqualable
    {
        #region ACTIONS

        public static event Action<QuestInGame> OnStatusChanged;
        public static event Action<QuestInGame> OnValuesChanged;
        public static event Action<QuestInGame> OnTimeLeftReduced;

        #endregion

        #region VARIABLES

        [SerializeField] private int id;
        [SerializeField] private bool hasLimitedTime;
        [SerializeField] private int limitedTime;
        [SerializeField] private QuestStatus status;
        [SerializeField] private QuestEffectsModule effectsModule;

        [SerializeField] private List<QuestObjectiveBase> objectives;


        #endregion

        #region PROPERTIES

        public int Id => id;
        public QuestStatus Status => status;
        public bool HasLimitedTime => hasLimitedTime;

        #endregion

        #region CONSTRUCTORS

        public QuestInGame() { }
        public QuestInGame(QuestData questData) : base(questData)
        {
            id = Guid.NewGuid().GetHashCode();

            hasLimitedTime = questData.HasLimitedTime;
            limitedTime = questData.LimitedTime;

            status = QuestStatus.ACTIVE;

            objectives = new List<QuestObjectiveBase>();
            foreach (QuestObjectiveBase objective in questData.ObjectivesData)
                objectives.Add(QuestObjectiveBase.CreateInstance(objective));
        }

        #endregion

        #region METHODS

        public void Initialize()
        {
            if (status != QuestStatus.ACTIVE)
                return;

            InitializeObjectives();
        }

        public void Dispose()
        {
            CleanUpObjectives();
        }

        public void TryReduceTimeLeft()
        {
            if (Status != QuestStatus.ACTIVE)
                return;
            if (!HasLimitedTime)
                return;

            limitedTime--;
            limitedTime = Mathf.Clamp(limitedTime, 0, int.MaxValue);
            if (limitedTime == 0)
                SetStatus(QuestStatus.FAILED);
            else
                OnTimeLeftReduced?.Invoke(this);
        }

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        private void InitializeObjectives()
        {
            CleanUpObjectives();
            foreach (QuestObjectiveBase objective in objectives)
            {
                objective.Initialize();
                objective.OnValueChanged += HandleObjectiveValueChanged;
                objective.OnStatusChanged += HandleObjectiveStatusChanged;
            }
        }

        private void CleanUpObjectives()
        {
            foreach (QuestObjectiveBase objective in objectives)
            {
                objective.Dispose();
                objective.OnValueChanged -= HandleObjectiveValueChanged;
                objective.OnStatusChanged -= HandleObjectiveStatusChanged;
            }
        }

        protected void SetStatus(QuestStatus status)
        {
            if (this.status == status)
                return;

            this.status = status;

            switch (status)
            {
                case QuestStatus.FAILED:
                    effectsModule.HandleTaskFailed();
                    Dispose();
                    break;
                case QuestStatus.COMPLETED:
                    effectsModule.HandleTaskCompleted();
                    Dispose();
                    break;
            }

            OnValuesChanged?.Invoke(this);
            OnStatusChanged?.Invoke(this);
        }

        private bool AllObjectivesEnded()
        {
            foreach (var objective in objectives)
            {
                if (!objective.IsEnded)
                    return false;
            }
            return true;
        }

        #region HANDLERS

        private void HandleObjectiveValueChanged()
        {
            OnValuesChanged?.Invoke(this);
        }

        private void HandleObjectiveStatusChanged(QuestStatus status)
        {
            if (status == QuestStatus.FAILED)
                SetStatus(QuestStatus.FAILED);
            else if (AllObjectivesEnded())
                SetStatus(QuestStatus.COMPLETED);
        }

        #endregion

        #endregion
    }
}
