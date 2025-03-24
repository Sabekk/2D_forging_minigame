using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Gameplay.Quests
{
    [Serializable]
    public abstract class QuestObjectiveBase : IDisposable
    {
        #region ACTIONS

        public event Action OnValueChanged;
        public event Action<QuestStatus> OnStatusChanged;

        #endregion

        #region VARIABLES

        [SerializeField] private string objectiveName;
        [SerializeField] private ComparisonType comparisonType;
        [SerializeField] private int targetValue;
        [SerializeField] private bool hasDurationTime;
        [SerializeField, ShowIf(nameof(hasDurationTime))] private int durationTime;

        [Tooltip("If enabled, objective will be failure when reach targeted value")]
        [SerializeField] private bool reversedCondition;

        [SerializeField, HideInInspector] private QuestStatus status;
        [SerializeField, HideInInspector] private int currentValue;
        [SerializeField, HideInInspector] private int timeLeft;

        #endregion

        #region PROPERTIES

        public virtual int CurrentValue
        {
            get => currentValue;
            set
            {
                bool hasChanged = !Mathf.Approximately(value, currentValue);

                currentValue = value;
                CheckStatus(currentValue);

                if (hasChanged)
                    OnValueChanged?.Invoke();
            }
        }

        public virtual int TargetValue
        {
            get => targetValue;
            protected set => targetValue = value;
        }

        public virtual bool IsCompleted
        {
            get
            {
                CheckCurrentStatus();
                return IsEnded;
            }
        }

        public virtual bool IsEnded => status != QuestStatus.ACTIVE;
        public bool HasDurationTime => hasDurationTime;
        public bool ReversedCondition => reversedCondition;
        public string ObjectiveName => objectiveName;
        public int DurationTime => durationTime;
        public int TimeLeft
        {
            get => timeLeft;
            protected set => timeLeft = value;
        }

        #endregion

        #region CONSTRUCTORS

        public QuestObjectiveBase() { }

        public QuestObjectiveBase(QuestObjectiveBase objective)
        {
            objectiveName = objective.objectiveName;
            comparisonType = objective.comparisonType;
            reversedCondition = objective.reversedCondition;
            hasDurationTime = objective.hasDurationTime;
            durationTime = objective.durationTime;
            timeLeft = durationTime;
        }

        #endregion

        #region METHODS

        public static QuestObjectiveBase CreateInstance(QuestObjectiveBase objective, params object[] args)
        {
            return Activator.CreateInstance(objective.GetType(), args) as QuestObjectiveBase;
        }

        public virtual void Initialize() { }

        public virtual void Dispose() { }

        protected void CheckCurrentStatus()
        {
            CheckStatus(CurrentValue);
        }

        protected void SetStatus(QuestStatus status)
        {
            this.status = status;
            OnStatusChanged?.Invoke(status);
        }

        private void CheckStatus(float checkedValue)
        {
            if (IsEnded)
                return;

            bool ended = false;
            switch (comparisonType)
            {
                case ComparisonType.EAUAL:
                    ended = Mathf.Approximately(checkedValue, targetValue);
                    break;
                case ComparisonType.LOWER:
                    ended = checkedValue < targetValue;
                    break;
                case ComparisonType.GREATER:
                    ended = checkedValue > targetValue;
                    break;
                case ComparisonType.LOWER_OR_EQUAL:
                    ended = checkedValue <= targetValue;
                    break;
                case ComparisonType.GREATER_OR_EQUAL:
                    ended = checkedValue >= targetValue;
                    break;
                default:
                    Debug.LogWarning("Unknown comparison type. This should not happen.");
                    ended = false;
                    break;
            }

            if (ended)
                SetStatus(reversedCondition ? QuestStatus.FAILED : QuestStatus.COMPLETED);
        }

        #endregion
    }
}