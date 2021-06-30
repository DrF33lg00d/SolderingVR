using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rosin : MonoBehaviour
{
    private int toMuchSolder;

    private float timer = 0.0f;
    private bool isIron = false;
    private bool isSolder = false;
    private SolderingIron solderingIron = null;
    private Solder solderItem = null;

    private void Start()
    {
        toMuchSolder = Random.Range(1, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "soldering_iron")
        {
            isIron = true;
            solderingIron = other.gameObject.GetComponent<SolderingIron>();
        }
        if (other.gameObject.name == "solder")
        {
            isSolder = true;
            solderItem = other.gameObject.GetComponent<Solder>();
        }
    }

    public void Update()
    {
        if (isIron && isSolder && solderItem.isHeating && solderItem.temperature >= solderItem.temperatureMelting)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                if (toMuchSolder > 0)
                {
                    solderingIron.isTinned = true;
                    solderingIron.changeStingMaterial();
                    solderingIron.CreateSolderPoint(solderingIron.transform, true);
                    toMuchSolder--;
                }
                else if (toMuchSolder == 0)
                {
                    solderingIron.isTinned = true;
                    solderingIron.changeStingMaterial();
                }
                timer = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "soldering_iron")
        {
            isIron = false;
            solderingIron = null;
        }
        if (other.gameObject.name == "solder")
        {
            isSolder = false;
            solderItem = null;
        }    
    }
}
