
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database
{
    public class BaseDatabase<T> : ScriptableObject where T : BaseDataOfDatabase
    {
        #region VARIABLES

        [SerializeField] private List<T> datas;

        #endregion

        #region PROPERTIES

        public List<T> Datas => datas;

        #endregion

        #region METHODS

        public void AddNewItemData(T itemData)
        {
            datas.Add(itemData);
        }

        public void DeleteItemData(T itemData)
        {
            datas.Remove(itemData);
        }

        public T GetItemData(int id)
        {
            return Datas.Find(x => x.IdEquals(id));
        }

        #endregion
    }
}
