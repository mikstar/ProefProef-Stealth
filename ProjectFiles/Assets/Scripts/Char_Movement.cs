using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Char_Movement : MonoBehaviour {
    private Rigidbody rigbody;
    private Animator anim;
    private bool actionActive = false;
    private bool falling = false;
    public float maxSpeed = 4f;
    public GameObject graficModel;
    //public Transform testObj;

	// Use this for initialization
	void Start () {
        rigbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!actionActive)
        {
            if (!falling)
            {
                rigbody.angularVelocity = Vector3.zero;
                //Debug.Log(rigbody.velocity.magnitude);
                float moveX = Input.GetAxis("Horizontal");
                float moveY = Input.GetAxis("Vertical");

                Vector3 velo = Vector3.zero;
                velo.x = moveX * maxSpeed;
                velo.z = moveY * maxSpeed;

                if (velo.magnitude > 0)
                {
                    velo = velo * (maxSpeed / velo.magnitude);
                    rigbody.velocity = velo;
                    transform.rotation = Quaternion.LookRotation(rigbody.velocity);
                }

                //slip off ledge detection
                RaycastHit hit;
                if (!Physics.Raycast(transform.position, -graficModel.transform.up, out hit, 1))
                {
                    Debug.Log("Fall!!");
                    falling = true;
                    actionActive = true;
                    rigbody.isKinematic = true;
                    transform.DOMove(transform.position + (transform.forward*0.5f) - (transform.up * 0.5f), 0.2f).SetEase(Ease.OutQuad).OnComplete(droppedPlayer);
                
                }

                //Climb ledge detection
                if (Physics.Raycast(transform.position, graficModel.transform.forward, out hit,0.75f))
                {
                    if (hit.collider.gameObject.tag == "ClimbObj")
                    {
                        if (Input.GetButtonDown("Fire3"))
                        {
                            Vector3 climbPoint = hit.point;

                            float length = hit.transform.localScale.x * ((BoxCollider)hit.collider).size.x;
                            float width = hit.transform.localScale.z * ((BoxCollider)hit.collider).size.z;
                            float height = hit.transform.localScale.y * ((BoxCollider)hit.collider).size.y;
                            Vector3 dimensions = new Vector3(length, height, width);
                            //now to know the world position of top most level of the wall:
                            climbPoint.y = (hit.transform.position.y + dimensions.y / 2)-0.5f;

                            transform.position = climbPoint + (hit.normal/2);
                            actionActive = true;
                            rigbody.isKinematic = true;
                            anim.SetTrigger("ClimbUp");
                        }
                    }
                }
            }
            else
            {
                Debug.Log("falling");
                RaycastHit hit;
                if (Physics.Raycast(transform.position, -graficModel.transform.up, out hit))
                {
                    if(hit.distance < 0.55f)
                    {
                        falling = false;
                        Debug.Log("Landed");
                    }
                }
            }
        }
    }
    public void resetCharPos()
    {
        Vector3 tempPos =  graficModel.transform.position;
        tempPos.y -= 0.17f;
        transform.position = tempPos;
        
        actionActive = false;
        rigbody.isKinematic = false;
    }

    public void droppedPlayer()
    {
        actionActive = false;
        rigbody.isKinematic = false;
        rigbody.velocity = new Vector3(0,-5,0);
    }
}
