using Database.Recipes;
using UnityEngine;
using TMPro;
using Gameplay.Machines;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace UI.Window.Machines
{
    public class MachineWindow_Forging : UIWindowNested
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Information")] private TextMeshProUGUI productName;
        [SerializeField, FoldoutGroup("Information")] private TextMeshProUGUI costsText;
        [SerializeField, FoldoutGroup("Information")] private TextMeshProUGUI chanceText;
        [SerializeField, FoldoutGroup("Information")] private TextMeshProUGUI timeText;
        [SerializeField, FoldoutGroup("Information")] private Transform recipeButtonsField;

        [SerializeField] private Button startProduceButton;

        private MachineInGame selectedMachine;
        private RecipeData selectedRecipe;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public void SelectMachine(MachineInGame machine)
        {
            if (selectedMachine != null)
            {
                //clear
            }
            selectedMachine = machine;
        }


        #region UI_BUTTONS

        public void TryStartProduce()
        {
            if (selectedRecipe == null)
                return;
            
        }

        #endregion

        #endregion
    }
}