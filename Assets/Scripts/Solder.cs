using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solder : MonoBehaviour
{
    public float temperatureMelting = 240f;
    public bool isHeating = false;
    public float temperature = 20.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeating)
        {
            temperature = Mathf.Clamp(temperature + 1, 20, temperatureMelting+10);
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
