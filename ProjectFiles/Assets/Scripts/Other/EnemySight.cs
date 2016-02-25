﻿using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
    public Vector3 personalLastSighting;

    private GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Tester");
    }

    void Update() {
        if (playerInSight) {
            //Debug.Log("PLAYER IN SIGHT");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;
                
                if (Physics.Raycast(transform.position, direction.normalized, out hit, 1000)) {
                    if (hit.collider.gameObject == player) {
                        playerInSight = true;
                        personalLastSighting = player.transform.position;
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
