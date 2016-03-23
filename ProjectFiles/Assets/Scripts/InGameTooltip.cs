using UnityEngine;
using System.Collections;

public class InGameTooltip : MonoBehaviour {

    private float size = 0;
    public GameObject tooltipImg;

	// Use this for initialization
	void Start () {
        tooltipImg.transform.localScale = Vector3.zero;
        tooltipImg.transform.localPosition = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float i = Mathf.Lerp(size, tooltipImg.transform.localScale.y,0.8f);
        tooltipImg.transform.localScale = new Vector3(-i,i,i);
        tooltipImg.transform.LookAt(Camera.main.transform);

        
        tooltipImg.transform.localPosition = new Vector3(0, Mathf.Lerp(size, tooltipImg.transform.localPosition.y, 0.8f), 0);

    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            size = 1;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            size = 0;
        }
    }
}
