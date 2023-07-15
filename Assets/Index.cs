using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Index:MonoBehaviour
{
    // index [cell , row]
    [SerializeField] public int cellIndex, rowIndex;

    [SerializeField] public bool isPencil;
    public void SetIndex(int cell,int row)
    {
        this.cellIndex = cell;
        this.rowIndex = row;
    }

    public void SetChosenIndex()
    {
        Grid.chosenIndex = this;
        GameObject board = GameObject.Find("Board");
    }

    public void SetPencil(bool pencil)
    {
        this.isPencil = pencil;
    }


}
