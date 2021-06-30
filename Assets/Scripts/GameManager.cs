using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool secondS = false;
    public bool thirdS = false;
    public string sceneName;
    public Text countText;




    private GameObject[] menu;
    private GameObject[] modal;
    private Text taskDescription;

    
    public void Start()
    {
        menu = GameObject.FindGameObjectsWithTag("UI_Scene");
        modal = GameObject.FindGameObjectsWithTag("UI_Scene_Modal");
        taskDescription = GameObject.Find("Task Description").GetComponent<Text>();
        taskDescription.text = "";
        SwitchMenuToModal(false);
        

    }

    public void OpenFirstScene()
    {
        sceneName = "Welcome";
        taskDescription.text = "В этом задании необходимо подготовить\n" +
                               "паяльник для дальнейшей работы с ним.\n" +
                               "Используйте паяльник, колбу с припоем\n" +
                               "и канифоль, чтобы залудить паяльник.";
        SwitchMenuToModal(true);
        
    }
    public void OpenSecondScene()
    {
        sceneName = "Welcome";
        taskDescription.text = "В этом задании вам предстоит спаять\n" +
                               "несколько проводов между собой.\n" +
                               "Используйте паяльник, колбу с припоем\n" +
                               "и канифоль для пайки проводов.";
        SwitchMenuToModal(true);
        
    }
    public void OpenThirdScene()
    {
        sceneName = "Task3";
        taskDescription.text = "В этом задании необходимо спаять\n" +
                               "радиокомпоненты к плате. На столе\n" +
                               "имеются необходимые компоненты.\n" +
                               "Следуйте инструкциям для успешного\n" +
                               "выполнения задания.";
        SwitchMenuToModal(true);
 
    }

    public void Confirm()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void Cancel()
    {
        SwitchMenuToModal(false);
        sceneName = null;
    }

    public void SwitchMenuToModal(bool isModal)
    {
        foreach (var item in modal)
        {
            item.SetActive(isModal);
        }
    }

    public void ExitFromApp()
    {
        // UnityEditor.EditorApplication.isPlaying = false; // disable when build
        Application.Quit();
        
    }
}
