using Database;
using Database.Recipes;
using System;
using UnityEngine;

namespace Gameplay.Machines
{
    [Serializable]
    public class MachineProduction
    {
        #region VARIABLES

        [SerializeField] private int craftingItemId;
        [SerializeField] private float produceTime;
        [SerializeField] private float timeLeft;
        [SerializeField] private float successChance;

        #endregion

        #region PROPERTIES       

        public int CraftingItemId => craftingItemId;
        public bool CraftingEnded => timeLeft <= 0;
        public float SuccessChance => successChance;
        public float PercentageProgress => produceTime <= 0 ? 100f : timeLeft / produceTime;
        public float TimeLeft => timeLeft;

        #endregion

        #region CONSTRUCTORS

        public MachineProduction() { }
        public MachineProduction(int craftingItemId, float successChance, int produceTime)
        {
            this.craftingItemId = craftingItemId;

            this.successChance = successChance;
            this.produceTime = produceTime;
            timeLeft = produceTime;
        }

        #endregion

        #region METHODS

        public void ProgressProduction()
        {
            timeLeft--;
        }

        #endregion
    }
}
