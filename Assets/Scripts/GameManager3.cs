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
        tutorText.text = "1. Возьмите деталь с рабочего стола и вставить её в место,\n" +
                         "соответствующее маркировке на плате.\n" +
                         "2. Переверните плату, возьмите паяльник и колбу с припоем.\n" +
                         "3. Приложите паяльник с припоем к выводам детали у отверстий\n" +
                         "в плате.\n" +
                         "4. Повторите 1-3 пункты со всеми деталями на столе.\n\n" +
                         "Просмотрите внимательно, в какую сторону нужно вставлять\n" +
                         "конденсаторы, микросхемы и диоды в плату.\n" +
                         "";
        UpdateCountText();

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
