using UnityEngine;
using System.Collections;

public class LvlStartEnd : MonoBehaviour {

    public LvlObjective objectives;

	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && objectives.hasObjective == true)
        {
            Camera.main.GetComponent<Camfade>().fadeOutNewScene(0);

        }
    }
}
