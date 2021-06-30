using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2 : GameManager
{
    public GameObject iron;
    void Start()
    {
        base.Start();
        Text tutorText = GameObject.Find("TutorialText").GetComponent<Text>();
        tutorText.text = "1. \n";
        iron = GameObject.Find("soldering_iron");
    }

    void Update()
    {
        if (iron.GetComponent<SolderingIron>().isTinned && iron.transform.childCount < 2)
        {
            SetCompletedText();
        }
    }
    
    public void SetCompletedText()
    {
        string congrats = "Поздравляем! Вы выполнили задание!\n" +
                          "Можете перезапустить задание или\n" +
                          "выбрать другое на доске.";
            
        Text tutorText = GameObject.Find("TutorialText").GetComponent<Text>();
        tutorText.text = congrats;
    }
    
    public void Restart()
    {
        SceneManager.LoadScene("Scenes/Task2");
    }
}
