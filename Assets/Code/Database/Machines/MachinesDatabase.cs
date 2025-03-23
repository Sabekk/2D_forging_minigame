using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Database.Machines
{
    [CreateAssetMenu(menuName = "Database/MachinesDatabase", fileName = "MachinesDatabase")]
    public sealed class MachinesDatabase : BaseDatabase<MachineData>
    {
        #region VARIABLES

        public const string GET_MACHINES_DATA_METHOD = "@" + nameof(MachinesDatabase) + "." + nameof(GetMachineDatas) + "()";

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public static IEnumerable GetMachineDatas()
        {
            ValueDropdownList<int> values = new();
            foreach (MachineData machineData in MainDatabases.Instance.MachinesDatabase.Datas)
                values.Add(machineData.DataName, machineData.Id);

            return values;
        }

        #endregion
    }
}
