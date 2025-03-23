using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDataOfDatabase : IIdEqualable
{
    [SerializeField, ReadOnly] protected int id = Guid.NewGuid().GetHashCode();
    [SerializeField, FoldoutGroup("Base setting")] protected string dataName;

    public int Id => id;
    public string DataName => dataName;

    public bool IdEquals(int id)
    {
        return Id == id;
    }
}
