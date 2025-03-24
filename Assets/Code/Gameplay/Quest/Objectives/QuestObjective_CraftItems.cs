using Database.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Quests
{
    public class QuestObjective_CraftItems : QuestObjectiveBase
    {
        #region VARIABLES

        [SerializeField, ValueDropdown(ItemsDatabase.GET_ITEM_DATA_METHOD)] private int targetItemDataId;

        #endregion

        #region PROPERTIES

        public int TargetItemDataId => targetItemDataId;

        #endregion

        #region CONSTRUCTORS

        public QuestObjective_CraftItems() { }

        public QuestObjective_CraftItems(QuestObjective_CraftItems objective) : base(objective)
        {

        }

        #endregion

        #region METHODS

        //TODO Attach to event of creating items

        #endregion
    }
}
