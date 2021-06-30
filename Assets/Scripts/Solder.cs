using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solder : MonoBehaviour
{
    public float temperatureMelting = 240f;
    public bool isHeating = false;
    public float temperature = 20.0f;

    void FixedUpdate()
    {
        if (isHeating)
        {
            temperature = Mathf.Clamp(temperature + 2, 20, temperatureMelting+10);
        }
        else
        {
            temperature = Mathf.Clamp(temperature - 0.5f, 20, temperatureMelting);
        }
    }
    
    private void OnCollisionStay(Collision other)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "soldering_iron")
        {
            isHeating = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "soldering_iron")
        {
            isHeating = false;
        }
    }
    
}
