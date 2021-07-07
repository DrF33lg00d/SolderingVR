using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolderSlotV2 : SolderSlot
{
    public void Awake()
    {
        var openingPrefab = Resources.Load("op", typeof(GameObject)) as GameObject;
        
        var op = Instantiate(openingPrefab, transform);
        op.transform.localPosition += Vector3.up * 0.0011f;
    }
}
