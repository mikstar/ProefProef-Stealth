using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
    public Vector3 personalLastSighting;

	private NavMeshAgent nav;
    private SphereCollider col;
    private GameObject player;
    private LastPlayerSighting lastPlayerSighting;
    private Vector3 previousSighting;

    void Awake() {
		nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
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

            if (angle < fieldOfViewAngle * 0.5f && other.transform.position.y < transform.position.y + 0.8f)
            {
	            RaycastHit hit;   

	            if (Physics.Raycast(transform.position, direction.normalized, out hit, col.radius)) {
                    Debug.Log(hit.collider.name);
                    Debug.DrawRay(transform.position, direction.normalized, Color.green);

	                if (hit.collider.gameObject.tag == "Player") {
						playerInSight = true;
                        lastPlayerSighting.position = player.transform.position;
                        Debug.Log("i can see you");
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
