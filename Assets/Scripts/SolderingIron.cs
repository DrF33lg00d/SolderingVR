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
        if (countTries < 1 && time_tink > 1.5 && isTinned && isConnRosin)
        {
            GameObject s = Instantiate(solderPoint, ironEnd);
            s.transform.SetParent(transform);
            solderPoints.Add(s);
            countTries++;
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "cleaner")
        {
            
            if (isTinned)
            {
                isTinned = false;
                changeStingMaterial();
            }
        }

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
        // connect iron to solder for tinkining
        if (other.gameObject.name == "solder")
        {
            isConnSolder = true;
            Solder sample = other.gameObject.GetComponent<Solder>();
            if (sample.isHeating && sample.temperature >= sample.temperatureMelting && !isTinned && isConnRosin)
            {
                isTinned = true;
                changeStingMaterial();
                time_tink += Time.deltaTime;
            }
        }
        
        if (other.gameObject.CompareTag("SolderSlot") && isTinned && isConnSolder)
        {
            Transform place = other.gameObject.transform.parent;
            int index_component = -1;
            for (int i=0; i< place.childCount; i++)
            {
                if (index_component < 0)
                {
                    if (place.GetChild(i).CompareTag("Component")) index_component = i;
                }
            }

            if (index_component >= 0)
            {
                Transform comp = place.GetChild(index_component);
                GameObject solderSlot = other.gameObject;

                time_soldering += Time.deltaTime;

                if (time_soldering > 1 && solderSlot.transform.childCount < 1){
                    
                    GameObject point = Instantiate(solderPoint, solderSlot.transform);
                    // point.transform.SetParent(comp.transform);
                    comp.GetComponent<ComponentItem>().isSoldered = true;
                    // component.GetComponent<XRGrabInteractable>().enabled = false;
                    // component.GetComponent<ComponentItem>().isSoldered = true;
                }


            }
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
    
}
