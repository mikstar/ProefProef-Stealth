using UnityEngine;
using System.Collections;

public class Char_Movement : MonoBehaviour {
    private Rigidbody rigbody;

	// Use this for initialization
	void Start () {
        rigbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 velo = Vector3.zero;

        velo.x = moveX*4;
        velo.z = moveY*4;

        rigbody.velocity = velo;

        //Climb ledge detection
        RaycastHit hit;
        float distanceToGround = 0;

        if (Physics.Raycast(transform.position, Vector3.forward, out hit))
        {
            if (hit.collider.gameObject.tag == "ClimbObj")
            {

            }

            //ledgeHit!
            Vector3 climbPoint = hit.point;

            float length = hit.transform.localScale.x * ((BoxCollider)hit.collider).size.x;
            float width = hit.transform.localScale.z * ((BoxCollider)hit.collider).size.z;
            float height = hit.transform.localScale.y * ((BoxCollider)hit.collider).size.y;
            Vector3 dimensions = new Vector3(length, height, width);
            //now to know the world position of top most level of the wall:
            climbPoint.y = hit.transform.position.y + dimensions.y / 2;


        }
    }
}
