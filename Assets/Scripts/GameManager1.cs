using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager1 : GameManager
{
    public GameObject iron;
    void Start()
    {
        base.Start();
        Text tutorText = GameObject.Find("TutorialText").GetComponent<Text>();
        tutorText.text = "1. Возьмите паяльник за деревянную ручку. Ни в коем случае не берите паяльник\n" +
                         "за нагретую металлическую часть для своей безопасности.\n" +
                         "2. Возьмите колбу с припоем в другую руку. Колба представлена в виде\n" +
                         "прозрачной колбы с красной крышкой и тонкой сипралью припоя внутри.\n" +
                         "3. Введите жало паяльника в канифоль и нанесите на жало припой.\n" +
                         "Старайтесь не держать слишком долго припой. Жало должно быть покрыто\n" +
                         "тонким металлическим слоем.\n" +
                         "4. Если не получилось и на жале слишком много припоя, то используте очиститель\n" +
                         "чтобы очистить жало и повторите пункт 3 снова.";
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
        SceneManager.LoadScene("Scenes/Task1");
    }
}
