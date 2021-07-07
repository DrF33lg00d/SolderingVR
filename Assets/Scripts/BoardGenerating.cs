using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.ProBuilder;

[Serializable] public class BoardSaving
{
    public string name;
    public float[] sizePlate;
    public int[] countFaces;
    public List<int> tracesInd;
    public List<PlaceInfo> places;
}

[Serializable] public struct PlaceInfo
{
    public string namePlace;
    public string nameModel;
    public Vector3 position;
    public Quaternion rotation;
}

public class BoardGenerating : MonoBehaviour
{
    public Material testMaterial; 
    public Material traceMaterial;
    public string namePlate = "flashlight_1";

    [SerializeField] public float[] sizePlate = new float[] {1, 1};

    public void GenerateMainPlate(Axis direction){
        bool isUp = direction == Axis.Up;
        ProBuilderMesh k;
        k = ShapeGenerator.GeneratePlane(
            PivotLocation.FirstCorner, 
            isUp ? sizePlate[0]: sizePlate[1],
            isUp ? sizePlate[1]: sizePlate[0],
            isUp ? Convert.ToInt32(100 * sizePlate[0]):Convert.ToInt32(100 * sizePlate[1]),
            isUp ? Convert.ToInt32(100 * sizePlate[1]):Convert.ToInt32(100 * sizePlate[0]),
            direction);
        k.transform.SetParent(transform);
        k.transform.localPosition = isUp ? Vector3.up * 0.0012f : Vector3.zero; 
        k.transform.localScale = Vector3.one * 0.2f;
        k.GetComponent<MeshRenderer>().material = testMaterial;
        k.ToMesh();
        k.Refresh();
    }

    public void GenerateSidePlates(bool isCloser=true)
    {
        float [] angelXYZ = new float[]{1,2,3};
        ProBuilderMesh k = ShapeGenerator.GeneratePlane(
            PivotLocation.FirstCorner, 
            isCloser? sizePlate[1]: sizePlate[0], 
            0.0012f,
            1,
            1,
            Axis.Up);
        k.transform.SetParent(transform);
        if (isCloser)
        {
            k.transform.localPosition = Vector3.up*0.0012f; 
            angelXYZ[0] = 0;
            angelXYZ[1] = 90;
            angelXYZ[2] = -90;
        }
        else
        {
            k.transform.localPosition = Vector3.zero;
            angelXYZ[0] = 0;
            angelXYZ[1] = 0;
            angelXYZ[2] = 90;
        }
        
        k.transform.localScale = new Vector3(1, 1,0.2f);
        k.transform.Rotate(angelXYZ[0], angelXYZ[1], angelXYZ[2]);
        k.GetComponent<MeshRenderer>().material = testMaterial;
        k.ToMesh();
        k.Refresh();

        var width = isCloser ? sizePlate[0] : sizePlate[1];
        k = ShapeGenerator.GeneratePlane(
            PivotLocation.FirstCorner, 
            isCloser? sizePlate[1]: sizePlate[0], 
            0.0012f,
            1,
            1,
            Axis.Down);
        k.transform.SetParent(transform);
        // width = isCloser? width: width * Vector3.right*0.2f;
        if (isCloser)
        {
            k.transform.localPosition =  width * Vector3.forward*0.2f ;
            angelXYZ[0] = -90;
            angelXYZ[1] = 0;
            angelXYZ[2] = 0;
        }
        else
        {
            k.transform.localPosition = width * Vector3.right*0.2f + Vector3.up*0.0012f;
            angelXYZ[0] = 90;
            angelXYZ[1] = -90;
            angelXYZ[2] = 0;
        }
        
        k.transform.localScale = new Vector3(0.2f, 1, 1);
        k.transform.Rotate(angelXYZ[0], angelXYZ[1], angelXYZ[2]);
        
        // if (position == Vector3.zero) k.transform.localPosition = position * 0.2f;
        // else if (position == Vector3.forward) k.transform.localPosition = position * 0.2f * sizePlate[0];
        // else k.transform.localPosition = position * 0.2f * sizePlate[1];
        // // k.transform.localPosition = position * 0.2f;
        // if (needUp) k.transform.localPosition += Vector3.up * 0.0012f;
        
        k.GetComponent<MeshRenderer>().material = testMaterial;
        k.ToMesh();
        k.Refresh(); 
        
    }
    
    

    public void Update()
    {
        if (Input.GetButtonDown("SaveJson"))
        {
            print("Pressed F1");
            SaveCurrentScheme();
        }
    }

