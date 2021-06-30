using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "soldering_iron")
        {
            SolderingIron iron = other.gameObject.GetComponent<SolderingIron>();
            iron.isTinned = false;
            iron.changeStingMaterial();
            iron.ClearPoints();
        }
    }
}
