using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Effects
{
    public abstract class EffectBase : IIdEqualable
    {
        #region VARIABLES

        [SerializeField, ReadOnly] private int id = Guid.NewGuid().GetHashCode();
        [SerializeField] private string effectName;
        [SerializeField] private EffectType effectType;
        [SerializeField] private bool isStackable;
        [SerializeField, SuffixLabel("Seconds"), ShowIf(nameof(effectType), EffectType.TIME_LIMITED)] int timeLimit;

        #endregion

        #region PROPERTIES

        public int Id => id;
        public int TimeLimit => timeLimit;
        public string EffectName => effectName;
        public bool IsStackable => isStackable;
        public EffectType EffectType => effectType;

        #endregion

        #region METHODS

        public virtual void ExecuteEffect() { }

        public virtual void RemoveEffect() { }

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion
    }
}