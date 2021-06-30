using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class SolderingIron : MonoBehaviour
{
    public float temperature = 280.0f;
    public Material solderSting;
    public Material emptySting;
    public Transform ironEnd;
    public GameObject solderPoint;
    public bool isTinned = false;

    
    private bool isConnSolder = false;
    private bool isConnRosin = false;
    private bool isSolderKeep = false;
    private int countTries = 0;
    private float time_tink = 0;
    private float time_soldering = 0;
    private List<GameObject> solderPoints = new List<GameObject>();
    private GameObject component = null;


    public void Start()
    {
        if (isTinned)
        {
            changeStingMaterial();
        }
    }

    public void Update()
    {
        // if (countTries < 1 && time_tink > 1.5 && isTinned && isConnRosin)
        // {
        //     GameObject s = Instantiate(solderPoint, ironEnd);
        //     s.transform.SetParent(transform);
        //     solderPoints.Add(s);
        //     countTries++;
        // }

    }
    
    
    
    public void CreateSolderPoint(Transform parent = null, bool scaleUp = false)
    {
        GameObject s = Instantiate(solderPoint, ironEnd);
        if (parent == transform)
        {
            s.transform.SetParent(transform);
            solderPoints.Add(s);
        }
        else s.transform.SetParent(parent);
        if (scaleUp) s.transform.localScale += s.transform.localScale;
        
        //     countTries++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "rosin")
        {
            isConnRosin = true;
            
        }

        if (other.gameObject.CompareTag("Component"))
        {
            component = other.gameObject;
        }


        
        
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "solder")
        {
            isConnSolder = true;
            // Solder sample = other.gameObject.GetComponent<Solder>();
            // if (sample.isHeating && sample.temperature >= sample.temperatureMelting && !isTinned && isConnRosin)
            // {
            //     isTinned = true;
            //     changeStingMaterial();
            //     time_tink += Time.deltaTime;
            // }
        }
        
        if (other.gameObject.CompareTag("SolderSlot") && isTinned && isConnSolder)
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //clear connections to objects
        if (other.gameObject.name == "cleaner")
        {
            isConnRosin = false;
        }
        if (other.gameObject.name == "solder")
        {
            isConnSolder = false;
            time_tink = 0;
        }
        if (other.gameObject.name == "rosin")
        {
            isConnRosin = false;
        }

        if (other.gameObject.CompareTag("SolderSlot"))
        {
            time_soldering = 0;
        }
        if (other.gameObject.CompareTag("Resistor"))
        {
            component = null;
        }
    }

    public void changeStingMaterial()
    {
        Material[] ms = GetComponent<MeshRenderer>().materials;
        if(isTinned)
            ms[1] = solderSting;
        else
            ms[1] = emptySting;
        GetComponent<MeshRenderer>().materials = ms;
    }

    public void ClearPoints()
    {
        foreach (var point in solderPoints)
        {
            Destroy(point);
        }
        solderPoints.Clear();
    }
    
}
