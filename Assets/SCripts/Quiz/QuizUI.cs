using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizUI : MonoBehaviour
{
    [SerializeField]private QuizManager quizManager;
    [SerializeField]private Text questionText,timerText;
    [SerializeField]private Image questionImage;
    [SerializeField]AudioSource questionAudio;
    [SerializeField]private List<Button> options;
    [SerializeField]private Color correctColor, wrongColor, normalColor;
    public GameObject quizPanel,directionPanel,oKButton;
    public TextMeshProUGUI dirTex;
    private Question question;
    private bool answered;
    public Text TimerText { get { return timerText; }}
    void Start()
    {
        for (int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
    }

    // Update is called once per frame
    public void SetQuestion(Question question)
    {
        this.question = question;
        switch(question.questionType)
        {
            case QuestionType.TEXT:
            questionImage.transform.parent.gameObject.SetActive(false);
            break;
            case QuestionType.IMAGE:
            ImageHolder();
            //questionImage.transform.gameObject.SetActive(true);
            break;
        }
        questionText.text = question.questionInfo;
        List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);
        for (int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<Text>().text = answerList[i];
            options[i].name = answerList[i];
            options[i].image.color = normalColor;
        }
        answered = false;
    }
    void ImageHolder()
    {
        questionImage.transform.parent.gameObject.SetActive(true);
        questionImage.transform.gameObject.SetActive(true);
        questionImage.sprite = question.questionImage;
    }
    public void OKButton()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
        //directionPanel.SetActive(false);
    }
    private void OnClick(Button btn)
    {
        if(!answered)
        {
            answered = true;
            bool val =quizManager.Answer(btn.name);
            if(val)
            {
                btn.image.color = correctColor;
                //quizPanel.SetActive(false);
                //directionPanel.SetActive(true);
            }else
            {
                btn.image.color = wrongColor;
            }
        }
    }
}