    public void GenerateCurrentPlate()
    {
        GameObject places = new GameObject("places");
        places.transform.SetParent(transform);
        places.transform.localPosition = Vector3.zero;
        
        GenerateMainPlate(Axis.Up);    // up side
        GenerateMainPlate(Axis.Down);    // down side  
        GenerateSidePlates();
        GenerateSidePlates(false);
    }

    public void DeleteCurrentPlate()
    {
        for (int index = 0; index < transform.childCount; index++)
        {
            var a = transform.GetChild(index);
            Destroy(a.gameObject);
        }
    }

    public void LoadScheme()
    {
        BoardSaving boardLoaded = JsonUtility.FromJson<BoardSaving>(
            File.ReadAllText(Application.persistentDataPath + $"/SavedPlates/{namePlate}.json")
            );
        sizePlate[0] = boardLoaded.sizePlate[0];
        sizePlate[1] = boardLoaded.sizePlate[1];
        GenerateCurrentPlate();
        GameObject plate = transform.GetChild(2).gameObject;
        
        //restore traces for board
        ProBuilderMesh tracelines = plate.GetComponent<ProBuilderMesh>();
        tracelines.GetComponent<MeshRenderer>().sharedMaterials =  new []
        {
            testMaterial, traceMaterial
        };
        
        for (int i = 0; i < tracelines.faceCount; i++)
        {
            tracelines.faces[i].submeshIndex = 0;
        }

        foreach (int index in boardLoaded.tracesInd)
        {
            tracelines.faces[index].submeshIndex = 1;
        }
        tracelines.ToMesh();
        tracelines.Refresh();
        
        //set componentplaces to the board
        GameObject places = transform.GetChild(0).gameObject;
        GameObject openingPrefab = Resources.Load("op", typeof(GameObject)) as GameObject;
        foreach (PlaceInfo info in boardLoaded.places)
        {
            var instance = Resources.Load(info.namePlace.Replace("(Clone)", ""), typeof(GameObject)) as GameObject;
            GameObject place = Instantiate(instance, places.transform);
            place.transform.localPosition = info.position;
            place.transform.localRotation = info.rotation;
            for (int i = 0; i < place.transform.childCount; i++)
            {
                if(place.transform.GetChild(i).CompareTag("SolderSlot"))
                {
                    GameObject op = Instantiate(
                        openingPrefab,
                        place.transform.GetChild(i));
                    op.transform.SetParent(transform.GetChild(1));
                    op.transform.localPosition = new Vector3(
                        op.transform.localPosition.x,
                        0,
                        op.transform.localPosition.z);
                }
            }
        }
    }
    
    public List<int> GetTraceList(ProBuilderMesh plate)
    {
        List<int> result = new List<int>();
        for (int index = 0; index < plate.faceCount; index++)
        {
            if (plate.faces[index].submeshIndex == 1)
            {
                result.Add(index);
            }
        }

        return result;
    }

    public List<PlaceInfo> GetPlacesList()
    {
        List<PlaceInfo> info = new List<PlaceInfo>();
        var placesGroup = transform.GetChild(0);
        for (int childIndex = 0; childIndex < placesGroup.childCount; childIndex++)
        {
            PlaceInfo place = new PlaceInfo()
            {
                namePlace = placesGroup.GetChild(childIndex).name,
                nameModel =  placesGroup.GetChild(childIndex).gameObject.GetComponent<ComponentPlace>().sample.name,
                position = placesGroup.GetChild(childIndex).localPosition,
                rotation = placesGroup.GetChild(childIndex).localRotation
            };
            info.Add(place);
        }
        return info;
    }
    
    public void SaveCurrentScheme()
    {
        
        if (!Directory.Exists(Application.persistentDataPath + "/SavedPlates"))
            Directory.CreateDirectory(Application.persistentDataPath + "/SavedPlates");
        
        BoardSaving board = new BoardSaving();
        board.name = namePlate;
        board.sizePlate = sizePlate;
        board.countFaces = new[]
        {
            Convert.ToInt32(100 * sizePlate[0]),
            Convert.ToInt32(100 * sizePlate[1])
        };
        board.tracesInd = new List<int>();
        ProBuilderMesh tracelines = transform.GetChild(2).gameObject.GetComponent<ProBuilderMesh>();
        if (tracelines)
        {
            board.tracesInd = GetTraceList(tracelines);
        }
        else
        {
            Debug.LogError("No tracelines found.");
        }
        board.places = GetPlacesList();
        
        string savePath = Application.persistentDataPath + $"/SavedPlates/{board.name}.json";
        string json = JsonUtility.ToJson(board, true);
        File.WriteAllText(savePath, json);

        Debug.Log($"File has been created to \'{savePath}\'");




    }
    
}
