using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuizManager : MonoBehaviour
{
    public QuizUI quizUI;
    [SerializeField]private QUizDataScriptable quizData;
    private  List<Question> questions;
    private Question selectedQuestion;
    private float currentTime;
    public float timeLimit = 30;
    public int correctAnswers =0;
    public int failedAnswers =0;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = timeLimit;
        questions = quizData.questions;
        SelectQuestion();
    }

    void SelectQuestion()
    {
        int val = Random.Range(0,questions.Count);
        selectedQuestion = questions[val];
        quizUI.SetQuestion(selectedQuestion);
        //questions.RemoveAt(val);
    }
    private void Update() {
        currentTime-= Time.deltaTime;
        SetTimer(currentTime);
    }
    private void SetTimer(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        quizUI.TimerText.text = "Time : "+ time.ToString("mm':'ss");
        if(currentTime <= 0)
        {
            quizUI.directionPanel.SetActive(true);
            //percentage();
        }
    }
    private float percentage()
    {
        int a,sum,percent;
        sum = failedAnswers + correctAnswers;
        a = correctAnswers / sum;
        percent = a * 100;
        return percent;
    }
    public bool Answer(string answered)
    {
        bool correctAnswer = false;
        if(answered == selectedQuestion.correctAnswer)
        {
            
            correctAnswer = true;
            correctAnswers++;
            Invoke("SelectQuestion", 0.4f);
        }
        else
        {
            //No
            failedAnswers++;
            Invoke("SelectQuestion", 0.4f);
        }
        //Invoke("SelectQuestion", 0.4f);
        return correctAnswer;
    }
}
[System.Serializable]
public class Question
{
    public string questionInfo;
    public QuestionType questionType;
    public Sprite questionImage;
    public List<string> options;
    public string correctAnswer;

}
[System.Serializable]
public enum QuestionType
{
    TEXT,IMAGE
}
