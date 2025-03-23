using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Database
{
    public class BaseDataOfDatabase : IIdEqualable
    {

        #region VARIABLES

        [SerializeField, ReadOnly] protected int id = Guid.NewGuid().GetHashCode();
        [SerializeField, FoldoutGroup("Base setting"), Tooltip("Basic name of created object by this data")] protected string dataName;

        #endregion

        #region PROPERTIES

        public int Id => id;
        public string DataName => dataName;

        #endregion

        #region METHODS

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion

    }
}
