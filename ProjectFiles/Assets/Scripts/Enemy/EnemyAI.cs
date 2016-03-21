using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    //Public
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float patrolWaitTime = 1f;
    public float chaseWaitTime = 5f;
    public float engageWaitTime = 1.5f;
    public Transform[] patrolWaypoints;

    public GameObject eMark;
    public GameObject qMark;
    public GameObject animObj;

    //Private
    private EnemySight enemySight;
    private NavMeshAgent nav;
    private Transform player;
    private PlayerHealth playerHealth;

    private float chaseTimer;
    private float patrolTimer;
    private float engageTimer;
    private int waypointIndex;
    private LastPlayerSighting lastPlayerSighting;

    private Animator anim;
    private bool chasing;

    private EnemyAttack enemAttack;
    

    void Awake() {
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent<LastPlayerSighting>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        anim = animObj.GetComponent<Animator>();
        enemAttack = transform.Find("SmackZone").gameObject.GetComponent<EnemyAttack>();
    }

    void Update() {
        if(enemySight.personalLastSighting != lastPlayerSighting.resetPosition && playerHealth.health > 0f)
            Chasing();
        else 
            Patrolling();
    }
    void Engaging() {
        if(chasing == false) { 
            Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;
            Quaternion muhrotation = Quaternion.LookRotation(sightingDeltaPos);
            Quaternion currentRot = transform.localRotation;

            anim.SetBool("Walking", false);
            anim.SetBool("Alert", true);
            nav.speed = 0f;

            transform.localRotation = Quaternion.Lerp(currentRot, muhrotation, Time.deltaTime * 4f);

            engageTimer += Time.deltaTime;
            
            if (engageTimer > engageWaitTime)
            {
                anim.SetBool("Alert", false);
                chasing = true;
            }
        }
    }
 
    void Chasing() {
        anim.SetBool("Walking", false);
        anim.SetBool("Running", true);

        Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;

        //if (sightingDeltaPos.sqrMagnitude > 4f)
		nav.destination = enemySight.personalLastSighting;
        nav.speed = chaseSpeed;
        
 
        if (nav.remainingDistance < nav.stoppingDistance){
            anim.SetBool("Running", false);

            if (enemAttack.playerInRange)
            {

                anim.SetTrigger("Smack");
                if (playerHealth.health >= 100f)
                    playerHealth.health = 0f;
            }

            chaseTimer += Time.deltaTime;

            //if (enemySight.playerInSight == false) { 
				
                if (chaseTimer >= chaseWaitTime)
                {    
                    lastPlayerSighting.position = lastPlayerSighting.resetPosition;
                    enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
                    chaseTimer = 0f;
                   
                }
            //}
        }else 
			chaseTimer = 0f;
          
      }

    void Patrolling() {
        
        nav.speed = patrolSpeed;
        anim.SetBool("Walking", true);
        
        if (nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance){

            anim.SetBool("Walking", false);
            patrolTimer += Time.deltaTime;
          

            if (patrolTimer >= patrolWaitTime)
            {
                if (waypointIndex == patrolWaypoints.Length - 1)
                    waypointIndex = 0;
                else
                    waypointIndex++;

                patrolTimer = 0f;
            }
        }
        else {
            patrolTimer = 0f;
        }

        nav.destination = patrolWaypoints[waypointIndex].position;
    }
}
