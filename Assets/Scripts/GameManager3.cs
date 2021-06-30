using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager3 : GameManager
{
    
    public int Completed = 0;
    public int toCompleted = 0;

    public GameObject board;

    private List<XRRayInteractor> interactors = new List<XRRayInteractor>();

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        countText = GameObject.Find("CountText").GetComponent<Text>();
        
        for (int i = 0; i < board.transform.childCount; i++)
        {
            if (board.transform.GetChild(i).gameObject.name.Contains("Slots"))
            {
                Transform slots = board.transform.GetChild(i);
                for (int j = 0; j < slots.childCount; j++)
                {
                    Transform slot = slots.GetChild(j); 
                    for (int k = 0; k < slot.childCount; k++)
                    {
                        if (slot.GetChild(k).CompareTag("SolderSlot")) toCompleted++;

                    }
                }
                // Transform placeComponent = slots.transform.GetChild(i);
                //
                // toCompleted += placeComponent.childCount - 1;
            }
            
        }

        Text tutorText = GameObject.Find("TutorialText").GetComponent<Text>();
        tutorText.text = "1. Вставить компонент в отверстия\n" +
                         "2. Пропаять паяльником с припоем\n" +
                         "выводы компонента у отверстий в плате\n\n" +
                         "Просмотрите внимательно, в какую сторону\n" +
                         "нужно вставлять конденсаторы и микросхемы.\n" +
                         "";
        UpdateCountText();
        interactors.Add(GameObject.Find("LeftHand Controller").GetComponent<XRRayInteractor>());
        interactors.Add(GameObject.Find("RightHand Controller").GetComponent<XRRayInteractor>());
        
    }

    // Update is called once per frame
    void Update()
    {
        // foreach (var interactor in interactors)
        // {
        //     XRController a = new XRController();
        //     interactor.TryGetCurrentUIRaycastResult()
        //     if (interactor.TryGetCurrentUIRaycastResult())
        //     {
        //         interactor.maxRaycastDistance = 5;
        //     }
        //     else interactor.maxRaycastDistance = 0.1f;
        // }
    }

    public void AddCompleted(int count = 1)
    {
        Completed += count;
        UpdateCountText();
        
    }

    public void UpdateCountText()
    {
        countText.text = Completed + " / " + toCompleted;
        SetCompletedText();
    }

    public void SetCompletedText()
    {
        if (Completed == toCompleted)
        {
            string congrats = "Поздравляем! Вы выполнили задание!";
            
            Text tutorText = GameObject.Find("TutorialText").GetComponent<Text>();
            tutorText.text = congrats;

        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scenes/Task3");
    }
}
