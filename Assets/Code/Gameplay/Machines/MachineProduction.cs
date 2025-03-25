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
        [SerializeField] private int produceTime;
        [SerializeField] private int timeLeft;
        [SerializeField] private float successChance;

        #endregion

        #region PROPERTIES       

        public int CraftingItemId => craftingItemId;
        public bool CraftingEnded => produceTime <= 0;
        public float SuccessChance => successChance;

        #endregion

        #region CONSTRUCTORS

        public MachineProduction() { }
        public MachineProduction(int craftingItemId, float successChance, int produceTime)
        {
            this.craftingItemId = craftingItemId;

            this.successChance = successChance;
            this.produceTime = produceTime;
        }

        #endregion

        #region METHODS

        public void ProgressProduction()
        {
            produceTime--;
        }

        #endregion
    }
}
