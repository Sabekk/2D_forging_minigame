using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Quests
{
    [CreateAssetMenu(menuName = "Database/QuestsDatabase", fileName = "QuestsDatabase")]
    public class QuestsDatabase : BaseDatabase<QuestData>
    {
        #region VARIABLES

        public const string GET_QUESTS_DATA_METHOD = "@" + nameof(QuestsDatabase) + "." + nameof(GetQuestsDatas) + "()";

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public static IEnumerable GetQuestsDatas()
        {
            ValueDropdownList<int> values = new();
            foreach (QuestData questData in MainDatabases.Instance.QuestsDatabase.Datas)
                values.Add(questData.DataName, questData.Id);

            return values;
        }

        #endregion
    }
}
