using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireComponent : MonoBehaviour
{
    public Material wireRosin;
    public Material wireClear;
    public Material wireSolder;
    public GameObject neiborWire = null;

    private int level = 0;
    private bool isIron = false;
    private bool isRosin = false;
    private bool isSolder = false;
    private bool isWire = false;

    private bool inRosin = false;
    private bool inSolder = false;
    
    private float timer = 0;
    
    
    void Start()
    {
        level = Int32.Parse(name.Replace("wire", ""));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "soldering_iron") isIron = true;
        if (other.gameObject.name == "rosin") isRosin = true;
        if (neiborWire)
        {
            if (other.gameObject == neiborWire) isWire = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "solder")
        {
            Solder s = other.gameObject.GetComponent<Solder>();
            if (s.temperature >= s.temperatureMelting) isSolder = true;
            else isSolder = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "solder") isSolder = false;
        if (other.gameObject.name == "soldering_iron") isIron = false;
        if (other.gameObject.name == "rosin") isRosin = false;
        if (neiborWire)
        {
            if (other.gameObject == neiborWire) isWire = false;
        }
    }

    private void Update()
    {
        // if wire in rosin and soldering iron
        if (isIron && isRosin)
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                timer = 0;
                Material[] ms = GetComponent<MeshRenderer>().materials;
                ms[1] = wireRosin;
                GetComponent<MeshRenderer>().materials = ms;
                inRosin = true;
            }
        }

        
        
        if (isIron && isSolder && isWire && inSolder)
        {
            timer += Time.deltaTime;
            if (timer > 1f && transform.childCount < 5)
            {
                timer = 0;
                Rigidbody w2 = neiborWire.GetComponent<Rigidbody>();
                neiborWire.transform.SetParent(transform);
                foreach (var coll in neiborWire.GetComponents<Collider>())
                {
                    coll.enabled = false;
                }
                w2.velocity = Vector3.zero;
                w2.isKinematic = true;
                
                GameObject.Find("soldering_iron").GetComponent<SolderingIron>().CreateSolderPoint(transform);
                if (transform.childCount == 2)
                {
                    GameManager2 gm = GameObject.Find("GameManager").GetComponent<GameManager2>();
                    gm.AddCompleted();
                }
            }
        }else if (isIron && isSolder && inRosin)
        {
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                timer = 0;
                Material[] ms = GetComponent<MeshRenderer>().materials;
                ms[1] = wireSolder;
                GetComponent<MeshRenderer>().materials = ms;
                inSolder = true;
            }
        }
    }
}
