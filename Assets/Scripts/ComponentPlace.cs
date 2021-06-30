using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ComponentPlace : MonoBehaviour
{
    public bool isSloted = false;
    public bool isReady = false;
    public List<Collider> slots;
    public int countSlots = 0;
    public GameObject sample;

    private GameObject instance;
    private void Start()
    {
        if (!isReady && sample != null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                child.gameObject.AddComponent<SolderSlot>();
            }
            
            instance = Instantiate(sample, transform);
            instance.transform.localPosition=Vector3.zero;

            if (instance.GetComponent<MeshRenderer>())
            {
                Material[] ms = UpdateToTransperent(instance.GetComponent<MeshRenderer>().materials);
                instance.GetComponent<MeshRenderer>().materials = ms;
            }
            for (int i = 0; i < instance.transform.childCount; i++)
            {
                Transform child_instance = instance.transform.GetChild(i);
                Material[] ms = child_instance.GetComponent<MeshRenderer>().materials;
                child_instance.GetComponent<MeshRenderer>().materials = UpdateToTransperent(ms);
            }
        }
    }

    public void DeleteProjection()
    {
        if (instance != null)
        {
            Destroy(instance);
            instance = null;
        }
    }

    public Material[] UpdateToTransperent(Material[] ms)
    {
        for (int i = 0; i < ms.Length; i++)
        {
            ms[i].SetOverrideTag("RenderType", "Transparent");
            ms[i].SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
            ms[i].SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            ms[i].SetInt("_ZWrite", 0);
            ms[i].DisableKeyword("_ALPHATEST_ON");
            ms[i].EnableKeyword("_ALPHABLEND_ON");
            ms[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
            ms[i].renderQueue = (int) UnityEngine.Rendering.RenderQueue.Transparent;
            Color c = ms[i].color;
            c.a = 0.3f;
            ms[i].color = c;
            // ms[i].color = Color.Lerp(GetComponent<Renderer>().material.color, color, fadeSpeed * Time.deltaTime);

        }

        return ms;
    }
    
}
