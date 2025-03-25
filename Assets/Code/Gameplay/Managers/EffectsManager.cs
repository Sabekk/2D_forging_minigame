using Gameplay.Effects;
using Gameplay.Management.Characters;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Management.Effects
{
    public class EffectsManager : GameplayManager<EffectsManager>
    {
        #region ACTION

        public event Action<IEffectable> OnAddedEffect;
        public event Action<IEffectable> OnRemovedEffect;

        #endregion


        #region VARIABLES

        [SerializeField] private Dictionary<IEffectable, EffectsStack> effectStack;
        [SerializeReference] private PlayerEffect debugPlayerEffect;

        #endregion

        #region PROPERTIES

        public Dictionary<IEffectable, EffectsStack> EffectStack
        {
            get
            {
                if (effectStack == null)
                    effectStack = new();
                return effectStack;
            }
        }

        #endregion

        #region METHODS

        public override void AttachEvents()
        {
            base.AttachEvents();
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
        }

        public void AddEffect<TE, TT>(TE effect, TT target) where TE : TargetedEffect<TT> where TT : IEffectable
        {
            if (effect == null)
            {
                Debug.LogError($"Missing effect, check settings: {nameof(TE)}");
                return;
            }

            bool canAdd = true;

            switch (effect.EffectType)
            {
                case EffectType.CONSTANT:
                    EffectsStack stack = null;
                    if (EffectStack.TryGetValue(target, out stack) == false)
                    {
                        stack = new EffectsStack();
                        EffectStack.Add(target, stack);
                    }

                    if (stack.CanAddEffect(effect))
                        stack.AddEffect(effect);
                    else
                        canAdd = false;

                    break;
                case EffectType.TIME_LIMITED:

                    //TODO
                    break;
                default:
                    break;
            }

            if (canAdd)
            {
                effect.ExecuteEffect(target);
                OnAddedEffect?.Invoke(target);
            }
        }

        public void RemoveEffect<TE, TT>(TE effect, TT target) where TE : TargetedEffect<TT> where TT : IEffectable
        {
            if (effect == null)
            {
                Debug.LogError($"Missing effect, check settings: {nameof(TE)}");
                return;
            }

            switch (effect.EffectType)
            {
                case EffectType.CONSTANT:
                    if (EffectStack.TryGetValue(target, out var stack))
                    {
                        stack.RemoveEffect(effect);
                        if (stack.ShouldBeRemoved)
                            EffectStack.Remove(target);
                    }

                    break;
                case EffectType.TIME_LIMITED:

                    //TODO
                    break;
                default:
                    break;
            }

            effect.RemoveEffect(target);
            OnRemovedEffect?.Invoke(target);
        }

        [Button]
        private void TryAddEffect()
        {
            if (debugPlayerEffect == null)
                return;

            if (CharacterManager.Instance == null)
                return;

            AddEffect(debugPlayerEffect, CharacterManager.Instance.Player);
        }

        [Button]
        private void TryRemoveEffect()
        {
            if (debugPlayerEffect == null)
                return;

            if (CharacterManager.Instance == null)
                return;

            RemoveEffect(debugPlayerEffect, CharacterManager.Instance.Player);
        }

        #endregion
    }
}