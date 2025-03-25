using Gameplay.Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Management.Effects
{
    public class EffectsStack
    {
        #region VARIABLES

        [SerializeField] private List<EffectBase> constEffects;
        //TODO Add limited by time effects

        #endregion

        #region PROPERTIES

        public List<EffectBase> ConstEffects => constEffects ??= new();
        public bool ShouldBeRemoved => ConstEffects.Count == 0;

        #endregion

        #region METHODS

        public bool CanAddEffect(EffectBase effect)
        {
            if (effect.IsStackable)
                return true;
            else
                return ConstEffects.ContainsId(effect.Id) == false;
        }

        public void AddEffect(EffectBase effect)
        {
            ConstEffects.Add(effect);
        }

        public void RemoveEffect(EffectBase effect)
        {
            ConstEffects.Remove(effect);
        }

        #endregion
    }
}