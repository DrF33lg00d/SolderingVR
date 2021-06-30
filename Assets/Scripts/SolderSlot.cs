using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolderSlot : MonoBehaviour
{
    private SolderingIron iron = null;
    private bool isSolder = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "soldering_iron") iron = other.gameObject.GetComponent<SolderingIron>();
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
        if (other.gameObject.name == "soldering_iron") iron = null;
        if (other.gameObject.name == "solder") isSolder = false;
    }

    private void Update()
    {
        if (isSolder && iron && transform.parent.GetComponent<ComponentPlace>().isSloted)
        {
            int index_component = -1;
            for (int i=0; i< transform.parent.childCount; i++)
            {
                if (index_component < 0)
                {
                    if (transform.parent.GetChild(i).CompareTag("Component")) index_component = i;
                }
            }
            if (iron.isTinned && transform.childCount == 0 && index_component >= 0)
            {
                Transform comp = transform.parent.GetChild(index_component);

                if (transform.childCount == 0){
                    iron.CreateSolderPoint(transform);

                    comp.GetComponent<ComponentItem>().isSoldered = true;
                    GameObject manager = GameObject.Find("GameManager");
                    if (manager.GetComponent<GameManager3>())
                    {
                        manager.GetComponent<GameManager3>().AddCompleted();
                    }

                    transform.GetChild(0).localPosition = Vector3.zero;
                }
            }
        }
    }
}
