using Gameplay.Effects;
using Gameplay.Management.Characters;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Gameplay.Management.Effects
{
    public class EffectsManager : GameplayManager<EffectsManager>
    {
        #region ACTION

        public event Action OnAddedGlobalEffect;
        public event Action OnRemovedGlobalEffect;
        public event Action<IEffectable> OnAddedEffect;
        public event Action<IEffectable> OnRemovedEffect;

        #endregion

        #region VARIABLES

        [SerializeField] private EffectsStack globalStack;
        [SerializeField] private Dictionary<IEffectable, EffectsStack> effectStack;
        [SerializeReference, FoldoutGroup("Debug")] private PlayerEffect debugPlayerEffect;
        [SerializeReference, FoldoutGroup("Debug")] private GlobalEffect debugGlobalEffect;

        #endregion

        #region PROPERTIES

        public EffectsStack GlobalStack => globalStack ??= new();
        public Dictionary<IEffectable, EffectsStack> EffectStack => effectStack ??= new();

        #endregion

        #region METHODS

        public override void AttachEvents()
        {
            base.AttachEvents();
            //TODO ticking for limited effects
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            //TODO ticking for limited effects
        }

        public string GetEffectsDescription<TE>(List<TE> effects) where TE : GlobalEffect
        {
            StringBuilder builder = new();
            if (effects != null)
                foreach (var effect in effects)
                    builder.AppendLine(effect.EffectName);

            return builder.ToString();
        }

        public void ExecuteEffects<TE>(List<TE> effects) where TE : GlobalEffect
        {
            if (effects == null)
                return;

            foreach (var effect in effects)
                ExecuteEffect(effect);
        }

        public void RemoveEffects<TE>(List<TE> effects) where TE : GlobalEffect
        {
            if (effects == null)
                return;

            foreach (var effect in effects)
                RemoveEffect(effect);
        }

        public void ExecuteEffect<TE>(TE effect) where TE : GlobalEffect
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
                    if (GlobalStack.CanAddEffect(effect))
                        GlobalStack.AddEffect(effect);
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
                effect.ExecuteEffect();
                OnAddedGlobalEffect?.Invoke();
            }
        }

        public void RemoveEffect<TE>(TE effect) where TE : GlobalEffect
        {
            if (effect == null)
            {
                Debug.LogError($"Missing effect, check settings: {nameof(TE)}");
                return;
            }

            if (effect.EffectType == EffectType.PERMAMENT)
                return;

            switch (effect.EffectType)
            {
                case EffectType.CONSTANT:
                    GlobalStack.RemoveEffect(effect);
                    break;
                case EffectType.TIME_LIMITED:

                    //TODO
                    break;
                default:
                    break;
            }

            effect.RemoveEffect();
            OnRemovedGlobalEffect?.Invoke();
        }

        //TODO Inlcude target into description
        public string GetEffectsDescription<TE, TT>(List<TE> effects, TT target) where TE : TargetedEffect<TT> where TT : IEffectable
        {
            StringBuilder builder = new();

            if (effects != null)
                foreach (var effect in effects)
                    builder.AppendLine(effect.EffectName);

            return builder.ToString();
        }


        public void ExecuteEffects<TE, TT>(List<TE> effects, TT target) where TE : TargetedEffect<TT> where TT : IEffectable
        {
            if (effects == null)
                return;

            foreach (var effect in effects)
                ExecuteEffect(effect, target);
        }

        public void RemoveEffects<TE, TT>(List<TE> effects, TT target) where TE : TargetedEffect<TT> where TT : IEffectable
        {
            if (effects == null)
                return;

            foreach (var effect in effects)
                RemoveEffect(effect, target);
        }

        public void ExecuteEffect<TE, TT>(TE effect, TT target) where TE : TargetedEffect<TT> where TT : IEffectable
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

            if (effect.EffectType == EffectType.PERMAMENT)
                return;

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

        [Button, FoldoutGroup("Debug")]
        private void TryAddPlayerEffect()
        {
            if (debugPlayerEffect == null)
                return;

            if (CharacterManager.Instance == null)
                return;

            ExecuteEffect(debugPlayerEffect, CharacterManager.Instance.Player);
        }

        [Button, FoldoutGroup("Debug")]
        private void TryRemovePlayerEffect()
        {
            if (debugPlayerEffect == null)
                return;

            if (CharacterManager.Instance == null)
                return;

            RemoveEffect(debugPlayerEffect, CharacterManager.Instance.Player);
        }



        [Button, FoldoutGroup("Debug")]
        private void TryAddGlobalEffect()
        {
            if (debugGlobalEffect == null)
                return;

            ExecuteEffect(debugGlobalEffect);
        }

        [Button, FoldoutGroup("Debug")]
        private void TryRemoveGlobalEffect()
        {
            if (debugGlobalEffect == null)
                return;

            RemoveEffect(debugGlobalEffect);
        }

        #endregion
    }
}