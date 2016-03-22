using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Char_Movement : MonoBehaviour {
    private Rigidbody rigbody;
    private Animator anim;
    private bool actionActive = false;
    private bool falling = false;
    private bool hanging = false;
    public float maxSpeed = 4f;
    private Vector3 graficModeloffset;
    private Vector3 graficModeloffsetRotation;
    public Transform camCenterObj;
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
                if (!hanging)
                {
                    //normal walking state

                    //slip off ledge detection
                    RaycastHit hit;
                    if (!Physics.Raycast(transform.position + (transform.up * 0.5f), -transform.transform.up, out hit, 1))
                    {
                        //go to fall state
                        falling = true;
                        actionActive = true;
                        rigbody.isKinematic = true;
                        transform.DOMove(transform.position + (transform.forward * 0.5f) - (transform.up * 0.5f), 0.2f).SetEase(Ease.InQuad).OnComplete(droppedPlayer);
                        anim.SetBool("Moving", false);
                        anim.SetBool("Falling", true);
                    }
                    else
                    {
                        //set movement by input
                        rigbody.angularVelocity = Vector3.zero;
                        float moveX = Input.GetAxis("Horizontal");
                        float moveZ = Input.GetAxis("Vertical");

                        Vector3 velo = Vector3.zero;
                        velo.x = moveX * maxSpeed;
                        velo.z = moveZ * maxSpeed;

                        //rotate player in direction of movement
                        if (velo.magnitude > 0)
                        {
                            velo = velo * (maxSpeed / velo.magnitude);
                            transform.rotation = Quaternion.LookRotation(velo);
                            velo.y = rigbody.velocity.y;
                            rigbody.velocity = velo;

                            anim.SetBool("Moving", true);
                        }
                        else
                        {
                            anim.SetBool("Moving", false);
                        }


                        //Climb ledge detection
                        if (Input.GetButton("Fire3"))
                        {
                            //JumpCheck
                            RaycastHit hitT;
                            if (!Physics.Raycast(transform.position + (transform.up * 0.5f) + (transform.forward * 0.5f), -transform.up, 1))
                            {
                                if (Physics.Raycast(transform.position + (-transform.up * 0.5f) + (transform.forward * 0.5f), -transform.forward, out hitT, 0.5f))
                                {
                                    if (!Physics.Raycast(hitT.point + transform.up, hitT.normal, 1.5f))
                                    {
                                        Debug.DrawRay(hitT.point + transform.up + (hitT.normal * 1.5f), Vector3.down, Color.green, 0.2f);
                                        if (Physics.Raycast(hitT.point + transform.up + (hitT.normal * 1.5f), Vector3.down, 1f))
                                        {
                                            transform.DOMove(hitT.point + (Vector3.up * 0.5f) - (hitT.normal * 0.3f), 0.2f);
                                            transform.LookAt(transform.position + hitT.normal);
                                            actionActive = true;
                                            rigbody.isKinematic = true;
                                            anim.SetBool("Moving", false);
                                            anim.SetTrigger("JumpForward");
                                            camCenterObj.DOLocalMove(new Vector3(0, 0, 2f), 1);
                                        }
                                    }
                                }
                            }
                            //ClimbUpCheck
                            if (Physics.Raycast(transform.position + (transform.up * 0.5f), transform.transform.forward, out hit, 0.75f))
                            {
                                if (hit.collider.gameObject.tag == "ClimbObj")
                                {
                                    Vector3 climbPoint = hit.point;

                                    float length = hit.transform.localScale.x * ((BoxCollider)hit.collider).size.x;
                                    float width = hit.transform.localScale.z * ((BoxCollider)hit.collider).size.z;
                                    float height = hit.transform.localScale.y * ((BoxCollider)hit.collider).size.y;
                                    Vector3 dimensions = new Vector3(length, height, width);
                                    //now to know the world position of top most level of the wall:
                                    climbPoint.y = (hit.transform.position.y + dimensions.y / 2) - 0.5f;
                                    camCenterObj.DOLocalMove(new Vector3(0, 1, 0.4f), 1);
                                    transform.DOMove(climbPoint + (hit.normal * 0.2f) - new Vector3(0, 0.5f, 0), 0.2f);
                                    transform.LookAt(transform.position - hit.normal);
                                    actionActive = true;
                                    rigbody.isKinematic = true;
                                    anim.SetTrigger("ClimbUp");
                                    anim.SetBool("Moving", false);
                                }
                                else if (hit.collider.gameObject.tag == "HangObj")
                                {
                                    Vector3 climbPoint = hit.point;

                                    float length = hit.transform.localScale.x * ((BoxCollider)hit.collider).size.x;
                                    float width = hit.transform.localScale.z * ((BoxCollider)hit.collider).size.z;
                                    float height = hit.transform.localScale.y * ((BoxCollider)hit.collider).size.y;
                                    Vector3 dimensions = new Vector3(length, height, width);
                                    //now to know the world position of top most level of the wall:
                                    climbPoint.y = (hit.transform.position.y + dimensions.y / 2) - 0.5f;

                                    transform.DOMove(climbPoint + (hit.normal * 0.2f) - new Vector3(0, 0.5f, 0), 0.2f);
                                    transform.LookAt(transform.position - hit.normal);
                                    rigbody.isKinematic = true;
                                    hanging = true;
                                    actionActive = true;
                                    anim.SetTrigger("GrabUpToLedge");
                                    anim.SetBool("Moving", false);
                                }
                            }
                        }
                        else if (Input.GetButton("Fire2"))
                        {
                            if (!Physics.Raycast(transform.position + (transform.up * 0.5f) + (transform.forward * 0.5f), -transform.up, 1))
                            {
                                RaycastHit hitE;
                                if (Physics.Raycast(transform.position + (-transform.up * 0.5f) + (transform.forward * 0.5f), -transform.forward, out hitE, 0.5f))
                                {
                                    transform.DOMove(hitE.point + (transform.up * 0.5f) - (hitE.normal*0.3f),0.2f);
                                    transform.LookAt(transform.position + hitE.normal);

                                    hanging = true;
                                    actionActive = true;
                                    rigbody.isKinematic = true;
                                    anim.SetTrigger("ClimbDown");
                                    anim.SetBool("Moving", false);
                                }
                            }
                        }
                    }
                }
                else
                {
                    //hangstate

                    //climb left/right
                    if (Input.GetAxis("Horizontal") < 0 && !Physics.Raycast(transform.position + (transform.up * 0.5f), -transform.right, 0.5f))
                    {
                        if (Physics.Raycast(transform.position + (-transform.right * 0.5f) + (transform.up * 0.4f), transform.forward, 0.7f))
                        {
                            transform.position += -transform.right * 0.015f;
                            anim.SetBool("Moving", true);
                            anim.SetBool("ClimbingLeft", true);

                        }
                    }
                    else if (Input.GetAxis("Horizontal") > 0 && !Physics.Raycast(transform.position + (transform.up * 0.5f), transform.right, 0.5f))
                    {
                        if (Physics.Raycast(transform.position + (transform.right * 0.5f) + (transform.up * 0.4f), transform.forward, 0.7f))
                        {
                            transform.position += transform.right * 0.015f;
                            anim.SetBool("Moving", true);
                            anim.SetBool("ClimbingLeft", false);
                        }
                    }
                    else
                    {
                        anim.SetBool("Moving", false);
                    }

                    if (Input.GetAxis("Vertical") > 0)
                    {
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position + (transform.up * 0.5f), transform.transform.forward, out hit, 0.75f))
                        {
                            if (hit.collider.gameObject.tag == "ClimbObj")
                            {
                                //climb up
                                actionActive = true;
                                hanging = false;
                                rigbody.isKinematic = true;
                                anim.SetBool("Moving", false);
                                anim.SetTrigger("ClimbUp");
                                camCenterObj.DOLocalMove(new Vector3(0, 1, 0.4f), 1);
                            }
                        }
                    }
                    else if (Input.GetAxis("Vertical") < 0)
                    {

                        hanging = false;
                        falling = true;
                        anim.SetBool("Falling", true);
                        anim.SetBool("FallNow", true);
                        anim.SetBool("Moving", false);
                        rigbody.isKinematic = false;
                    }
                }
            }
            else
            {
                //Falling
                RaycastHit hit;
                if (Physics.Raycast(transform.position + (transform.up * 0.5f), -transform.transform.up * 5, out hit))
                {
                    if(hit.distance < 0.55f)
                    {
                        falling = false;
                        anim.SetBool("Falling", false);
                    }
                }
            }
        }
    }
    public void resetCharPosX(float offset)
    {
        graficModeloffset.x = offset;
    }
    public void resetCharPosY(float offset)
    {
        graficModeloffset.y = offset;
    }
    public void resetCharPosZ(float offset)
    {
        graficModeloffset.z = offset;
    }
    public void resetCharRotaX(float offset)
    {
        graficModeloffsetRotation.x = offset;
    }
    public void resetCharRotaY(float offset)
    {
        graficModeloffsetRotation.y = offset;
    }
    public void resetCharRotaZ(float offset)
    {
        graficModeloffsetRotation.z = offset;
    }
    public void resetCharPos()
    {
        resetCharPosStayStatic();
        rigbody.isKinematic = false;
    }
    public void resetCharPosStayStatic()
    {
        Vector3 tempPos = transform.position;
        tempPos += transform.forward * graficModeloffset.z;
        tempPos += transform.right * graficModeloffset.x;
        tempPos += transform.up * graficModeloffset.y;
        transform.position = tempPos;
        camCenterObj.position = transform.position;
        transform.Rotate(graficModeloffsetRotation);

        actionActive = false;

        graficModeloffset = Vector3.zero;
        graficModeloffsetRotation = Vector3.zero;
    }
    public void actionDone()
    {
        actionActive = false;
    }

    public void droppedPlayer()
    {
        actionActive = false;
        rigbody.isKinematic = false;
        rigbody.velocity = new Vector3(0,-4,0);
    }

    public void killPlayer()
    {
        actionActive = true;
        rigbody.isKinematic = true;
    }
}
