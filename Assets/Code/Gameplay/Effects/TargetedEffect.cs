using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Effects
{
    [Serializable]
    public abstract class TargetedEffect<IEffectable> : EffectBase
    {
        #region VARIABLES

        private IEffectable EffectTarget;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public virtual void ExecuteEffect(IEffectable target) { }
        public virtual void RemoveEffect(IEffectable target) { }

        #endregion
    }
}
