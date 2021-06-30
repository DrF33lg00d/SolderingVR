using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEditor;
using UnityEngine;

public class ComponentItem : MonoBehaviour
{
   public bool isSoldered = false;
   public bool isSloted = false;
   public int countChildSolders = 0;
   public string nameSlot;

   public List<Collider> colls = new List<Collider>();
   

   private void Start()
   {
      foreach (var c in GetComponents<Collider>())
      {
         colls.Add(c);
      }
   }

   private void Update()
   {
      if (transform.position.y < -100)
      {
         Transform spawn = GameObject.FindWithTag("Respawn").transform;
         GetComponent<Rigidbody>().velocity = Vector3.zero;
         GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
         transform.position = spawn.position;
         toggleColliders(true);
      }
   }


   public void toggleColliders(bool Activated)
   {
      foreach (var coll in colls)
      {
         coll.enabled = Activated;
      }
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag("Slot") && other.gameObject.name.Contains(nameSlot))
      {
         GameObject slot = other.gameObject;
         bool noComponent = true;
         for (int i = 0; i < slot.transform.childCount; i++)
         {
            if (slot.transform.GetChild(i).CompareTag("Component")) noComponent = false;
         }
         if (noComponent){
            toggleColliders(false);



            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;

            transform.SetParent(slot.transform);
            transform.position = slot.transform.position;
            transform.rotation = slot.transform.rotation;
            isSloted = true;
            slot.GetComponent<ComponentPlace>().DeleteProjection();
            slot.GetComponent<ComponentPlace>().isSloted = true;
         }
      }
      
      
      // && other.gameObject.name.Contains(nameSlot)
      
   }

   private void OnTriggerStay(Collider other)
   {
      if (other.gameObject.name == "solder")
      {
         
      }
   }

   public bool AttachDone()
   {
      return isSoldered && transform.childCount == countChildSolders;
   }

   private void OnTriggerExit(Collider other)
   {
      
      if (other.gameObject.CompareTag("Slot") && isSloted )
      {
         GameObject slot = other.gameObject;

         GetComponent<Rigidbody>().useGravity = true;
         GetComponent<Rigidbody>().isKinematic = false;
         isSloted = false;
         toggleColliders(true);
      }    
   }


}
