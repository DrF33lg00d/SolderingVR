using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireComponent : MonoBehaviour
{
    private int level = 0;
    void Start()
    {
        level = Int32.Parse(name.Replace("wire", ""));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
