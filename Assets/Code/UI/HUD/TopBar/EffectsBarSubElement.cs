using Gameplay.Character;
using Gameplay.Management.Characters;
using Gameplay.Management.Effects;
using System.Text;
using TMPro;
using UnityEngine;

namespace Gameplay.HUD
{
    public class EffectsBarSubElement : HUDBar
    {
        #region VARIABLES

        [SerializeField] private TextMeshProUGUI activeEffectsInfo;

        private StringBuilder builder;

        #endregion

        #region PROPERTIES

        private EffectsManager Effects => EffectsManager.Instance;
        private CharacterManager Characters => CharacterManager.Instance;
        private PlayerInGame Player => Characters == null ? null : Characters.Player;

        #endregion

        #region METHODS

        public override void Initialize()
        {
            base.Initialize();
            RefreshActiveEffects();
        }

        public override void AttachEvents()
        {
            base.AttachEvents();
            if (Effects)
            {
                Effects.OnAddedEffect += HandleEffectsChanged;
                Effects.OnRemovedEffect += HandleEffectsChanged;
                Effects.OnAddedGlobalEffect += HandleGlobalEffectsChanged;
                Effects.OnRemovedGlobalEffect += HandleGlobalEffectsChanged;
            }
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            if (Effects)
            {
                Effects.OnAddedEffect -= HandleEffectsChanged;
                Effects.OnRemovedEffect -= HandleEffectsChanged;
                Effects.OnAddedGlobalEffect -= HandleGlobalEffectsChanged;
                Effects.OnRemovedGlobalEffect -= HandleGlobalEffectsChanged;
            }
        }

        private void RefreshActiveEffects()
        {
            if (builder == null)
                builder = new();
            builder.Clear();

            Effects.FillBuilderByGlobalEffectsInfo(ref builder);
            Effects.FillBuilderByEffectsInfoForTarget(Player, ref builder);

            activeEffectsInfo.SetText(builder);
        }

        #region HANDLERS

        private void HandleEffectsChanged(IEffectable effectable)
        {
            if (effectable is PlayerInGame player)
                RefreshActiveEffects();
        }

        private void HandleGlobalEffectsChanged()
        {
            RefreshActiveEffects();
        }

        #endregion

        #endregion
    }
}