using Database;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementInGameFromData<T> where T: BaseDataOfDatabase
{
    #region VARIABLES

    [SerializeField, ReadOnly] protected int dataId;

    protected T data;

    #endregion

    #region PROPERTIES

    public T Data { get; }

    #endregion

    #region CONSTRUCTORS

    public ElementInGameFromData() { }
    public ElementInGameFromData(T data)
    {
        this.data = data;
        dataId = data.Id;
    }

    #endregion

    #region METHODS

    #endregion
}
