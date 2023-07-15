using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node : MonoBehaviour
{
    [SerializeField] string nodeId;
    [SerializeField] int x, y, z;
    [SerializeField] int trueValue;
    [SerializeField] int predictValue;

    public Node(){
        
    }

    public void SetName()
    {
        this.nodeId = x.ToString()+y.ToString()+z.ToString();
    }

    public void SetNodeIndex(int x,int y,int z)
    {
        this.x = x; this.y = y;this.z = z; 
    }

    
}
