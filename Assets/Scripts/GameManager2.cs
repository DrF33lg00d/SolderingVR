using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2 : GameManager
{
    public GameObject iron;

    private int countToComplete = 0; 
    private int countCurrent = 0;
    
    
    void Start()
    {
        base.Start();
        Text tutorText = GameObject.Find("TutorialText").GetComponent<Text>();
        tutorText.text = "Инструкция пайки проводов:\n" +
                         "1. Возьмите провод одним контроллером и паяльник другим.\n" +
                         "2. Окуните конец провода в канифоль и прижмите на несколько секунд паяльником\n" +
                         "3. Положите провод на стол и аккуратно нанесите на конец провода с канифолью припой.\n" +
                         "4. Повторите 1-2 действия со вторым этого же цвета проводом.\n" +
                         "5. Положите 2 провода так, чтобы они касались друг друга концами с припоем.\n" +
                         "6. После этого проведите паяльником с припоем по месту их соединения.\n\n" +
                         "Повторите действия 1-5 с оставшимися парами проводов." +
                         "";
        iron = GameObject.Find("soldering_iron");
        countToComplete = GameObject.FindGameObjectsWithTag("WireGroup").Length;
        UpdateCompleteText();
        
    }


    public void AddCompleted(int c = 1)
    {
        countCurrent += c;
        UpdateCompleteText();
    }
    public void UpdateCompleteText()
    {
        countText.text = countCurrent + " / " + countToComplete;
        if (countCurrent == countToComplete) SetCompletedText();
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
