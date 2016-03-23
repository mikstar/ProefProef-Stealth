using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
    public Vector3 personalLastSighting;
    public AudioClip spotted;

	private NavMeshAgent nav;
    private SphereCollider col;
    private GameObject player;
    private LastPlayerSighting lastPlayerSighting;
    private Vector3 previousSighting;
    private AudioSource audio;
    
    

    void Awake() {
		nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
        audio = GetComponent<AudioSource>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent<LastPlayerSighting>();

        personalLastSighting = lastPlayerSighting.resetPosition;
        previousSighting = lastPlayerSighting.resetPosition;
    }

    void Update() {

		if(lastPlayerSighting.position != previousSighting)
			personalLastSighting = lastPlayerSighting.position;

		previousSighting = lastPlayerSighting.position;

    }

    void OnTriggerStay(Collider other){
        if (other.gameObject == player)
		{
            playerInSight = false;
            Vector3 playerPos = other.transform.position + new Vector3(0f, 0.5f, 0f);
            Vector3 direction = playerPos - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle * 0.5f && playerPos.y < transform.position.y + 0.8f)
            {
	            RaycastHit hit;   

	            if (Physics.Raycast(transform.position, direction.normalized, out hit, col.radius)) {

	                if (hit.collider.gameObject.tag == "Player") {
                        
                        playerInSight = true;
                        lastPlayerSighting.position = player.transform.position;
                    
	                }
	            }
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject == player) {
            playerInSight = false;
        }
    }
}
