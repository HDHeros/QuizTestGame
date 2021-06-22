using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DataSet", menuName = "DatSet", order = 51)]

public class DataSet : ScriptableObject
{
    [SerializeField] private SeleteableGameObject[] _selecteableGameObjects;
    
    public List<SeleteableGameObject> GetDataSet()
    {
        return new List<SeleteableGameObject>(_selecteableGameObjects);
    }

}
