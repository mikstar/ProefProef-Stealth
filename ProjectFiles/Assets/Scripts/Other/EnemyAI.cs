using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float patrolWaitTime = 1f;
    public float chaseWaitTime = 5f;
    public Transform[] patrolWaypoints;

    private EnemySight enemySight;
    private NavMeshAgent nav;
    private Transform player;
    private PlayerHealth playerHealth;
    
   
    private float chaseTimer;
    private float patrolTimer;
    private int waypointIndex;
    private LastPlayerSighting lastPlayerSighting;

    void Awake() {
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent<LastPlayerSighting>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update() {
        if (enemySight.personalLastSighting != lastPlayerSighting.resetPosition && playerHealth.health > 0f)
        {
            Chasing();
        }
        else {
            Patrolling();
        }
    }

    void Chasing() {
        Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;
        if (sightingDeltaPos.sqrMagnitude > 4f) {
            nav.destination = enemySight.personalLastSighting;
            nav.speed = chaseSpeed;
        }

        if (nav.remainingDistance < nav.stoppingDistance)
        {
           
            chaseTimer += Time.deltaTime;
            if (chaseTimer > chaseWaitTime)
            {
                lastPlayerSighting.position = lastPlayerSighting.resetPosition;
                enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
                chaseTimer = 0f;
            }
        }
        else {
            chaseTimer = 0f;
        }
    }


    void Patrolling() {
        nav.speed = patrolSpeed;
        

        if (nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance){
            patrolTimer += Time.deltaTime;

            if (patrolTimer > patrolWaitTime)
            {
                if (waypointIndex == patrolWaypoints.Length - 1)
                {
                    waypointIndex = 0;
                }
                else {
                    waypointIndex++;
                }
            }
        }
        else {
            patrolTimer = 0f;
        }

        nav.destination = patrolWaypoints[waypointIndex].position;
    }
}
