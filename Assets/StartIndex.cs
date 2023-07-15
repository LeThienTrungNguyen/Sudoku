using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartIndex : MonoBehaviour
{
    GameObject value;
    GameObject pencil;

    Index index;
    // Start is called before the first frame update
    void Start()
    {
        value = transform.GetChild(0).gameObject;
        pencil = transform.GetChild(1).gameObject;
        index = GetComponent<Index>();
        index.SetPencil(false);

        if (index.isPencil)
        {
            pencil.SetActive(true);
            value.SetActive(false);
        }
        else
        {
            pencil.SetActive(false);
            value.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
