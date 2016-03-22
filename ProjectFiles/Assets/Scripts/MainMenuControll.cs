using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuControll : MonoBehaviour {

    public Transform camObj;

    public RectTransform TitleIcon;
    public RectTransform TitleText;
    public RectTransform startBut;
    public RectTransform quitBut;
    public RectTransform quitButY;
    public RectTransform quitButN;


    public Transform startCamPos;
    public Transform menuCamPos;
    public Transform quitCamPos;

    private bool started = true;

    // Use this for initialization
    void Start () {
        camObj.DOMove(startCamPos.position, 4).SetEase(Ease.InOutQuart).OnComplete(fadeTitle).SetDelay(1.5f);
        camObj.DORotate(startCamPos.eulerAngles,4).SetEase(Ease.InOutQuart).SetDelay(1.5f);
    }

    private void fadeTitle()
    {
        TitleIcon.gameObject.GetComponent<Image>().DOFade(1, 1);
        TitleText.gameObject.GetComponent<Text>().DOFade(1, 1).SetDelay(0.5f);
        started = false;
    }

    void Update()
    {
        if (started == false && Input.anyKeyDown)
        {
            started = true;
            toMenu();
        }
    }
    
    public void toMenu()
    {
        TitleIcon.DOAnchorPosY((Screen.height*0.65f) - (Screen.height/2), 1);
        startBut.DOAnchorPosY((Screen.height * 0.4f) - (Screen.height / 2), 1);
        quitBut.DOAnchorPosY((Screen.height * 0.3f) - (Screen.height / 2), 1);
        TitleText.gameObject.GetComponent<Text>().DOFade(0, 0.5f);

        quitButY.DOAnchorPosX((Screen.width * -0.3f) - (Screen.width / 2), 1);
        quitButN.DOAnchorPosX((Screen.width * 1.3f) - (Screen.width / 2), 1);


        camObj.DOMove(menuCamPos.position,1).SetEase(Ease.OutQuart);
        camObj.DORotate(menuCamPos.eulerAngles, 1).SetEase(Ease.OutQuart);
    }

    public void startGame()
    {

        TitleIcon.DOAnchorPosY((Screen.height * 1.3f) - (Screen.height / 2), 1);
        startBut.DOAnchorPosY((Screen.height * -0.3f) - (Screen.height / 2), 1);
        quitBut.DOAnchorPosY((Screen.height * -0.3f) - (Screen.height / 2), 1);

        Camera.main.GetComponent<Camfade>().fadeOut();
        //Camera.main.GetComponent<Camfade>().fadeOutNewScene(1);
    }

    public void endGame()
    {
        Application.Quit();
    }

    public void toExitScreen()
    {
        TitleIcon.DOAnchorPosY((Screen.height * 1.3f) - (Screen.height / 2), 1);
        startBut.DOAnchorPosY((-Screen.height * 0.2f) - (Screen.height / 2), 1);
        quitBut.DOAnchorPosY((-Screen.height * 0.2f) - (Screen.height / 2), 1);

        quitButY.DOAnchorPosX((Screen.width * 0.3f) - (Screen.width / 2), 1);
        quitButN.DOAnchorPosX((Screen.width * 0.7f) - (Screen.width / 2), 1);

        camObj.DOMove(quitCamPos.position, 1).SetEase(Ease.OutQuart);
        camObj.DORotate(quitCamPos.eulerAngles, 1).SetEase(Ease.OutQuart);
    }
}
