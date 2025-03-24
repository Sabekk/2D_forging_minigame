using Gameplay.Management.Timing;
using System;

namespace Gameplay.Quests
{
    public abstract class QuestObjectiveComplex : QuestObjectiveBase, IAttachableEvents
    {
        #region ACTIONS

        public event Action OnRefreshLeftTime;

        #endregion

        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        public QuestObjectiveComplex() { }

        public QuestObjectiveComplex(QuestObjectiveComplex objective) : base(objective)
        {
            TargetValue = objective.TargetValue;
            CurrentValue = 0;
        }

        #endregion

        #region METHODS

        public override void Initialize()
        {
            base.Initialize();
            AttachEvents();
        }

        public override void Dispose()
        {
            base.Dispose();
            DetachEvents();
        }

        public virtual void AttachEvents()
        {
            if (HasDurationTime)
            {
                if (TimeManager.Instance)
                    TimeManager.Instance.OnSecondPassed += HandleSecondChanged;
            }
        }

        public virtual void DetachEvents()
        {
            if (HasDurationTime)
            {
                if (TimeManager.Instance)
                    TimeManager.Instance.OnSecondPassed -= HandleSecondChanged;
            }
        }

        protected virtual string GetBaseInfo()
        {
            return string.Format("{0} ({1}/{2})", ObjectiveName, CurrentValue, TargetValue);
        }

        #region HANDLERS

        private void HandleSecondChanged()
        {
            if (!HasDurationTime || TimeLeft <= 0)
                return;

            TimeLeft--;
            if (TimeLeft <= 0)
            {
                SetStatus(ReversedCondition ? QuestStatus.COMPLETED : QuestStatus.FAILED);
                Dispose();
            }

            OnRefreshLeftTime?.Invoke();
        }

        #endregion

        #endregion
    }
}
