using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Camfade : MonoBehaviour {

    public bool fadeInAtStart = true;
    public GameObject fadeObj;
    private int lvlNum = 0;

	// Use this for initialization
	void Start () {
        fadeObj.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        if (fadeInAtStart)
        {
            fadeObj.GetComponent<Image>().DOFade(0, 1.5f);
        }
	}
    public void fadeOut()
    {
        fadeObj.GetComponent<Image>().DOFade(1, 1.5f);
    }
    public void fadeOutNewScene(int sceneNum)
    {
        lvlNum = sceneNum;
        fadeObj.GetComponent<Image>().DOFade(1, 1.5f).OnComplete(testCall);
    }
    private void testCall()
    {
        SceneManager.LoadScene(lvlNum);
    }

}
