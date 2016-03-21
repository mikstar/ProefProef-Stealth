using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
    public Vector3 personalLastSighting;

	private NavMeshAgent nav;
    private GameObject player;
    private LastPlayerSighting lastPlayerSighting;
    private Vector3 previousSighting;

    void Awake() {
		nav = GetComponent<NavMeshAgent>();
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
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle * 0.5f && other.transform.position.y < transform.position.y + 0.8f)
            {
	            RaycastHit hit;   

	            if (Physics.Raycast(transform.position, direction.normalized, out hit, 1000)) {
					Debug.DrawRay(transform.position, direction.normalized, Color.green);
	                if (hit.collider.gameObject == player) {
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
