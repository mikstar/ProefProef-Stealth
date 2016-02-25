using UnityEngine;
using System.Collections;

public class EaseFollow : MonoBehaviour {

    public Transform followObj;
    public Vector3 offset;
    [Range(0f, 1f)]
    public float easeStrength;
    private Transform trans;

	// Use this for initialization
	void Start () {
        trans = transform;
        trans.position = followObj.position + offset;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        trans.position = Vector3.Lerp(trans.position, followObj.position+offset,easeStrength);
	}
}
