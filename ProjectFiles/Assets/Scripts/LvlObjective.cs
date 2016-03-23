using UnityEngine;
using System.Collections;

public class LvlObjective : MonoBehaviour {

    public bool hasObjective = false;
    public GameObject stealObj;
    public GameObject getEffect;
    public AudioSource Asource;


    // Update is called once per frame
    void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && hasObjective == false)
        {
            getEffect.SetActive(true);
            Asource.PlayScheduled(AudioSettings.dspTime + 0.5f);
            //Asource.Play();
            hasObjective = true;
            Destroy(stealObj);
        }
    }
}
