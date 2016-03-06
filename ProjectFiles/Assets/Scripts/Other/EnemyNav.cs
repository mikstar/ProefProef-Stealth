using UnityEngine;
using System.Collections;

public class EnemyNav : MonoBehaviour {
    
    //Public
    public LayerMask detectionLayer;
    
    //Private
    private Transform myTransform;
    private NavMeshAgent myNavMeshAgent;
    private Collider[] hitColliders;
    private float checkRate;
    private float nextCheck;
    private float detectionRadius = 10;

	void Start () {
        SetInitialReferences();
	}
	

	void Update () {
        CheckIfPlayerInRange();
	}

    void SetInitialReferences() {
        myTransform = transform;
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        checkRate = Random.Range(0.8f, 1.2f);
    }

    void CheckIfPlayerInRange()
    {
        if (Time.time > nextCheck && myNavMeshAgent.enabled == true) {
            nextCheck = Time.time + checkRate;

            hitColliders = Physics.OverlapSphere(myTransform.position, detectionRadius, detectionLayer);

            if (hitColliders.Length > 0) {
                myNavMeshAgent.SetDestination(hitColliders[0].transform.position);
            }
        }
    }
}
