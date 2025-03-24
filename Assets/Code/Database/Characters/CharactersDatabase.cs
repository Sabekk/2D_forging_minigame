using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Database.Character
{
    [CreateAssetMenu(menuName = "Database/CharactersDatabase", fileName = "CharactersDatabase")]
    public class CharactersDatabase : BaseDatabase<CharacterData>
    {
        #region VARIABLES

        public const string GET_CHARACTERS_DATA_METHOD = "@" + nameof(CharactersDatabase) + "." + nameof(GetMachineDatas) + "()";

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public static IEnumerable GetMachineDatas()
        {
            ValueDropdownList<int> values = new();
            foreach (CharacterData characterData in MainDatabases.Instance.CharactersDatabase.Datas)
                values.Add(characterData.DataName, characterData.Id);

            return values;
        }

        #endregion
    }
}